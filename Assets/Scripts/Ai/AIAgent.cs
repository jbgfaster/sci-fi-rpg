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
    [HideInInspector]
    
    public string tagOpponent = "Enemy";
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
        stateMachine.Update();
    }
}
