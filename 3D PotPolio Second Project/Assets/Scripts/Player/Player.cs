using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHealth
{
    Player player;

    //움직임을 위한 인풋 시스템용
    public PlayerInput input;

    //이동 방향 받고 리턴용
    Vector3 dir = Vector3.zero;

    //애니메이션 용
    Animator anim;

    //다른 행동중 움직임을 제한하기 위해
    bool canMove = true;

    Monster monster;

    //체력 관련 변수들
    float hp;
    float maxHp = 100;
    Slider hpBar;

    //회전 관련 변수들
    float turnToX;
    float turnToY;
    float turnToZ;

    float turnSpeed = 30.0f;

    //아이템 관련 변수
    Item item;
    ItemFactory itemFactory;
    ItemIDCode itemID;
    ItemData_Potion potion;

    float findItemRange = 3.0f;
    Inventory playerInventory;
    InventoryUI playerInventoryUI;

    public float HP
    {
        get { return hp; }
        set 
        { 
            hp = value; 

        }
    }

    public float MaxHP
    {
        get { return maxHp; }
    }

    [SerializeField]    //private여도 유니티에서 수치바꿀수 있게 해주는 것
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
        hpBar = GameObject.Find("HpSlider").GetComponent<Slider>();
        player = GetComponent<Player>();
        playerInventory = GetComponentInChildren<Inventory>();
        playerInventoryUI = FindObjectOfType<InventoryUI>();

    }

    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += OnMoveInput;
        input.Player.Attack.performed += OnAttackInput;
        input.Player.Look.performed += OnLookInput;
        input.Player.TempItemUse.performed += OnTempItemUse;
        input.Player.TakeItem.performed += OnTakeItem;
        input.Player.TestMakeItem.performed += OnTestMakeItem;
    }



    private void OnDisable()
    {
        input.Player.TestMakeItem.performed -= OnTestMakeItem;
        input.Player.TempItemUse.performed -= OnTempItemUse;
        input.Player.Attack.performed -= OnAttackInput;
        input.Player.Move.performed -= OnMoveInput;
        input.Player.Look.performed -= OnLookInput;
        input.Player.Disable();
        input.Player.TakeItem.performed -= OnTakeItem;
    }

    

    private void Start()
    {
        hp = maxHp;
        SetHP();
        potion = new ItemData_Potion();
    }

    private void Update()
    {
        //transform.position += dir * Time.deltaTime * 10;
        transform.Translate(dir * Time.deltaTime * 10, Space.Self);
        if (dir == Vector3.zero)
        {
            anim.SetBool("IsMove", false);
        }

        
    }

    private void OnMoveInput(InputAction.CallbackContext obj)
    {
        if(canMove)
        {
            //2개의 축만 필요해 2d vector로 만들면 readvalue값을 2d로 받아야만 한다.
            //이후 3d로 변환하는 과정을 거친다.
            Vector3 tempDir;
            tempDir = obj.ReadValue<Vector2>();
            dir.x = tempDir.x;
            dir.z = tempDir.y;

            anim.SetFloat("DirSignal_Front", dir.z);
            anim.SetFloat("DirSignal_Side", dir.x);
            anim.SetBool("IsMove", true);
        }
        
    }


    private void OnAttackInput(InputAction.CallbackContext obj)
    {
        //if (comboTimer > 0)
        //{
        //    anim.SetBool("CanCombo", true);
        //}
        Debug.Log("attack");
        anim.SetBool("IsMove", false);
        anim.SetTrigger("AttackOn");

        //comboTimer = 0.5f;
        //anim.SetBool("CanCombo", false);
    }

    //private void OnTriggrEnter(Collider other)
    //{
    //    //플레이어 칼에있는 컬라이더의 트리거
    //    if(other.CompareTag("Monster"))
    //    {
    //        Monster monster;
    //        monster = other.GetComponent<Monster>();
    //        if(monster.playerTriggerOff == false)
    //        {
    //            Attack(monster);
    //            monster.SetHP();
                
    //        }
    //        monster.playerTriggerOff = false;

    //    }
    //}

    private void OnLookInput(InputAction.CallbackContext obj)
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

    private void OnTestMakeItem(InputAction.CallbackContext obj)
    {
        ItemFactory.MakeItem(ItemIDCode.HP_Potion, transform.position, Quaternion.identity);
    }

    public void SetHP()
    {
        hpBar.value = HP / MaxHP;
    }

    public void Attack(IBattle target)
    {
       //target.HP -= (AttackDamage - target.Defence);
    }
}
