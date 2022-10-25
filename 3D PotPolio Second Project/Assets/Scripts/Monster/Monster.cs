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

    private Transform FindPlayer(out bool outIsFindPlayer)  //ontrigger���� �Ǵ°ǵ� �����غ��� �;� �����
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, monsterSearchRadius, tempLayerMask);
        //���⼭ null�� �ߴ� ��, tempLayerMask ���� 128, player�� 7���� layer
        //player�� �ö��̴��� ���� ����� ��������
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
