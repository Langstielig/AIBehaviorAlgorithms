using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform destination;
    private NavMeshSurface surface;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Debug.Log("Agent on nav mesh " + agent.isOnNavMesh);
        if (!agent.isOnNavMesh)
        {
            if (NavMesh.SamplePosition(agent.transform.position, out var hit, 2f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
                //Debug.Log("Warped agent onto NavMesh");
            }
            else
            {
                //Debug.Log("Could not find NavMesh under agent");
            }
        }

        //Debug.Log("Destination: " + (destination != null ? destination.position.ToString() : "null"));
    }

    //IEnumerator Start()
    //{
    //    surface = FindObjectOfType<NavMeshSurface>();

    //    if(surface != null && surface.navMeshData == null)
    //    {
    //        Debug.Log("Building NavMesh...");
    //        surface.BuildNavMesh();
    //        yield return null;
    //    }

    //    if(NavMesh.SamplePosition(agent.transform.position, out var hit, 2f, NavMesh.AllAreas))
    //    {
    //        agent.Warp(hit.position);
    //        Debug.Log("Agent warped to NavMesh");
    //    }
    //    else
    //    {
    //        Debug.Log("Agent is off NavMesh at start");
    //    }

    //    yield return new WaitForEndOfFrame();

    //    if(destination != null)
    //    {
    //        agent.SetDestination(destination.position);
    //        Debug.Log("Destination set to " + destination.position);
    //    }
    //    else
    //    {
    //        Debug.Log("Destination is null");
    //    }
    //}

    public void MoveTo(Vector3 position)
    {
        //agent.destination = position;
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(position);
            //Debug.Log("SetDestination called: " + position);
        }
        else
        {
            //Debug.Log("Agent not on NavMesh, cannot move");
        }
    }
}
