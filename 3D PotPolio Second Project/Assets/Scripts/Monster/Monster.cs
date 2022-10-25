using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //몬스터가 해야할 일들
    //1, 일정 구간 순찰
    //2. 순찰 중 일정범위 내 플레이어 존재시 추적
    //3. 너무 먼 거리를 오면 제자리로 돌아감
    //4. 움직임, 공격, 맞음, 죽음 4가지 상태
    //5. 체력 필요 => 체력 체크 용 인터페이스 필요

    NavMeshAgent agent;
    Transform[] patrolPoints;
    bool isStopAgent;

    public delegate void Action(NavMeshAgent agent);
    Action changePatrolAction;
    int destinationIndex = 0;


    float monsterSearchRadius = 5;
    LayerMask playerLayer;
    int tempLayerMask;
    Transform player;

    bool isFindPlayer;

    bool isMonsterChase = false;

    enum MonsterState
    {
        patrol = 0,
        chase,
        attack,
        hit
    }


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerLayer = LayerMask.NameToLayer("Player");
       
    }

    private void Start()
    {
        Transform patrolPoint = GameObject.FindGameObjectWithTag("PatrolPoint").transform.GetComponent<Transform>();

        patrolPoints = new Transform[patrolPoint.childCount];

        for (int i = 0; i < patrolPoint.childCount; i++)
        {
            patrolPoints[i] = patrolPoint.transform.GetChild(i);
        }

        tempLayerMask = (1 << playerLayer);

        agent.SetDestination(patrolPoints[0].transform.position);
        
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            SetPatrol();
        }

        FindPlayer(out isFindPlayer);
        if(isFindPlayer)
        {
            ChasePlayer();
        }


    }

    private void SetPatrol()
    {
        if(!isMonsterChase)
        {
            destinationIndex++;

            destinationIndex %= patrolPoints.Length;

            agent.SetDestination(patrolPoints[destinationIndex].transform.position);


            Debug.Log("setpatrol");
        }
        
    }

    private Transform FindPlayer(out bool outIsFindPlayer)  //ontrigger쓰면 되는건데 연습해보고 싶어 사용함
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, monsterSearchRadius, tempLayerMask);
        //여기서 null값 뜨는 중, tempLayerMask 값은 128, player도 7번쨰 layer
        //player에 컬라이더가 없어 생기던 문제였음
        player = null;
        outIsFindPlayer = false;

        if (colliders != null)
        {

            foreach (Collider collider in colliders)
            {
                player = collider.GetComponent<Transform>();
                outIsFindPlayer = true;
                Debug.Log($"{player.name}");

                
            }
        }


        return player;
    }

    private void ChasePlayer()
    {
         
        if(player != null)
        {
            isMonsterChase = true;
            agent.SetDestination(player.position);
        }
    
    }




}
