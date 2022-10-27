using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    //���Ͱ� �ؾ��� �ϵ�
    //1, ���� ���� ���� => ����
    //2. ���� �� �������� �� �÷��̾� ����� ���� => ����
    //3. �ʹ� �� �Ÿ��� ���� ���ڸ��� ���ư� => ����
    //4. ������, ����, ����, ���� 4���� ����  => ���� ���� 3���� ����
    //5. ü�� �ʿ� => ü�� üũ �� �������̽� �ʿ� => ���� �ؾ���

    NavMeshAgent agent;
    Transform[] patrolPoints;   //��������

    public delegate void Action(NavMeshAgent agent);
    int destinationIndex = 0;


    float monsterSearchRadius = 5;
    LayerMask playerLayer;
    int tempLayerMask;

    //ã���� �� ������ �÷��̾� Ʈ������
    Transform playerTransform = null;

    //�÷��̾� ü�� �� �������� ���� �÷��̾� ��ũ��Ʈ
    Player player;

    //���� ���� üũ��
    bool isMonsterChase = false;
    bool isPatrol = true;
    bool isCombat = false;
    bool isDie = false;

    float hp;
    float maxHP = 100;
    float ratio;

    float attackDelay = 1.5f;
    float criticalRate = 15.0f; // 15�ۼ�Ʈ Ȯ���� ġ��Ÿ

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


    //���� ���� üũ�� enum
    enum MonsterState
    {
        patrol = 0,
        chase,
        combat,
        die
    }

    // enum �ν��Ͻ������ �⺻���� patrol�� ����
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

        tempLayerMask = (1 << playerLayer); //��Ʈ�÷���, 0000 0001 �� playerLayer(7��° ���̾�) ��ŭ �Űܶ� => 0100 0000 �� ��

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
            PatrolUpdate(); // ������ �ϸ� �ֺ��� �÷��̾ ������ polling������� ���üũ
        }
        else if(isMonsterChase)
        {
            ChaseUpdate();  //�÷��̾�� ������ ������ ����, ���߿� �÷��̾ ������� patrol�� ���ư����� ������ ��
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


    private void FindPlayer()  //ontrigger���� �Ǵ°ǵ� �����غ��� �;� �����
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, monsterSearchRadius, tempLayerMask);
        //���⼭ null�� �ߴ� ��, tempLayerMask ���� 128, player�� 7���� layer
        //player�� �ö��̴��� ���� ����� ��������
        

        if (colliders.Length > 0)  //�÷��̾�� �Ѹ���̴� �����ϱ⸸ �ϸ� ã�������� �Ǵ�
        {
            monsterState = MonsterState.chase;
            SetMonsterState(monsterState);
            Debug.Log($"{playerTransform.name}");

        }
    }

    private void ChasePlayer()  //FindPlayer�� ã�� �÷��̾� Ʈ���������� �����ϴ� �Լ�
    {

        if (agent.remainingDistance <= agent.stoppingDistance)  //�÷��̾ �����ϸ� �����¼��� ��ȯ
        {
            monsterState = MonsterState.combat;
            SetMonsterState(monsterState);
        }
        else if(agent.remainingDistance > 5)    //�ʹ� �־����� �ٽ� ����
        {
            monsterState = MonsterState.patrol;
            SetMonsterState(monsterState);
            agent.SetDestination(patrolPoints[destinationIndex].transform.position);
            //���¹ٲ� ������ �缳��
        }
        else
        {
            agent.SetDestination(playerTransform.position);
            //�̵��ϴ� �÷��̾� ��ġ ������ ���� ������ �� ����
        }


    }

    private void CombatPlayer()
    {
        //agent.remainingDistance��ٰ� ��� üũ�Ϸ��� setDestination�� ��� �ؾߵż� ������
        if ((transform.position - playerTransform.position).sqrMagnitude < 2.5f * 2.5f) 
        {
            if (player.HP > 0)
            {
                Debug.Log("������");
                MonsterAttack();
            }
            else
            {
                monsterState = MonsterState.patrol;
                SetMonsterState(monsterState);
                Debug.Log("���� �¸�");
            }
        }
        else  //�ʹ� �־����� �ٽ� �÷��̾� ����
        {
            monsterState = MonsterState.chase;
            SetMonsterState(monsterState);
            agent.SetDestination(playerTransform.position);
            //���¹ٲ� ������ �缳��
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
        //ü�� �����, ü���� 0�� �Ǹ� ����, ������ �״� �ִϸ��̼� �����ϰ� �ִϸ��̼� ������ ���� Destroy 
    }

    private void SetMonsterState(MonsterState mon)  //�÷��̾� ���� �������ִ� �Լ�
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
        SetPatrol();    // ���� ��Ű��
        FindPlayer(); // �÷��̾� ã��, ����� ã�� �÷��̾�
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
