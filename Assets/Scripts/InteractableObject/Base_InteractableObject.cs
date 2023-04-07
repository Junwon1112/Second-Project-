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
    /// ó���� �����ϴ� �ܰ�� Ŭ���� ���ζ� ������Ƽ �Ȱ�ġ�� �ٷ� ������ ����
    /// </summary>
    protected void SetData()
    {
        hp = objectData.hp;
        maxHP = objectData.maxHP;
        defence = objectData.defence;
    }
}
