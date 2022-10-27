using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    //몬스터가 해야할 일들
    //1, 일정 구간 순찰 => 구현
    //2. 순찰 중 일정범위 내 플레이어 존재시 추적 => 구현
    //3. 너무 먼 거리를 오면 제자리로 돌아감 => 구현
    //4. 움직임, 공격, 맞음, 죽음 4가지 상태  => 죽음 제외 3가지 구현
    //5. 체력 필요 => 체력 체크 용 인터페이스 필요 => 구현 해야함

    NavMeshAgent agent;
    Transform[] patrolPoints;   //순찰지점

    public delegate void Action(NavMeshAgent agent);
    int destinationIndex = 0;


    float monsterSearchRadius = 5;
    LayerMask playerLayer;
    int tempLayerMask;

    //찾았을 때 추적할 플레이어 트랜스폼
    Transform playerTransform = null;

    //플레이어 체력 등 가져오기 위한 플레이어 스크립트
    Player player;

    //몬스터 상태 체크용
    bool isMonsterChase = false;
    bool isPatrol = true;
    bool isCombat = false;
    bool isDie = false;

    float hp;
    float maxHP = 100;
    float ratio;

    float attackDelay = 1.5f;
    float criticalRate = 15.0f; // 15퍼센트 확률로 치명타

    bool isAttackContinue = false;

    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float MaxHP
    {
        get { return maxHP; }
    }

    Slider hpSlider;

    Animator anim;


    //몬스터 상태 체크용 enum
    enum MonsterState
    {
        patrol = 0,
        chase,
        combat,
        die
    }

    // enum 인스턴스만들고 기본값을 patrol로 설정
    MonsterState monsterState = MonsterState.patrol;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerLayer = LayerMask.NameToLayer("Player");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();
        hpSlider = GetComponentInChildren<Slider>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Transform patrolPoint = GameObject.FindGameObjectWithTag("PatrolPoint").transform.GetComponent<Transform>();

        patrolPoints = new Transform[patrolPoint.childCount];

        for (int i = 0; i < patrolPoint.childCount; i++)
        {
            patrolPoints[i] = patrolPoint.transform.GetChild(i);
        }

        tempLayerMask = (1 << playerLayer); //비트플래그, 0000 0001 을 playerLayer(7번째 레이어) 만큼 옮겨라 => 0100 0000 이 됨

        hp = maxHP;

        ratio = hp / maxHP;

        hpSlider.value = ratio;

        anim.SetBool("isPatrol", true);

        agent.SetDestination(patrolPoints[0].transform.position);

        SetMonsterState(monsterState);

    }

    private void Update()
    {
        

        if(isPatrol)
        {
            PatrolUpdate(); // 순찰을 하며 주변에 플레이어가 없는지 polling방식으로 계속체크
        }
        else if(isMonsterChase)
        {
            ChaseUpdate();  //플레이어에게 도착할 때까지 추적, 도중에 플레이어가 사라지면 patrol로 돌아가도록 만들어야 됨
        }
        else if(isCombat)
        {
            CombatUpdate(); 
        }
        else if(isDie)
        {
            DieUpdate();
        }

    }


    private void SetPatrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            
            destinationIndex++;
            destinationIndex %= patrolPoints.Length;
            agent.SetDestination(patrolPoints[destinationIndex].transform.position);

            Debug.Log("setpatrol");
        }
        
    }


    private void FindPlayer()  //ontrigger쓰면 되는건데 연습해보고 싶어 사용함
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, monsterSearchRadius, tempLayerMask);
        //여기서 null값 뜨는 중, tempLayerMask 값은 128, player도 7번쨰 layer
        //player에 컬라이더가 없어 생기던 문제였음
        

        if (colliders.Length > 0)  //플레이어는 한명뿐이니 존재하기만 하면 찾은것으로 판단
        {
            monsterState = MonsterState.chase;
            SetMonsterState(monsterState);
            Debug.Log($"{playerTransform.name}");

        }
    }

    private void ChasePlayer()  //FindPlayer가 찾은 플레이어 트랜스폼으로 추적하는 함수
    {

        if (agent.remainingDistance <= agent.stoppingDistance)  //플레이어에 도착하면 공격태세로 전환
        {
            monsterState = MonsterState.combat;
            SetMonsterState(monsterState);
        }
        else if(agent.remainingDistance > 5)    //너무 멀어지면 다시 순찰
        {
            monsterState = MonsterState.patrol;
            SetMonsterState(monsterState);
            agent.SetDestination(patrolPoints[destinationIndex].transform.position);
            //상태바뀔때 목적지 재설정
        }
        else
        {
            agent.SetDestination(playerTransform.position);
            //이동하는 플레이어 위치 갱신을 위해 시작할 때 실행
        }


    }

    private void CombatPlayer()
    {
        //agent.remainingDistance썼다가 계속 체크하려면 setDestination을 계속 해야돼서 포기함
        if ((transform.position - playerTransform.position).sqrMagnitude < 2.5f * 2.5f) 
        {
            if (player.HP > 0)
            {
                Debug.Log("전투중");
                MonsterAttack();
            }
            else
            {
                monsterState = MonsterState.patrol;
                SetMonsterState(monsterState);
                Debug.Log("몬스터 승리");
            }
        }
        else  //너무 멀어지면 다시 플레이어 추적
        {
            monsterState = MonsterState.chase;
            SetMonsterState(monsterState);
            agent.SetDestination(playerTransform.position);
            //상태바뀔때 목적지 재설정
        }
    }

    private void MonsterAttack()
    {
        if(!isAttackContinue)
        {
            isAttackContinue = true;
            StartCoroutine(MonsterAttackCoroutine(attackDelay));
        }
    }

    IEnumerator MonsterAttackCoroutine(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        anim.SetTrigger("OnAttack");
        isCriticalAttack(criticalRate);
        isAttackContinue = false;
    }

    private void isCriticalAttack(float criticalPercent)
    {
        float criticalAttack;
        criticalAttack = Random.Range(0, 100.0f);
        if(criticalAttack < criticalPercent)
        {
            anim.SetTrigger("OnCritical");
        }
    }

    private void Die()
    {
        //체력 만들고, 체력이 0이 되면 죽음, 죽으면 죽는 애니메이션 실행하고 애니메이션 종료후 몬스터 Destroy 
    }

    private void SetMonsterState(MonsterState mon)  //플레이어 상태 세팅해주는 함수
    {

        switch (mon)
        {
            case MonsterState.patrol:
                isPatrol = true;
                isMonsterChase = false;
                isCombat = false;
                isDie = false;
                anim.SetBool("isPatrol", true);
                anim.SetBool("isChase", false);
                anim.SetBool("isCombat", false);
                break;
            case MonsterState.chase:
                isPatrol = false;
                isMonsterChase = true;
                isCombat = false;
                isDie = false;
                anim.SetBool("isPatrol", false);
                anim.SetBool("isChase", true);
                anim.SetBool("isCombat", false);
                break;
            case MonsterState.combat:
                isPatrol = false;
                isMonsterChase = false;
                isCombat = true;
                isDie = false;
                anim.SetBool("isPatrol", false);
                anim.SetBool("isChase", false);
                anim.SetBool("isCombat", true);
                break;
            case MonsterState.die:
                isPatrol = false;
                isMonsterChase = false;
                isCombat = false;
                isDie = true;
                anim.SetBool("isDie", true);

                break;

            default:
                break;
        }
    }

    private void PatrolUpdate()
    {
        SetPatrol();    // 순찰 시키기
        FindPlayer(); // 플레이어 찾기, 참고로 찾은 플레이어
    }

    private void ChaseUpdate()
    {
        ChasePlayer();
    }

    private void CombatUpdate()
    {
        CombatPlayer();   
    }
    private void DieUpdate()
    {
        Die();
    }




}
