using System.IO;
using UnityEngine;
using System.Globalization;

public class MyLogger
{
    private static string statsPath = Application.dataPath + "/AI_Log.csv";
    private static string performancePath = Application.persistentDataPath + "/AIPerformingLog.csv";

    private static bool statsHeaderWritten = false;
    private static bool performanceHeaderWritten = false;

    private static void WriteInStatsPath(string item)
    {
        if(!statsHeaderWritten)
        {
            File.AppendAllText(statsPath, "time,actionName,score,cost,distanceCost,totalCost,hunger,sleep,money,inventory");
            statsHeaderWritten = true;
        }
        File.AppendAllText(statsPath, item + "\n");
    }

    public static void LogStats(NPCController npc, Action action)
    {
        WriteInStatsPath(string.Format(CultureInfo.InvariantCulture, "{0:F2},{1},{2:F2},{3:F2},{4:F2},{5:F2},{6:F2},{7:F2},{8:F2},{9:F2}",
            Time.time, action.name, action.score, action.cost, action.distanceCost,
            action.totalCost, npc.stats.hunger, npc.stats.energy, npc.stats.money, 
            npc.Inventory.Inventory[ResourceType.wood]));
    }

    private static void WriteInPerformancePath(string item)
    {
        if (!performanceHeaderWritten)
        {
            Debug.Log("PATH: " + performancePath);
            File.AppendAllText(performancePath, "Time,DecisionTime(ms),FPS,ActionName,ActionChangesFrames,GCAlloc(B)\n");
            performanceHeaderWritten = true;
        }
        File.AppendAllText(performancePath, item + "\n");
    }

    public static void LogPerformance(double decisionTime, float fps, string actionName, int actionChangeFrames)
    {
        float time = Time.time;
        string timeString = time.ToString("F2", CultureInfo.InvariantCulture);

        string decisionTimeString = decisionTime.ToString("F3", CultureInfo.InvariantCulture);

        string fpsString = fps.ToString("F1" ,CultureInfo.InvariantCulture);

        long gcAlloc = System.GC.GetTotalMemory(false);

        WriteInPerformancePath($"{timeString},{decisionTimeString},{fpsString},{actionName},{actionChangeFrames},{gcAlloc}");
    }
}
