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
    Transform player = null;

    bool isFindPlayer;

    bool isMonsterChase = false;
    bool isPatrol = true;
    bool isCombat = false;

    enum MonsterState
    {
        patrol = 0,
        chase,
        combat,
    }

    MonsterState monsterState = MonsterState.patrol;


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
        SetMonsterState(monsterState);

        if(isPatrol)
        {
            PatrolUpdate(); 
        }
        else if(isMonsterChase)
        {
            ChaseUpdate();
        }
        else
        {
            AttackUpdate();
        }

        
        if(isFindPlayer)
        {
            
        }
        else
        {
            monsterState = MonsterState.patrol;
            SetMonsterState(monsterState);
        }


    }


    private void SetPatrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && isPatrol)
        {
            
            destinationIndex++;
            destinationIndex %= patrolPoints.Length;
            agent.SetDestination(patrolPoints[destinationIndex].transform.position);

            Debug.Log("setpatrol");
        }
        else if(agent.remainingDistance > agent.stoppingDistance && isPatrol)
        {
            agent.SetDestination(patrolPoints[destinationIndex].transform.position);
        }
    }


    private Transform FindPlayer(bool findPlayer)  //ontrigger쓰면 되는건데 연습해보고 싶어 사용함
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, monsterSearchRadius, tempLayerMask);
        //여기서 null값 뜨는 중, tempLayerMask 값은 128, player도 7번쨰 layer
        //player에 컬라이더가 없어 생기던 문제였음
        findPlayer = false;

        if (colliders != null)
        {
            foreach (Collider collider in colliders)
            {
                player = collider.GetComponent<Transform>();
                findPlayer = true;
                monsterState = MonsterState.chase;
                SetMonsterState(monsterState);
                Debug.Log($"{player.name}");
                
            }
        }

        


        return player;
    }

    private void ChasePlayer()
    {
        if(player != null)
        {
            
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                monsterState = MonsterState.combat;
                SetMonsterState(monsterState);
            }
            else
            {
                agent.SetDestination(player.position);
            }
            
                
        }
    
    }

    private void SetMonsterState(MonsterState mon)
    {

        switch (mon)
        {
            case MonsterState.patrol:
                isPatrol = true;
                isMonsterChase = false;
                isCombat = false;
                break;
            case MonsterState.chase:
                isPatrol = false;
                isMonsterChase = true;
                isCombat = false;
                break;
            case MonsterState.combat:
                isPatrol = false;
                isMonsterChase = false;
                isCombat = true;
                break;
            default:
                break;
        }
    }

    private void PatrolUpdate()
    {
        SetPatrol();
        FindPlayer(isFindPlayer); // 참고로 찾은 플레이어 트랜스폼 리턴함
    }

    private void ChaseUpdate()
    {
        ChasePlayer();
    }

    private void AttackUpdate()
    {
        Debug.Log("전투중");
    }




}
