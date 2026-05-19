using UnityEngine;

[CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
public class Sleep : Action
{
    public override HFSMAction CreateHFSMAction()
    {
        return new SleepHFSMAction();
    }

    public override void Execute(NPCController npc)
    {
        //MyLogger.LogStats(npc, this);
        Debug.Log("SleepAction");

        npc.DoSleep();
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        RequiredDestination = npc.context.home.transform;
        npc.moveController.destination = RequiredDestination;
    }
}
