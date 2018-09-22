using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;

    int patrolPointIndex;
    float proximityBeforeChangeDestination = 0.5f;
    NavMeshAgent navMeshAgent;
	private bool displayText = false;
    void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
		navMeshAgent.autoBraking = false;
		navMeshAgent.updatePosition = true;
		navMeshAgent.updateRotation = true;
		SetNextDestination ();
	}

    void Update()
    {

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < proximityBeforeChangeDestination)
        {
            SetNextDestination();
        }
    }

    void SetNextDestination()
    {
        navMeshAgent.SetDestination(patrolPoints[patrolPointIndex].position);
        patrolPointIndex = (patrolPointIndex + 1) % patrolPoints.Length;
    }

    void OnCollisionEnter(Collision c)
    {
        var other = c.gameObject;
        if (other.CompareTag("Player"))
        {
			displayText = true;
			navMeshAgent.isStopped = true;
        }
    }

	void OnGUI() {
		if (displayText)
		GUI.Label(new Rect(200, 200, 100, 100), "You are DETECTED!");
	}

}
