using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public bool finishedDeciding { get; set; }
    public bool finishedExecutingBestAction { get; set; }

    public Action bestAction { get; set; }
    private NPCController npc;
    [SerializeField] private Action[] availableActions;

    [SerializeField] private Billboard billboard;

    void Start()
    {
        npc = GetComponent<NPCController>();
        finishedDeciding = false;
        finishedExecutingBestAction = false;
    }

    void Update()
    {
        //if(bestAction == null)
        //{
        //    DecideBestAction(npc.availableActions);
        //}
    }

    // Loop though all the available actions
    // Give me the highest scoring action
    public void DecideBestAction()
    {
        finishedExecutingBestAction = false;

        float score = 0f;
        int nextBestActionIndex = 0;
        for(int i = 0; i < availableActions.Length; i++)
        {
            if (ScoreAction(availableActions[i]) > score)
            {
                nextBestActionIndex = i;
                score = availableActions[i].score;
            }
        }

        bestAction = availableActions[nextBestActionIndex];
        bestAction.SetRequiredDestination(npc);

        finishedDeciding = true;
        billboard.UpdateBestAction(bestAction.Name);
    }

    // Loop though all the considerations of the action
    // Score all the considerations 
    // Average the consideration scores (not just average) to overall action score
    public float ScoreAction(Action action)
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

        return action.score;
    }
}
