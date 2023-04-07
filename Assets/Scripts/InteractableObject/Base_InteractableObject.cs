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
    [SerializeField]
    protected Data_InteractableObject objectData;

    protected float hp;
    protected float maxHP;
    protected float defence;

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
        SetData();
        SetHP();
    }

    protected abstract void DropItem();
    
    protected void SetHP()
    {
        hp = MaxHP;
        hpSlider.value = HP / MaxHP;
    }

    /// <summary>
    /// 처음에 세팅하는 단계고 클래스 내부라 프로퍼티 안거치고 바로 변수에 넣음
    /// </summary>
    protected void SetData()
    {
        hp = objectData.hp;
        maxHP = objectData.maxHP;
        defence = objectData.defence;
    }
}
