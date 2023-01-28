using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public Animator animator;
   // public PlayerStats playerStats;//параметры солдата
  //  public IKWeapon weapon;//Инверсная кинематика и параметры оружия
    public NavMeshAgent navAgent;
    public AIStateMachine stateMachine;//Стэйт машин для АИ
    public AIStateID initialState;//первоначальный стэйт при запуске игры
  //  public AIAgentConfig config;
    public Transform currentTarget;//текущая цель
    public Transform lastSeenPosition;//последняя позиция текущей цели, которую видел солдат
    public bool inSight = false;//враг в поле зрения
    public GameObject[] waypoints;//точки на карте, куда будут идти солдаты в стэйте Patrol
    public LayerMask obstacleMask;//в инспекторе для всех статичных объектов нужно пометить layer как obstacle, чтобы не видел противника сквозь стены
    [HideInInspector]
    
    public string tagOpponent = "Enemy";//тэг врага для текущего солдата(по умолчанию enemy, выбирать в инспекторе)
    public AIStateID currentStateToShow;
    void Start()
    {
       // playerStats = gameObject.GetComponent<PlayerStats>();
      //  navMeshAgent.speed = playerStats.speed;
        //navMeshAgent.stoppingDistance = config.maxSightDistance;
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
