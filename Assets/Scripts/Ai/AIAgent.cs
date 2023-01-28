using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent navAgent;
    public AIStateMachine stateMachine;
    public AIStateID initialState;
    public Transform currentTarget;
    public Transform lastSeenPosition;
    public bool inSight = false;
    public GameObject[] waypoints;
    public LayerMask obstacleMask;
    public string tagOpponent = "Player";
    public AIStateID currentStateToShow;

    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AIStateMachine(this);  

        stateMachine.RegisterState(new AttackState());
        stateMachine.RegisterState(new ChaseState());


        stateMachine.ChangeState(initialState);  
    }
    
    void Update()
    {
        currentTarget=GetTarget(gameObject,tagOpponent);
        stateMachine.Update();
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
