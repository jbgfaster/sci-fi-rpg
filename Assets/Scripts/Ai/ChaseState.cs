using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : AIState
{
    private GameObject localAgent;
    public bool inSight = false;
    Transform currentTarget;
    [SerializeField] private float timer = 0f;


    public AIStateID GetID()
    {
        return AIStateID.Chase;
    }

    public void Enter(AIAgent agent)
    {
        currentTarget = agent.currentTarget;
        localAgent = agent.gameObject;
    }
    public void Update(AIAgent agent)
    {
        currentTarget = GetTarget(agent.gameObject, "Player");
        if (currentTarget == null)
        {
            return;
        }
        Vector3 targetDirection = currentTarget.position - agent.transform.position;  
        if (!Physics.Raycast(agent.transform.position, targetDirection, 10.0f, agent.obstacleMask))
        {
            inSight = true;
            StopMoving();
            agent.stateMachine.ChangeState(AIStateID.Attack);
        }
        else
        {
            inSight = false;            
            Chase();
        }        
    }

    public void Exit(AIAgent agent)
    {
        
    }

    void Chase()
    {
        localAgent.GetComponent<AIAgent>().navAgent.destination = currentTarget.transform.position;        
    }
    void StopMoving()
    {
        localAgent.GetComponent<AIAgent>().navAgent.ResetPath();
    }


    public Transform GetTarget(GameObject soldier, string tagOpponent)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tagOpponent);
        if (targets.Length < 1)
        {
            return null;
        }
        float nearTarget = 9999;    
        int index = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            float targetDistance = (targets[i].transform.position - soldier.transform.position).magnitude; 
            if (targetDistance < nearTarget)
            {
                nearTarget = targetDistance;
                index = i;
            }
        }
        return targets[index].transform;
    }
}
