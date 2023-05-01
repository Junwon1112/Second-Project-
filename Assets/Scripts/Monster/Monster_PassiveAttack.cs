using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Monster_PassiveAttack : Monster_Basic
{
    //���Ͱ� �ؾ��� �ϵ�
    //1, ���� ���� ���� => ����
    //2. ���� �� �������� �� �÷��̾� ����� ���� => ����
    //3. �ʹ� �� �Ÿ��� ���� ���ڸ��� ���ư� => ����
    //4. ������, ����, ����, ���� 4���� ����  => ���� ���� 3���� ����
    //5. ü�� �ʿ� => ü�� üũ �� �������̽� �ʿ� => ���� �ؾ���

    NavMeshAgent agent;

    /// <summary>
    /// ���������� ��ġ
    /// </summary>
    Transform[] patrolPoints;

    public delegate void Action(NavMeshAgent agent);
    int destinationIndex = 0;

    float initialMonsterSpeed = 3.5f;
    float moveSpeed;

    float monsterChaseRadius = 20.0f;
    LayerMask playerLayer;
    int tempLayerMask;

    /// <summary>
    /// ã���� �� ������ �÷��̾� Ʈ������
    /// </summary>
    Transform playerTransform = null;

    /// <summary>
    /// �÷��̾� ü�� �� �������� ���� �÷��̾� ��ũ��Ʈ
    /// </summary>
    Player player;

    [SerializeField]
    MonsterData monsterData;

    /// <summary>
    /// ���� ���� üũ��
    /// </summary>
    bool isMonsterChase = false;
    bool isPatrol = true;
    bool isCombat = false;
    bool isDie = false;
    bool isAttacked = false;

    float hp;
    float maxHP = 100;
    float ratio;

    Slider hpSlider;

    Animator anim;

    public float giveExp = 20.0f;


    float attackDamage = 30;

    float defence = 3;

    float attackDelay = 1.5f;
    float criticalRate = 15.0f; // 15�ۼ�Ʈ Ȯ���� ġ��Ÿ

    bool isAttackContinue = false;
    public bool playerTriggerOff = false;

    /// <summary>
    /// enum �ν��Ͻ������ �⺻���� patrol�� ����
    /// </summary>
    MonsterState monsterState = MonsterState.patrol;

    public override Transform CharacterTransform
    {
        get { return this.transform; }
    }

    /// <summary>
    /// ���ݷ°� ���õ� ������Ƽ
    /// </summary>
    public override float AttackDamage
    {
        get
        {
            return attackDamage;
        }
        set
        {
            attackDamage = value;
        }
    }
    /// <summary>
    /// ���°� ���õ� ������Ƽ
    /// </summary>
    public override float Defence
    {
        get { return defence; }
        set { defence = value; }
    }

    /// <summary>
    /// ü�°� ���õ� ������Ƽ, 0�̵� ��� ������ ���
    /// </summary>
    public override float HP
    {
        get { return hp; }
        set
        {
            if(hp > value)
            {
                isAttacked = true;
            }
            hp = value;

            if (hp <= 0 && !isDie)
            {
                anim.SetBool("isDie", true);
                SetMonsterState(MonsterState.die);
                agent.enabled = false;
                DropItem();
                Destroy(transform.parent.gameObject, 3.0f);
            }
        }
    }
    /// <summary>
    /// �ִ�ü�¿� ���� ������Ƽ
    /// </summary>
    public override float MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    public override float GiveExp
    {
        get { return giveExp; }
        set { giveExp = value; }
    }
    public override float AttackDelay
    {
        get { return attackDelay; }
        set { attackDelay = value; }
    }
    public override float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerLayer = LayerMask.NameToLayer("Player");
        hpSlider = GetComponentInChildren<Slider>();
        anim = GetComponent<Animator>();

        Initialize();
    }

    private void Initialize()
    {
        MoveSpeed = monsterData.moveSpeed;
        AttackDamage = monsterData.attackDamage;
        Defence = monsterData.defence;
        AttackDelay = monsterData.attackDelay;
        GiveExp = monsterData.exp;
        MaxHP = monsterData.maxHP;
    }

    private void Start()
    {
        playerTransform = GameManager.Instance.MainPlayer.transform;
        player = GameManager.Instance.MainPlayer;

        Transform patrolPoint = transform.parent.GetChild(1);

        patrolPoints = new Transform[patrolPoint.childCount];

        for (int i = 0; i < patrolPoint.childCount; i++)
        {
            patrolPoints[i] = patrolPoint.transform.GetChild(i);
        }
        agent.speed = MoveSpeed;

        //��Ʈ�÷���, 0000 0001 �� playerLayer(7��° ���̾�) ��ŭ �Űܶ� => 0100 0000 �� ��, �÷��̾� ã�� �� �÷��̾� ���̾�� ������ Ȱ���ϱ� ���� ���� 
        tempLayerMask = (1 << playerLayer);

        HP = MaxHP;

        SetHP();

        anim.SetBool("isPatrol", true);

        agent.SetDestination(patrolPoints[0].transform.position);

        SetMonsterState(monsterState);

    }

    private void Update()
    {
        LookingCameraHPBar();

        if (isPatrol)
        {
            PatrolUpdate(); // ������ �ϸ� �ֺ��� �÷��̾ ������ polling������� ���üũ
        }
        else if (isMonsterChase)
        {
            ChaseUpdate();  //�÷��̾�� ������ ������ ����, ���߿� �÷��̾ ������� patrol�� ���ư����� ������ ��
        }
        else if (isCombat)
        {
            CombatUpdate();
        }
    }




    /// <summary>
    /// ������ ���� ����Ǵ� �޼��� 
    /// </summary>
    private void SetPatrol()
    {
        //0�� ��ΰ� �����Ǳ� �ϴµ� �׵� remainingDistance�� 0���� �����ż� �ٷ� 1�� ��η� �Ѿ ����,
        //��� �ð��� �ʿ��� �׷� ��, �����δ� update���� �������� �ڷ�ƾ���� �ð� �˳��ϰ� ������ �̹��� �׳� �����Ÿ� 0�̸� ��� �� �ϵ��� ����  
        if (agent.remainingDistance <= agent.stoppingDistance && agent.remainingDistance != 0)
        {

            destinationIndex++;
            destinationIndex %= patrolPoints.Length;
            agent.SetDestination(patrolPoints[destinationIndex].transform.position);
        }

    }

    /// <summary>
    /// �ֺ��� �÷��̾ �ִ��� ã�� �޼���
    /// </summary>
    private void FindPlayer()
    {
        if(isAttacked)  //�÷��̾�� �Ѹ���̴� �ǰ� �پ��⸸ �ϸ� ã�������� �Ǵ� (isAttack�� �ٲٴ°� hp������Ƽ)
        {
            monsterState = MonsterState.chase;
            SetMonsterState(monsterState);
            agent.SetDestination(playerTransform.position);
        }
    }

    /// <summary>
    /// ã�� �÷��̾ ���� �� �����ϴ� �޼���
    /// </summary>
    private void ChasePlayer()  //FindPlayer�� ã�� �÷��̾� Ʈ���������� �����ϴ� �Լ�
    {

        if (agent.remainingDistance <= agent.stoppingDistance)  //�÷��̾ �����ϸ� �����¼��� ��ȯ
        {
            monsterState = MonsterState.combat;
            SetMonsterState(monsterState);
        }
        else if (agent.remainingDistance > monsterChaseRadius)    //�ʹ� �־����� �ٽ� ����
        {
            isAttacked = false;
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

    /// <summary>
    /// ���� ���� �Ÿ��� �÷��̾ ���� �� ������ �����ϴ� �޼���
    /// </summary>
    private void CombatPlayer()
    {
        //agent.remainingDistance��ٰ� ��� üũ�Ϸ��� setDestination�� ��� �ؾߵż� ������
        if ((transform.position - playerTransform.position).sqrMagnitude < 2.5f * 2.5f)
        {
            if (player.HP > 0)
            {
                //transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.LookRotation(playerTransform.position), 0.1f) * Quaternion.Euler(0,180, 0);
                transform.LookAt(Vector3.Lerp(transform.position, playerTransform.position, 0.1f));
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

    /// <summary>
    /// ������ ���� �� ���� �ֱ�� ������ �����ϴ� �޼���
    /// </summary>
    private void MonsterAttack()
    {
        if (!isAttackContinue)   //������Ʈ���� ������ ��������ʵ���
        {
            isAttackContinue = true;
            StartCoroutine(MonsterAttackCoroutine(attackDelay));
        }
    }

    /// <summary>
    /// ���ݼӵ��� ���� �����ֱⰡ �ٲ�� ���� ����� ����Ǵ� �͵鿡 ���� �޼���
    /// </summary>
    /// <param name="attackSpeed"></param>
    /// <returns></returns>
    IEnumerator MonsterAttackCoroutine(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        //monsterCollider.isTrigger = true; => �ִϸ��̼����� ����
        anim.SetTrigger("OnAttack");
        isCriticalAttack(criticalRate);
        isAttackContinue = false;
    }

    /// <summary>
    /// ġ��Ÿ ������ �߻��ϴ��� Ȯ���ϴ� �޼���
    /// </summary>
    /// <param name="criticalPercent"></param>
    private void isCriticalAttack(float criticalPercent)
    {
        float criticalAttack;
        criticalAttack = Random.Range(0, 100.0f);
        if (criticalAttack < criticalPercent)
        {
            anim.SetTrigger("OnCritical");
        }
    }

    /// <summary>
    /// ������ AI���¸� ��Ȳ�� ���� �ٲٴ� �Լ�
    /// </summary>
    /// <param name="mon"></param>
    public override void SetMonsterState(MonsterState mon)  //�÷��̾� ���� �������ִ� �Լ�
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

    /// <summary>
    /// ������Ʈ���� ������ �� ����� �Լ���
    /// </summary>
    private void PatrolUpdate()
    {
        SetPatrol();    // ���� ��Ű��
        FindPlayer(); // �÷��̾� ã��, ����� ã�� �÷��̾�
    }

    /// <summary>
    /// ������Ʈ���� ������ �� ����� �Լ���
    /// </summary>
    private void ChaseUpdate()
    {
        ChasePlayer();
    }

    /// <summary>
    /// ������Ʈ���� ������ �� ����� �Լ���
    /// </summary>
    private void CombatUpdate()
    {
        CombatPlayer();
    }
    //private void DieUpdate()
    //{
    //    Die();
    //}

    public override void MoveSlow(float slowRate, float time)
    {
        StartCoroutine(CoMoveSlow(slowRate, time));
    }

    IEnumerator CoMoveSlow(float slowRate, float time)
    {
        MoveSpeed *= slowRate;
        agent.speed = MoveSpeed;
        yield return new WaitForSeconds(time);
        MoveSpeed = initialMonsterSpeed;
        agent.speed = MoveSpeed;
    }

    /// <summary>
    /// ü�� ����ϴ� �޼���
    /// </summary>
    public override void SetHP()
    {
        hpSlider.value = HP / MaxHP;
    }

    /// <summary>
    /// Hp�ٰ� �׻� �÷��̾ ������ �ϴ� �Լ�
    /// </summary>
    private void LookingCameraHPBar()
    {
        hpSlider.transform.LookAt(Camera.main.transform.position);
    }

    /// <summary>
    /// �������� ����� �� ���Ǵ� �޼���
    /// </summary>
    private void DropItem()
    {
        ItemFactory.MakeItem(ItemIDCode.HP_Potion, transform.position, Quaternion.identity);
    }
}
