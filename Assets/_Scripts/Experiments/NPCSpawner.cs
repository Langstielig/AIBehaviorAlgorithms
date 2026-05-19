using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;
    [SerializeField] private int countOfNPC;

    public List<GameObject> spawnedNPCs = new List<GameObject>();

    private static float minXPosition = -4f;
    private static float maxXPosition = 11;

    private static float yPosition = 0;

    private static float minZPosition = -6;
    private static float maxZPosition = 10;

    private void Start()
    {
        float xPos = Random.Range(minXPosition, maxXPosition);
        float zPos = Random.Range(minZPosition, maxZPosition);
        Vector3 pos = new Vector3(xPos, yPosition, zPos);

        GameObject newNPC = Instantiate(NPCPrefab, pos, NPCPrefab.transform.rotation);

        spawnedNPCs.Add(newNPC);
    }
}
