using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, IHealth
{
    Player player;
    MainCamera_PlayerPos mainCamera_PlayerPos;

    [SerializeField]
    GameObject witchAttackPrefab;

    JobType job;
    public JobType Job { get; set; }
    [SerializeField]
    ScriptableObj_JobData jobData;

    /// <summary>
    /// 움직임을 위한 인풋 시스템용
    /// </summary>
    public PlayerInput input;

    /// <summary>
    /// 죽었을 때 리지드바디 변화용
    /// </summary>
    Rigidbody rigid;

    /// <summary>
    /// 공격 애니메이션 실행시 공격 전 위치를 저장하는 역할
    /// </summary>
    Vector3 attackPosition_Dir;
    Vector3 attackPosition;
    Quaternion attackRotation;


    /// <summary>
    /// 이동 방향 받고 리턴용
    /// </summary>
    Vector3 dir = Vector3.zero;
    /// <summary>
    /// 이동 중 다른 행위를 한뒤 다시 이동할 때 사용하기 위해 중간 버퍼용
    /// </summary>
    Vector3 restartDir = Vector3.zero;

    //float walkSoundVolume = 1.0f;
    //float AttackSoundVolume = 0.7f;

    /// <summary>
    /// 애니메이션 용 
    /// </summary>
    Animator anim;

    /// <summary>
    /// 체력 관련 변수들
    /// </summary>
    float hp = 100;
    float maxHp = 100;
    bool isHPImageAlpha1 = true;
    Slider hpBar;
    Image hpFillImage;
    TextMeshProUGUI hpValue_Text;

    /// <summary>
    /// 경험치 관련 변수들
    /// </summary>
    float exp = 0.0f;
    float maxExp = 100;
    Slider expBar;
    Image expFillImage;
    TextMeshProUGUI expValue_Text;

    
    public int level = 1;
    TextMeshProUGUI lvText;

    /// <summary>
    /// 스킬에 사용할 스킬포인트, 레벨업시 증가
    /// </summary>
    uint skillPoint = 1;

    /// <summary>
    /// 레벨업하면 레벨업 함수와 스킬UI에서 스킬포인트를 플레이어 스킬포인트와 동기화 해주는 함수 등록
    /// </summary>
    public delegate void del_LevelUP();
    public del_LevelUP newDel_LevelUp;

    SkillUI skillUI;



    /// <summary>
    /// 회전 관련 변수들
    /// </summary>
    float turnToX;
    float turnToY;
    float turnToZ;

    float turnSpeed = 20.0f;

    /// <summary>
    /// 아이템 관련 변수
    /// </summary>
    Item item;
    ItemFactory itemFactory;
    ItemIDCode itemID;
    ItemData_Potion potion;
    public ItemData_Weapon myWeapon;

    float findItemRange = 3.0f;
    Inventory playerInventory;
    InventoryUI playerInventoryUI;

    /// <summary>
    /// 무기 바꿀때 사용하기 위한 변수들
    /// </summary>
    GameObject weaponPrefab;
    CapsuleCollider weaponCollider;
    public Transform weaponHandTransform;
    public bool isFindWeapon = false;

    /// <summary>
    /// 스킬 사용 중인지 체크하기 위해 사용
    /// </summary>
    public bool isSkillUsing = false;

    SkillUse[] skillUses;
    Skill_Implement skill_Implement;

    bool isDie = false;
    bool isAttack = false;
    bool isMove = false;
    bool isKeepMoving = false;

    int firstSceneIndex = 4;
    int currentSceneIndex;
    public Transform CharacterTransform
    {
        get { return this.transform; }
    }

    public float HP
    {
        get { return hp; }
        set 
        {
            //hp = value;

            if (!isDie)
            {
                if(value > MaxHP)
                {
                    hp = MaxHP;
                    SetHP();
                }
                else if (value <= 0)
                {
                    hp = 0;
                    SetHP();
                    Die();
                }
                else if (value / MaxHP < 0.5f)
                {
                    hp = value;
                    SetHP();
                    HPBlink();

                }
                else 
                {
                    hp = value;
                    SetHP();
                }
            }
        }
    }

    public float MaxHP
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    public float Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            SetExp();
            if (player.Exp >= player.MaxExp)
            {
                player.newDel_LevelUp();    //레벨업 델리게이트
            }
        }
    }

    public float MaxExp
    {
        get { return maxExp; }
        set
        {
            maxExp = value;
        }
    }

    public uint SkillPoint
    {
        get { return skillPoint; }
        set { skillPoint = value; }
    }

    /// <summary>
    /// private여도 유니티에서 수치바꿀수 있게 해주는 것
    /// </summary>
    [SerializeField]    
    float attackDamage = 10;

    [SerializeField]
    float defence = 5;

    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }
    public float Defence
    {
        get { return defence; }
        set { defence = value; }
    }
    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    { 
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        hpBar = FindObjectOfType<FindPlayerInfoUI>().transform.GetChild(0).GetComponent<Slider>();
        hpFillImage = hpBar.transform.GetChild(1).GetComponentInChildren<Image>();
        hpValue_Text = FindObjectOfType<FindPlayerInfoUI>().transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        expBar = FindObjectOfType<FindPlayerInfoUI>().transform.GetChild(1).GetComponent<Slider>();
        expFillImage = expBar.transform.GetChild(1).GetComponentInChildren<Image>();
        expValue_Text = FindObjectOfType<FindPlayerInfoUI>().transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        lvText = GameObject.Find("Level_num").GetComponent<TextMeshProUGUI>();

        player = GetComponent<Player>();
        playerInventory = GetComponentInChildren<Inventory>();
        playerInventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
        weaponHandTransform = transform.GetComponentInChildren<FindWeaponHand>().transform;
        mainCamera_PlayerPos = FindObjectOfType<MainCamera_PlayerPos>();

        skillUses = FindObjectsOfType<SkillUse>();
        skill_Implement = FindObjectOfType<Skill_Implement>();
        skillUI = FindObjectOfType<SkillUI>();

        currentSceneIndex = TotalGameManager.Instance.CurrentScene.buildIndex;

        //Job = jobData.jobType;
        //if (this.gameObject.name != $"Player_{Job.ToString()}")
        //{
        //    Debug.Log("선택받지 못한 직업은 지워짐");
        //    Destroy(this.gameObject);
        //}
        //else
        //InGameManager.Instance.MainPlayer = GetComponent<Player>();


        MaxHP = 100;
        HP = MaxHP;
        SetHP();
        MaxExp = 100;
        Exp = 0;
        SetExp();
        SetLevel();
        potion = new ItemData_Potion();
        myWeapon = new ItemData_Weapon();

        newDel_LevelUp = LevelUp;
        transform.position = GameObject.Find("StartingPoint").transform.position;
    }

    private void OnEnable()
    {
        GetKey();
    }

    /// <summary>
    /// InputSystem에 등록한 단축키들에 해당하는 함수 해제
    /// </summary>
    private void OnDisable()
    {
        //input.Player.TestMakeItem.performed -= OnTestMakeItem;

        input.Player.TempItemUse.performed -= OnTempItemUse;
        input.Player.Attack.performed -= OnAttackInput;
        input.Player.Move.canceled -= OnMoveInput;
        input.Player.Move.performed -= OnMoveInput;
        input.Player.Look.performed -= OnLookInput;
        input.Player.TakeItem.performed -= OnTakeItem;
        input.Player.Disable();
    }

    /// <summary>
    /// InputSystem에 등록한 단축키들에 해당하는 함수 등록
    /// </summary>
    private void GetKey()
    {
        input = TotalGameManager.Instance.Input;
        input.Player.Enable();
        input.Player.Move.performed += OnMoveInput;
        input.Player.Move.canceled += OnMoveInput;
        input.Player.Attack.performed += OnAttackInput;
        input.Player.Look.performed += OnLookInput;
        input.Player.TempItemUse.performed += OnTempItemUse;
        input.Player.TakeItem.performed += OnTakeItem;
        //input.Player.TestMakeItem.performed += OnTestMakeItem;
    }

    private void Start()
    {
        if (!isDie)
        {
            Time.timeScale = 1.0f;
        }

    }

    private void Update()
    {

        transform.Translate(dir * Time.deltaTime * 10, Space.Self);   
        if (dir == Vector3.zero)
        {
            anim.SetBool("IsMove", false);
        }
        
    }

    private void OnMoveInput(InputAction.CallbackContext obj)
    {
        if (CanMove())
        {
            isKeepMoving = true;

            Vector3 tempDir;
            //2개의 축만 필요해 2d vector로 만들면 readvalue값을 2d로 받아야만 한다.
            //이후 3d로 변환하는 과정을 거친다.
            tempDir = obj.ReadValue<Vector2>();
            
            restartDir.x = tempDir.x;
            restartDir.z = tempDir.y;

            dir = restartDir;

            anim.SetFloat("DirSignal_Front", dir.z);
            anim.SetFloat("DirSignal_Side", dir.x);
            anim.SetBool("IsMove", true);
        }

        if(obj.canceled)
        {
            StopMove();
            isKeepMoving = false;
        }

    }

    /// <summary>
    /// 움직일 수 있는지 체크
    /// </summary>
    /// <returns></returns>
    private bool CanMove()
    {
        if (isDie || isAttack || isSkillUsing)
        {
            return false;
        }
        return true;
    }

    private void StopMove()
    {
        dir = Vector3.zero;
    }


    public void OnAttackInput(InputAction.CallbackContext obj)
    {
        if(!isDie || !isSkillUsing || !isMove)
        {
            if(isFindWeapon)
            {
                StopMove();
                anim.SetBool("IsMove", false);
                if (Job == JobType.SwordMan)
                {
                    anim.SetTrigger("AttackOn_SwordMan");
                }
                else if (Job == JobType.Witch)
                {
                    anim.SetTrigger("AttackOn_Witch");
                }
            }
        }
    }

    private void OnLookInput(InputAction.CallbackContext obj)
    {
        if(!isDie)
        {
            float moveX = obj.ReadValue<Vector2>().x;
            float moveY = obj.ReadValue<Vector2>().y;

            //좌우 회전
            turnToY = turnToY + moveX * turnSpeed * Time.deltaTime;

            //위아래 쳐다보기, 카메라 스크립트 구현 후 카메라만 움직이게 할 예정
            turnToX = turnToX + moveY * turnSpeed * Time.deltaTime;

            //turnToY = Mathf.Clamp(turnToY, -80, 80);    //최대값 설정
            //turnToX = Mathf.Clamp(turnToX, 0, 20);

            transform.eulerAngles = new Vector3(0, turnToY, 0);
            //mainCamera_PlayerPos.transform.localEulerAngles = new Vector3(-turnToX, 0, 0); Y축 넣었다가 어지러워서 뺐음
        }
    }

    /// <summary>
    /// Keyboard Q
    /// </summary>
    private void OnTempItemUse(InputAction.CallbackContext obj)     
    {

        //아이템 생성 ==> 성공
        //GameObject itemObj = ItemFactory.MakeItem((uint)ItemIDCode.HP_Potion, transform.position, Quaternion.identity);
        //아이템 사용
        //if(playerInventory.FindSameItemSlotForUseItem(potion). != null);
        if(playerInventory.FindSameItemSlotForUseItem(potion).SlotItemData != null)
        {
            int tempID;
            potion.Use(player);
            if(playerInventory.FindSameItemSlotForUseItem(potion).ItemCount == 1)
            {
                tempID = playerInventory.FindSameItemSlotForUseItem(potion).slotID;
                playerInventory.FindSameItemSlotForUseItem(potion).ClearSlotItem();
                playerInventoryUI.slotUIs[tempID].ItemData = null;
                playerInventoryUI.slotUIs[tempID].SlotUICount = 0;
                playerInventoryUI.SetAllSlotWithData();
            }
            else
            {
                tempID = playerInventory.FindSameItemSlotForUseItem(potion).slotID;
                playerInventory.FindSameItemSlotForUseItem(potion).ItemCount--;
                playerInventoryUI.slotUIs[tempID].SlotUICount--;
                playerInventoryUI.SetAllSlotWithData();
            }
            
        }
        
        
    }

    /// <summary>
    /// Keyboard F를 눌러 실행
    /// </summary>
    private void OnTakeItem(InputAction.CallbackContext obj)    
    {
        Collider[] findItem = Physics.OverlapSphere(transform.position, findItemRange, LayerMask.GetMask("Item"));
        if(findItem.Length > 0)
        {
            GameObject tempObj = findItem[0].gameObject;
            Item tempItem = tempObj.GetComponent<Item>();

            if(playerInventory.TakeItem(tempItem.data, 1))
            {
                playerInventoryUI.SetAllSlotWithData();
                Destroy(tempObj);
            }

        }
    }

    /// <summary>
    /// Mouse Right Click (UI꺼져있을 때)
    /// </summary>
    /// <param name="obj"></param>
    //private void OnTestMakeItem(InputAction.CallbackContext obj)    
    //{
    //    ItemFactory.MakeItem(ItemIDCode.Basic_Weapon_1, transform.position, Quaternion.identity);
    //}

    public void SetHP()
    {
        StartCoroutine(CoSetHP());
        hpValue_Text.text = (hp / maxHp * 100).ToString("F0") + '%';
    }

    public void SetExp()
    {
        //expBar.value = Exp / MaxExp;
        StartCoroutine(CoSetEXP());
        expValue_Text.text = (Exp / MaxExp * 100).ToString("F0") + '%';
    }


    IEnumerator CoSetHP()
    {
        while(hpBar.value != hp / maxHp)
        {
            hpBar.value = Mathf.Lerp(hpBar.value, hp / maxHp, 0.02f);
            yield return null;
        }
    }

    IEnumerator CoSetEXP()
    {
        while (expBar.value != Exp / MaxExp)
        {
            expBar.value = Mathf.Lerp(expBar.value, Exp / MaxExp, 0.02f);
            yield return null;
        }
    }



    private void HPBlink()
    {
        float cycle = 0.2f;
        StartCoroutine(CoHPBlink(cycle));
    }

    IEnumerator CoHPBlink(float cycle_seconds)
    {
        while(HP / MaxHP < 0.5f)
        {
            if(isHPImageAlpha1)
            {
                hpFillImage.CrossFadeAlpha(0, cycle_seconds, true);
                yield return new WaitForSeconds(cycle_seconds);
                isHPImageAlpha1 = false;
            }
            else 
            {
                hpFillImage.CrossFadeAlpha(1, cycle_seconds, true);
                yield return new WaitForSeconds(cycle_seconds);
                isHPImageAlpha1 = true;
            }
        }
        hpFillImage.CrossFadeAlpha(1, cycle_seconds, true);
        isHPImageAlpha1 = true;
    }

    public void SetLevel()
    {
        lvText.text = level.ToString();
    }

    public void SetSkillPointUp()
    {
        skillPoint++;
        skillUI.SynchronizeSkillPoint();
    }

    public void SetSkillPointDown()
    {
        if(SkillPoint > 0)
        {
            skillPoint--;
            skillUI.SynchronizeSkillPoint();
        }
    }

    public void LevelUp()
    {
        level++;
        SetLevel();
        SetSkillPointUp();

        Exp -= MaxExp;
        MaxExp *= 1.3f;
        SetExp();
        ParticlePlayer.Instance.PlayParticle(ParticleType.ParticleSystem_LevelUp, transform, transform.position, transform.rotation);
        SoundPlayer.Instance.PlaySound(SoundType.Sound_LevelUp);
    }

    /// <summary>
    /// 바로 아래 위치한 애니메이션으로 attackTrigger조절하는 함수에 collider를 전해주기 위한 함수
    /// </summary>
    public void TakeWeapon(GameObject weaponObject = null)     
    {
        PlayerWeapon tempPlayerWeapon;
        if (weaponObject == null)
        {
            tempPlayerWeapon = GetComponentInChildren<PlayerWeapon>();
            //무기 장착시 SkillUse클래스에서도 무기를 받아오도록 함(무기가 시작할 땐 장착되어있지 않아 SkillUse Awake에서 안한다)
        }
        else
        {
            tempPlayerWeapon = weaponObject.transform.GetComponent<PlayerWeapon>();
        }
        skill_Implement.TakeWeapon(weaponObject);

        if (tempPlayerWeapon != null)
        {
            weaponPrefab = tempPlayerWeapon.gameObject;
            weaponCollider = weaponPrefab.GetComponent<CapsuleCollider>();
            weaponCollider.enabled = false;
            isFindWeapon = true;
            Debug.Log("무기찾음");
        }
        else
        {
            isFindWeapon = false;
            Debug.Log("무기못찾음");
        }
        
    }

    public void EquipWeaponAbility()
    {
        AttackDamage += myWeapon.attackDamage;
    }

    public void UnEquipWeaponAbility()
    {
        attackDamage -= myWeapon.attackDamage;
        myWeapon = null;
    }

    private void Die()
    {
        isDie = true;
        CanMove();
        anim.SetBool("isDie", true);
        rigid.drag = 1000;
        rigid.angularDrag = 1000;
        rigid.isKinematic = true;
        rigid.mass = 100;
        StartCoroutine(CoDie());
    }

    IEnumerator CoDie()
    {
        float dieWaitingTime = 4.0f;
        yield return new WaitForSeconds(dieWaitingTime);
        FadeInOut.Instance.Fadeout();
        Time.timeScale = 0.0f;
    }

    

    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    
    public void IsMoveOn()
    {
        isMove = true;
    }

    public void IsMoveOff()
    {
        isMove = false;
    }

    public void IsAttackOn()
    {
        isAttack = true;
    }

    public void IsAttackOff()
    {
        isAttack = false;
    }

    public void AttackTriggerOn()
    {
        if(isFindWeapon)
        {
            weaponCollider.enabled = true;
        }
        
    }

    public void IsRestartMove()
    {
        if(isKeepMoving)
        {
            dir = restartDir;
            anim.SetFloat("DirSignal_Front", dir.z);
            anim.SetFloat("DirSignal_Side", dir.x);
            anim.SetBool("IsMove", true);
        }
    }

    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    public void AttackTriggerOff()
    {
        if(isFindWeapon)
        {
            weaponCollider.enabled = false;
        }
        
    }

    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    public void IsSkillUseOn()
    {
        StopMove();
        isSkillUsing = true;
    }

    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    public void IsSkillUseOff()
    {
        isSkillUsing = false;
    }

    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    public void AttackSoundStart()
    {
        if(Job == JobType.SwordMan)
        {
            SoundPlayer.Instance?.PlaySound(SoundType.Sound_Attack_SwordMan);
        }
        else if(Job == JobType.Witch)
        {
            SoundPlayer.Instance?.PlaySound(SoundType.Sound_Attack_Witch);
        }
    }

    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    public void WalkSoundAndEffectStart()
    {
        SoundPlayer.Instance?.PlaySound(SoundType.Sound_Walk);
        ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_Walk, transform.position, transform.rotation);
    }

    //------------------------------스킬 구현용 함수----------------------------------------------------


    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    public void UsingSkill_DashAttack()
    {
        Skill_Implement.Instance.SwordMan_Skill_DashAttack(2);
    }

    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    public void WitchAttack()
    {
        Vector3 compensatePosition = new Vector3(0.0f, 0.5f, 0.5f);  //투사체 프리팹 위치 보정
        Instantiate(witchAttackPrefab, transform.position + compensatePosition, transform.rotation);
    }

    /// <summary>
    /// 유니티 애니메이션에서 이벤트로 활성화 할 함수
    /// </summary>
    public void UsingSkill_Meteo()
    {
        Skill_Implement.Instance.Witch_Skill_Meteo(10);
    }
}
