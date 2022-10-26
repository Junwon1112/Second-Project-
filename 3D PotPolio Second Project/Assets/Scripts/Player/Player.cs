using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    PlayerInput input;
    Vector3 dir = Vector3.zero;
    Animator anim;
    bool canMove = true;
    float hp;
    float maxHp = 100;
    Slider hpBar;

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
    }

    private void OnDisable()
    {
        input.Player.Attack.performed -= OnAttackInput;
        input.Player.Move.performed -= OnMoveInput;
        input.Player.Disable();
    }

    private void Start()
    {
        hp = maxHp;
        SetHP();
    }

    private void Update()
    {
        transform.position += dir * Time.deltaTime * 10;
        if(dir == Vector3.zero)
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


    private void SetHP()
    {
        hpBar.value = hp / maxHp * 100;
    }


}
