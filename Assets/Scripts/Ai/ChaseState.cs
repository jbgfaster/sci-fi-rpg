using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : AIState
{
    private AIAgent localAgent;
    [SerializeField] private float timer = 0f;


    public AIStateID GetID()
    {
        return AIStateID.Chase;
    }

    public void Enter(AIAgent agent)
    {
        localAgent = agent;
    }
    
    public void Update(AIAgent agent)
    {
        
        if (localAgent.currentTarget == null)
        {
            return;
        }
        Vector3 targetDirection = localAgent.currentTarget.position - agent.transform.position;  
        if (!Physics.Raycast(agent.transform.position+Vector3.up, targetDirection, 10.0f, agent.obstacleMask))
        {
            StopMoving();
            agent.stateMachine.ChangeState(AIStateID.Attack);
        }
        else
        {
            Chase();
        }        
    }

    public void Exit(AIAgent agent)
    {
        
    }

    void Chase()
    {
        localAgent.GetComponent<AIAgent>().navAgent.destination = localAgent.currentTarget.transform.position;        
    }
    void StopMoving()
    {
        localAgent.GetComponent<AIAgent>().navAgent.ResetPath();
    }
}
