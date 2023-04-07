using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 상호작용이 가능한 오브젝트의 부모
/// 1. 플레이어가 공격할 수 있어야함 => 체력 존재
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
