using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //���Ͱ� �ؾ��� �ϵ�
    //1, ���� ���� ����
    //2. ���� �� �������� �� �÷��̾� ����� ����
    //3. �ʹ� �� �Ÿ��� ���� ���ڸ��� ���ư�
    //4. ������, ����, ����, ���� 4���� ����
    //5. ü�� �ʿ� => ü�� üũ �� �������̽� �ʿ�

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


    private Transform FindPlayer(bool findPlayer)  //ontrigger���� �Ǵ°ǵ� �����غ��� �;� �����
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, monsterSearchRadius, tempLayerMask);
        //���⼭ null�� �ߴ� ��, tempLayerMask ���� 128, player�� 7���� layer
        //player�� �ö��̴��� ���� ����� ��������
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
        FindPlayer(isFindPlayer); // ����� ã�� �÷��̾� Ʈ������ ������
    }

    private void ChaseUpdate()
    {
        ChasePlayer();
    }

    private void AttackUpdate()
    {
        Debug.Log("������");
    }




}
