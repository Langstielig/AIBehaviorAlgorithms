using System.Diagnostics;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    private bool finishedDeciding;
    public bool finishedExecutingBestAction { get; set; }

    public Action bestAction { get; set; }
    private NPCController npc;
    [SerializeField] private Action[] availableActions;

    [SerializeField] private Billboard billboard;

    [SerializeField] private bool withActionCost;
    [SerializeField] private bool withStochasticSelection;

    private Stopwatch stopwatch = new Stopwatch();
    private float fpsUpdateTimer;
    private float fps;
    private string logPath;
    private string lastActionName;
    private int framesSinceLastActionChange;

    void Start()
    {
        npc = GetComponent<NPCController>();
        finishedDeciding = false;
        finishedExecutingBestAction = false;

        //logPath = Path.Combine(Application.persistentDataPath, "AIPerformingLog.csv");
        //if(!File.Exists(logPath))
        //{
        //    File.WriteAllText(logPath, "Time,DecisionTime(ms),FPS,ActionChanges,GCAlloc(B)\n");
        //}
    }

    void Update()
    {
        fpsUpdateTimer += Time.deltaTime;
        if(fpsUpdateTimer >= 1f)
        {
            fps = 1f / Time.deltaTime;
            fpsUpdateTimer = 0f;
        }

        framesSinceLastActionChange++;
    }

    // Loop though all the available actions
    // Give me the highest scoring action
    public void DecideBestAction()
    {
        UnityEngine.Debug.Log("Utility AI recalculated");

        finishedExecutingBestAction = false;

        stopwatch.Restart();

        float score = 0f;
        float total = 0f;
        int nextBestActionIndex = 0;

        for(int i = 0; i < availableActions.Length; i++)
        {
            if (ScoreAction(availableActions[i]) > score)
            {
                total += availableActions[i].score;
                nextBestActionIndex = i;
                score = availableActions[i].score;
            }
        }

        //проверка, если total 0?

        if(withStochasticSelection)
        {
            float temperature = 0.3f;
            float[] expScores = new float[availableActions.Length];
            float expSum = 0f;

            for(int i = 0; i < availableActions.Length; i++)
            {
                expScores[i] = Mathf.Exp((availableActions[i].score - score) / temperature);
                expSum += expScores[i];
            }

            nextBestActionIndex = -1;
            float randomPoint = Random.value * expSum;
            float cumulative = 0f;

            for (int i = 0; i < availableActions.Length && nextBestActionIndex == -1; i++)
            {
                cumulative += expScores[i];
                if(cumulative >= randomPoint)
                {
                    nextBestActionIndex = i;
                }
            }
        }

        bestAction = availableActions[nextBestActionIndex];
        bestAction.SetRequiredDestination(npc);

        finishedDeciding = true;
        billboard.UpdateBestAction(bestAction.Name);

        MyLogger.LogStats(npc, bestAction);

        stopwatch.Stop();

        if(bestAction.Name != lastActionName)
        {
            MyLogger.LogPerformance(stopwatch.Elapsed.TotalMilliseconds, fps, bestAction.name, framesSinceLastActionChange);
            framesSinceLastActionChange = 0;
            lastActionName = bestAction.name;
        }
        else
        {
            MyLogger.LogPerformance(stopwatch.Elapsed.TotalMilliseconds, fps, bestAction.name, 0);
        }
    }

    // Loop though all the considerations of the action
    // Score all the considerations 
    // Average the consideration scores (not just average) to overall action score
    private float ScoreAction(Action action)
    {
        float score = 1f;
        for(int i = 0; i < action.considerations.Length; i++)
        {
            float considerationScore = action.considerations[i].ScoreConsideration(npc);
            score *= considerationScore;

            if(score == 0f)
            {
                action.score = 0f;
                return action.score;
            }
        }

        //Averaging scheme of overall score
        float originalScore = score;
        float modificationFactor = 1 - (1 / action.considerations.Length);
        float makeupValue = (1 - originalScore) * modificationFactor;
        action.score = originalScore + makeupValue * originalScore;

        if (withActionCost)
        {
            float totalCost = action.CalculateDynamicCost(npc);
            action.score *= (1f - totalCost);
        }

        return action.score;
    }
}
