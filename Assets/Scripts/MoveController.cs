using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveController : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator animator;
    [SerializeField] float speed=1;
    [SerializeField] float speedRun = 3;
    [SerializeField] bool isRun= false;

    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;        
    }

    void Update()
    { 
        animator.SetFloat("speed", navAgent.velocity.magnitude);

    }

    public void MoveTo(Vector3 newPosition)
    {
        navAgent.destination = newPosition;
    }

    public void RunSwitch()
    {
        isRun = !isRun;
        if (isRun)
        {
            navAgent.speed = speedRun;
        }
        else
        {
            navAgent.speed = speed;
        }
    }

}
