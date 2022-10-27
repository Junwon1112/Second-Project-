using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //�������� ���� ��ǲ �ý��ۿ�
    PlayerInput input;
    //�̵� ���� �ް� ���Ͽ�
    Vector3 dir = Vector3.zero;
    //�ִϸ��̼� ��
    Animator anim;
    //�ٸ� �ൿ�� �������� �����ϱ� ����
    bool canMove = true;

    //ü�� ���� ������
    float hp;
    float maxHp = 100;
    Slider hpBar;

    //ȸ�� ���� ������
    float turnToX;
    float turnToY;
    float turnToZ;

    float turnSpeed = 30.0f;

    public float HP
    {
        get { return hp; }
        set 
        { 
            hp = value; 

        }
    }
    

    private void Awake()
    {
        input = new PlayerInput();
        anim = GetComponent<Animator>();
        hpBar = GameObject.Find("HpSlider").GetComponent<Slider>();
    }

    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += OnMoveInput;
        input.Player.Attack.performed += OnAttackInput;
        input.Player.Look.performed += OnLookInput;
    }


    private void OnDisable()
    {
        input.Player.Attack.performed -= OnAttackInput;
        input.Player.Move.performed -= OnMoveInput;
        input.Player.Look.performed -= OnLookInput;
        input.Player.Disable();
    }

    private void Start()
    {
        hp = maxHp;
        SetHP();
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
            //2���� �ุ �ʿ��� 2d vector�� ����� readvalue���� 2d�� �޾ƾ߸� �Ѵ�.
            //���� 3d�� ��ȯ�ϴ� ������ ��ģ��.
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

    private void OnLookInput(InputAction.CallbackContext obj)
    {
        Debug.Log("�Ĵٺ���");

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


    private void SetHP()
    {
        hpBar.value = hp / maxHp;
    }


}
