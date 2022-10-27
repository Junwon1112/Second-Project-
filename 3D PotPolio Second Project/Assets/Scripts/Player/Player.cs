using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //움직임을 위한 인풋 시스템용
    PlayerInput input;
    //이동 방향 받고 리턴용
    Vector3 dir = Vector3.zero;
    //애니메이션 용
    Animator anim;
    //다른 행동중 움직임을 제한하기 위해
    bool canMove = true;

    //체력 관련 변수들
    float hp;
    float maxHp = 100;
    Slider hpBar;

    //회전 관련 변수들
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

    private void OnLookInput(InputAction.CallbackContext obj)
    {
        Debug.Log("쳐다보기");

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


    private void SetHP()
    {
        hpBar.value = hp / maxHp;
    }


}
