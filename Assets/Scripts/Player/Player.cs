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
    /// �������� ���� ��ǲ �ý��ۿ�
    /// </summary>
    public PlayerInput input;

    /// <summary>
    /// �׾��� �� ������ٵ� ��ȭ��
    /// </summary>
    Rigidbody rigid;


    /// <summary>
    /// �̵� ���� �ް� ���Ͽ�
    /// </summary>
    Vector3 dir = Vector3.zero;
    /// <summary>
    /// �̵� �� �ٸ� ������ �ѵ� �ٽ� �̵��� �� ����ϱ� ���� �߰� ���ۿ�
    /// </summary>
    Vector3 restartDir = Vector3.zero;

    //float walkSoundVolume = 1.0f;
    //float AttackSoundVolume = 0.7f;

    /// <summary>
    /// �ִϸ��̼� �� 
    /// </summary>
    Animator anim;

    /// <summary>
    /// ü�� ���� ������
    /// </summary>
    float hp;
    float maxHp = 100;
    Slider hpBar;

    /// <summary>
    /// ����ġ ���� ������
    /// </summary>
    float exp = 0.0f;
    float maxExp = 100;
    Slider expBar;

    [SerializeField]
    int level = 1;
    TextMeshProUGUI lvText;

    /// <summary>
    /// ��ų�� ����� ��ų����Ʈ, �������� ����
    /// </summary>
    uint skillPoint = 1;

    /// <summary>
    /// �������ϸ� ������ �Լ��� ��ųUI���� ��ų����Ʈ�� �÷��̾� ��ų����Ʈ�� ����ȭ ���ִ� �Լ� ���
    /// </summary>
    public delegate void del_LevelUP();
    public del_LevelUP newDel_LevelUp;

    SkillUI skillUI;



    /// <summary>
    /// ȸ�� ���� ������
    /// </summary>
    float turnToX;
    float turnToY;
    float turnToZ;

    float turnSpeed = 25.0f;

    /// <summary>
    /// ������ ���� ����
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
    /// ���� �ٲܶ� ����ϱ� ���� ������
    /// </summary>
    GameObject weaponPrefab;
    CapsuleCollider weaponCollider;
    public Transform weaponHandTransform;
    public bool isFindWeapon = false;

    /// <summary>
    /// ��ų ��� ������ üũ�ϱ� ���� ���
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
    /// private���� ����Ƽ���� ��ġ�ٲܼ� �ְ� ���ִ� ��
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
    /// InputSystem�� ����� ����Ű�鿡 �ش��ϴ� �Լ� ���
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
    /// InputSystem�� ����� ����Ű�鿡 �ش��ϴ� �Լ� ����
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
            //2���� �ุ �ʿ��� 2d vector�� ����� readvalue���� 2d�� �޾ƾ߸� �Ѵ�.
            //���� 3d�� ��ȯ�ϴ� ������ ��ģ��.
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
    /// ������ �� �ִ��� üũ
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

            //�¿� ȸ��
            turnToY = turnToY + moveX * turnSpeed * Time.deltaTime;

            //���Ʒ� �Ĵٺ���, ī�޶� ��ũ��Ʈ ���� �� ī�޶� �����̰� �� ����
            turnToX = turnToX + moveY * turnSpeed * Time.deltaTime;

            //turnToY = Mathf.Clamp(turnToY, -80, 80);    //�ִ밪 ����
            turnToX = Mathf.Clamp(turnToX, -20, 20);

            transform.eulerAngles = new Vector3(0, turnToY, 0);

        }
    }

    /// <summary>
    /// Keyboard Q
    /// </summary>
    private void OnTempItemUse(InputAction.CallbackContext obj)     
    {

        //������ ���� ==> ����
        //GameObject itemObj = ItemFactory.MakeItem((uint)ItemIDCode.HP_Potion, transform.position, Quaternion.identity);
        //������ ���
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
    /// Keyboard F�� ���� ����
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
    /// Mouse Right Click (UI�������� ��)
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
    /// �ٷ� �Ʒ� ��ġ�� �ִϸ��̼����� attackTrigger�����ϴ� �Լ��� collider�� �����ֱ� ���� �Լ�
    /// </summary>
    public void TakeWeapon()     
    {
        PlayerWeapon tempPlayerWeapon = FindObjectOfType<PlayerWeapon>();
        for(int i = 0; i < skillUses.Length; i++)   //���� ������ SkillUseŬ���������� ���⸦ �޾ƿ����� ��(���Ⱑ ������ �� �����Ǿ����� �ʾ� SkillUse Awake���� ���Ѵ�)
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
            Debug.Log("����ã��");
        }
        else
        {
            isFindWeapon = false;
            Debug.Log("�����ã��");
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
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
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
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void AttackTriggerOff()
    {
        if(isFindWeapon)
        {
            weaponCollider.enabled = false;
        }
        
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void IsSkillUseOn()
    {
        isSkillUsing = true;
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void IsSkillUseOff()
    {
        isSkillUsing = false;
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void AttackSoundStart()
    {
        SoundPlayer.Instance?.PlaySound(SoundType.Sound_Attack);
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void WalkSoundAndEffectStart()
    {
        SoundPlayer.Instance?.PlaySound(SoundType.Sound_Walk);
        ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_Walk, transform.position, transform.rotation);
    }

    //------------------------------��ų ������ �Լ�----------------------------------------------------
    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void UsingSkill_DashAttack()
    {
        Skill_Implement.Instance.Skill_DashAttack(2);
    }
}
