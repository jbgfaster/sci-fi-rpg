using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
    private GameObject localAgent;
    public bool inSight = false;
    Transform currentTarget;
    [SerializeField] private float timer = 0f;


    public AIStateID GetID()
    {
        return AIStateID.Attack;
    }

    public void Enter(AIAgent agent)
    {
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
            localAgent.GetComponent<Skills>().skill1.Action(localAgent, currentTarget.gameObject);
        }
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
