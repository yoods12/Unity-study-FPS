using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int nextIdx;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        var group = GameObject.Find("WayPoints");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints); // 자신포함해서 가져옴
            wayPoints.RemoveAt(0); // 자신은 필요없기 때문에 0번 삭제
        }
        MoveWayPoint();
    }
    void MoveWayPoint()
    {
        if (agent.isPathStale)
            return;
        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            nextIdx++;
            nextIdx = nextIdx % wayPoints.Count;
            MoveWayPoint();
        }
    }
}
