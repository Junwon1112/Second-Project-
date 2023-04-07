using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ȣ�ۿ��� ������ ������Ʈ�� �θ�
/// 1. �÷��̾ ������ �� �־���� => ü�� ����
/// </summary>
public abstract class Base_InteractableObject : MonoBehaviour,IHealth
{
    protected float hp;
    protected float maxHP = 50;
    protected float defence = 0;

    protected Slider hpSlider;


    public Transform CharacterTransform
    {
        get
        {
            return this.transform;
        }
    }

    public float HP 
    {
        get { return hp; }
        set
        {
            hp = value;

            if (hp <= 0)
            {
                DropItem();
                Destroy(this.gameObject);
            }
        }
    }

    public float MaxHP { get; }

    public float Defence { get; set; }

    protected virtual void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();
        SetHP();
    }

    protected abstract void DropItem();
    
    protected void SetHP()
    {
        hp = MaxHP;
        hpSlider.value = HP / MaxHP;
    }

}
