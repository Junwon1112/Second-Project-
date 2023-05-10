using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۰� ���õ� �����͸� �����ϴ� Ŭ����
/// </summary>
public class ItemDataManager : MonoBehaviour
{
    public ItemData_Potion[] itemDatas_Potion;
    public ItemData_Weapon[] itemDatas_Weapon;


    //-----------------�迭�� ����ϰ� �ε����� ã�� ����� id�� �ε����� ���� ���ƾ��ϰ�(id��10���̸� 10��°���� �迭�� �����ߵ�) itemdata�ڽ� ���´� �������� ��� �ϴ� ����----------- 


    ///// <summary>
    ///// �ε���, ������Ƽ�� �迭ó�� ���, ������Ƽ �̸��� this�� �ؼ� Ŭ�����̸����� ������Ƽ�� 
    ///// ȣ�� ��, �迭�� �Ἥ �ش� �迭�� ���� �ε����� ���� ������Ƽ ���� �������� 
    ///// </summary>
    ///// <param name="i"></param>
    ///// <returns></returns>
    //public ItemData this[int i]
    //{
    //    get
    //    {
    //        return itemDatas_Potion[i];
    //    }
    //}
    ////�迭ó�� ���� ������Ƽ

    //public ItemData this[ItemIDCode ID]  //�ε���
    //{
    //    get
    //    {
    //        return itemDatas_Potion[(int)ID];
    //    }
    //}
    ////�迭ó�� ���� ������Ƽ

    public ItemData FindItem_ItemData(uint _skillID)
    {
        if(_skillID < 10)
        {
            for (int i = 0; i < itemDatas_Potion.Length; i++)
            {
                if (itemDatas_Potion[i].ID == _skillID)
                {
                    return itemDatas_Potion[i];
                }
            }
        }
        else if(_skillID < 20)
        {
            for (int i = 0; i < itemDatas_Weapon.Length; i++)
            {
                if (itemDatas_Weapon[i].ID == _skillID)
                {
                    return itemDatas_Weapon[i];
                }
            }
        }
        

        return null;
    }


    //----------------------------------------------------------------------
    /// <summary>
    /// ID�� ���ϴ� �����۵����� ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="_skillID"></param>
    /// <returns></returns>
    public ItemData_Potion FindItem_Potion(uint _skillID)
    {
        for (int i = 0; i < itemDatas_Potion.Length; i++)
        {
            if (itemDatas_Potion[i].ID == _skillID)
            {
                return itemDatas_Potion[i];
            }
        }

        return null;
    }

    /// <summary>
    /// itemIDCode(������)�� ���ϴ� �����۵����� ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="_itemIDCode"></param>
    /// <returns></returns>
    public ItemData_Potion FindItem_Potion(ItemIDCode _itemIDCode)
    {
        for (int i = 0; i < itemDatas_Potion.Length; i++)
        {
            if (itemDatas_Potion[i].itemIDCode == _itemIDCode)
            {
                return itemDatas_Potion[i];
            }
        }

        return null;
    }

    /// <summary>
    /// �̸����� ���ϴ� �����۵����� ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="_itemName"></param>
    /// <returns></returns>
    public ItemData_Potion FindItem_Potion(string _itemName)
    {
        for (int i = 0; i < itemDatas_Potion.Length; i++)
        {
            if (itemDatas_Potion[i].itemName == _itemName)
            {
                return itemDatas_Potion[i];
            }
        }

        return null;
    }
    //-------------------------------------------------------����-------------------------

    /// <summary>
    /// ID�� ���ϴ� �����۵����� ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="_skillID"></param>
    /// <returns></returns>
    public ItemData_Weapon FindItem_Weapon(uint _skillID)
    {
        for (int i = 0; i < itemDatas_Weapon.Length; i++)
        {
            if (itemDatas_Weapon[i].ID == _skillID)
            {
                return itemDatas_Weapon[i];
            }
        }

        return null;
    }

    /// <summary>
    /// itemIDCode(������)�� ���ϴ� �����۵����� ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="_itemIDCode"></param>
    /// <returns></returns>
    public ItemData_Weapon FindItem_Weapon(ItemIDCode _itemIDCode)
    {
        for (int i = 0; i < itemDatas_Weapon.Length; i++)
        {
            if (itemDatas_Weapon[i].itemIDCode == _itemIDCode)
            {
                return itemDatas_Weapon[i];
            }
        }

        return null;
    }

    /// <summary>
    /// �̸����� ���ϴ� �����۵����� ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="_itemName"></param>
    /// <returns></returns>
    public ItemData_Weapon FindItem_Weapon(string _itemName)
    {
        for (int i = 0; i < itemDatas_Weapon.Length; i++)
        {
            if (itemDatas_Weapon[i].itemName == _itemName)
            {
                return itemDatas_Weapon[i];
            }
        }

        return null;
    }
}
