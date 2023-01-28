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
        Vector3 targetDirection = currentTarget.position - agent.transform.position;//получаем направление к цели
        if (!Physics.Raycast(agent.transform.position, targetDirection, 10.0f, agent.obstacleMask)) // && (targetDirection.magnitude <= weapon.shootRange))//проверка на наличие obstacle(препятствий) между солдатом и целью
        {
            inSight = true;//если между ними нет стены и других препятствий, то солдат видит противника
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
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tagOpponent);//помещаем туда цели с тэгом врага
        if (targets.Length < 1)
        {
            return null;
        }
        float nearTarget = 9999;//переменная для наименьшего расстояния, по умолчанию первый объект в массиве целей
        int index = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            float targetDistance = (targets[i].transform.position - soldier.transform.position).magnitude;//переменная проверяет дистанцию к каждой цели в массиве
            if (targetDistance < nearTarget)//если дистанция меньше чем предыдущая, то возвращает ее
            {
                nearTarget = targetDistance;
                index = i;
            }
        }
        return targets[index].transform;
    }
}
