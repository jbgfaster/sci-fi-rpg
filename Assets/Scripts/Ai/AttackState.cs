using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
    private AIAgent localAgent;
    public bool inSight = false;
    Transform currentTarget;
    [SerializeField] private float timer = 0f;


    public AIStateID GetID()
    {
        return AIStateID.Attack;
    }

    public void Enter(AIAgent agent)
    {
        localAgent = agent;
    }
    
    public void Update(AIAgent agent)
    {
        currentTarget =localAgent.currentTarget;
        if (currentTarget == null)
        {
            return;
        }
        Vector3 targetDirection = currentTarget.position-agent.transform.position;
        if (!Physics.Raycast(agent.transform.position+Vector3.up, targetDirection, 10.0f, agent.obstacleMask)) 
        {
            inSight = true;
            Attack();
        }
        else
        {
            inSight = false;
            agent.stateMachine.ChangeState(AIStateID.Chase);
        }
       
    }

    public void Exit(AIAgent agent)
    {
        agent.currentTarget = currentTarget;
    }

    void Attack()
    {
        timer -= Time.deltaTime;
        if (inSight && timer < 0)
        {
            timer = 1.0f;
            localAgent.GetComponent<Skills>().skill1.Action(localAgent.gameObject, currentTarget.gameObject);
        }
    }
}
