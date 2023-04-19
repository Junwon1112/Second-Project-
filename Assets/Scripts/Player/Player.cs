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



    /// <summary>
    /// 움직임을 위한 인풋 시스템용
    /// </summary>
    public PlayerInput input;

    /// <summary>
    /// 죽었을 때 리지드바디 변화용
    /// </summary>
    Rigidbody rigid;


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
    float hp;
    float maxHp = 100;
    Slider hpBar;

    /// <summary>
    /// 경험치 관련 변수들
    /// </summary>
    float exp = 0.0f;
    float maxExp = 100;
    Slider expBar;

    [SerializeField]
    int level = 1;
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

    float turnSpeed = 25.0f;

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

    public Transform CharacterTransform
    {
        get { return this.transform; }
    }

    public float HP
    {
        get { return hp; }
        set 
        {
            if(!isDie)
            {
                hp = value;
                if (hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    public float MaxHP
    {
        get { return maxHp; }
    }

    public float Exp
    {
        get { return exp; }
        set
        {
            exp = value;

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
        input = new PlayerInput();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        hpBar = GameObject.Find("HpSlider").GetComponent<Slider>();
        expBar = GameObject.Find("ExpSlider").GetComponent<Slider>();
        lvText = GameObject.Find("Level_num").GetComponent<TextMeshProUGUI>();
        player = GetComponent<Player>();
        playerInventory = GetComponentInChildren<Inventory>();
        playerInventoryUI = FindObjectOfType<InventoryUI>();
        weaponHandTransform = FindObjectOfType<FindWeaponHand>().transform;
        
        skillUses = FindObjectsOfType<SkillUse>();
        skill_Implement = FindObjectOfType<Skill_Implement>();
        skillUI = FindObjectOfType<SkillUI>();
    }

    /// <summary>
    /// InputSystem에 등록한 단축키들에 해당하는 함수 등록
    /// </summary>
    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += OnMoveInput;
        input.Player.Move.canceled += OnMoveInput;
        input.Player.Attack.performed += OnAttackInput;
        input.Player.Look.performed += OnLookInput;
        input.Player.TempItemUse.performed += OnTempItemUse;
        input.Player.TakeItem.performed += OnTakeItem;
        //input.Player.TestMakeItem.performed += OnTestMakeItem;
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
        input.Player.Disable();
        input.Player.TakeItem.performed -= OnTakeItem;
    }

    

    private void Start()
    {
        hp = maxHp;
        SetHP();
        SetExp();
        SetLevel();
        potion = new ItemData_Potion();
        myWeapon = new ItemData_Weapon();

        newDel_LevelUp = LevelUp;


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
        //else
        //{
        //    StopMove();
        //}
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


    private void OnAttackInput(InputAction.CallbackContext obj)
    {
        if(!isDie || !isSkillUsing || !isMove)
        {
            StopMove();
            anim.SetBool("IsMove", false);
            anim.SetTrigger("AttackOn");
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
            turnToX = Mathf.Clamp(turnToX, -20, 20);

            transform.eulerAngles = new Vector3(0, turnToY, 0);

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
                playerInventoryUI.slotUIs[tempID].slotUIData = null;
                playerInventoryUI.slotUIs[tempID].slotUICount = 0;
                playerInventoryUI.SetAllSlotWithData();
            }
            else
            {
                tempID = playerInventory.FindSameItemSlotForUseItem(potion).slotID;
                playerInventory.FindSameItemSlotForUseItem(potion).ItemCount--;
                playerInventoryUI.slotUIs[tempID].slotUICount--;
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

            playerInventory.TakeItem(tempItem.data, 1);
            playerInventoryUI.SetAllSlotWithData();
            Destroy(tempObj);

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
        hpBar.value = HP / MaxHP;
    }

    public void SetExp()
    {
        expBar.value = Exp / MaxExp;
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
    }

    /// <summary>
    /// 바로 아래 위치한 애니메이션으로 attackTrigger조절하는 함수에 collider를 전해주기 위한 함수
    /// </summary>
    public void TakeWeapon()     
    {
        PlayerWeapon tempPlayerWeapon = FindObjectOfType<PlayerWeapon>();
        for(int i = 0; i < skillUses.Length; i++)   //무기 장착시 SkillUse클래스에서도 무기를 받아오도록 함(무기가 시작할 땐 장착되어있지 않아 SkillUse Awake에서 안한다)
        {
            skillUses[i].TakeWeapon();
            skill_Implement.TakeWeapon();
        }
        if (tempPlayerWeapon != null)
        {
            weaponPrefab = tempPlayerWeapon.gameObject;
            weaponCollider = this.weaponPrefab.GetComponent<CapsuleCollider>();
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
        SoundPlayer.Instance?.PlaySound(SoundType.Sound_Attack);
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
        Skill_Implement.Instance.Skill_DashAttack(2);
    }
}
