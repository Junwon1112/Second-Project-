using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �κ��丮�� ������ Ŭ����
/// </summary>
public class Inventory : MonoBehaviour
{
    //������ ģ����
    /*
     * ������ �κ�
     * -������ ���� => ���� ���� ���ϱ�, ������ �����Ϳ� ����
     * -�پ��� �����۵� => ������ ���丮 ����, ������ ������, �����͸� ���� scriptable Object, ������ ��ü(�������̶� �����͸� ����) 
     * -������ ���Կ� ������ �߰� => �κ����� ����
     * -���Կ��� ������ ���� => �κ����� ����
     * -���Կ��� ������ �ű�� => �κ� ���ο��� �ű�� & â��� �κ����̿��� �ű��?
     * -������ ���� �Ǵ� �Һ� => 
     * -������ ����?
     */

    //�κ����� �ؾ��Ұ� : ������ ���Ե� ������ ��ȣ�ۿ�, ��ü ���Կ� ���� �ۿ� => ���Ի����� ������ �̵�, ��ü ���� Ŭ����

    //������ ���� ���鼭 ���ڸ� ã�� �Լ��� �������� �߰��ϴ� �Լ� �����ؾ� ��

    private uint money;
    public uint startingMoney = 1100;
    public ItemSlot[] itemSlots;
    private uint slotCount = 6; //���� ���� 6��
    
    private InventoryUI invenUI;

    //������ ����� ����� ������ ������ ���丮
    //ItemFactory itemFactory = new ItemFactory();
    //-----------------
    //ItemData potion; �׽�Ʈ�� �ڵ�

    public uint Money
    {
        get { return money; }
        set { money = value; }
    }

    public ItemSlot this[uint count]    //�ε����� �̿��� ������Ƽ
    {
        get
        {
            return itemSlots[count];
        }
        set
        {
            itemSlots[count] = value;
        }
    }

    private void Awake()
    {
        invenUI = FindObjectOfType<InventoryUI>();
        itemSlots = new ItemSlot[slotCount]; //������ ���� ������ŭ �Ҵ�, start���� �����ߴٰ� inventoryUI���� �����ϴ� start�Լ����� slot�� �ҷ��;��ؼ� awake�� �ű�

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = new ItemSlot();
            itemSlots[i].slotID = i;
        }
    }


    /// <summary>
    /// �������� ��ġ�� �̵���Ű�� �Լ�
    /// </summary>
    /// <param name="fromItemSlot">�������� �̵��ϱ� �� ������ ��ġ</param>
    /// <param name="toItemSlot">�������� �̵��� �� ������ ��ġ</param>
    /// <param name="count">�̵��� ������ ����, �Ķ���� ���ۼ��� �⺻�� 1�� ����</param>
    public void MoveSlotItem(ItemSlot fromItemSlot, ItemSlot toItemSlot, uint count = 1)
    {
        //from => to���� to�� null�϶�
        //from => to�� ���� ������ ���� ��
        //from => to�� ���� ������ �ٸ� ��

        if(fromItemSlot.SlotItemData != null && toItemSlot.SlotItemData == null)    //�ߵ�
        {
            toItemSlot.SlotItemData = fromItemSlot.SlotItemData;
            toItemSlot.ItemCount = fromItemSlot.ItemCount;


            fromItemSlot.SlotItemData = null;
            fromItemSlot.ItemCount = 0;
            
        }
        else if (fromItemSlot.SlotItemData == toItemSlot.SlotItemData)              //�ߵ�  
        {
            if(fromItemSlot.ItemCount + toItemSlot.ItemCount <= toItemSlot.SlotItemData.itemMaxCount)
            {
                toItemSlot.ItemCount += fromItemSlot.ItemCount;

                fromItemSlot.SlotItemData = null;
                fromItemSlot.ItemCount = 0;
                
            }
            else if(fromItemSlot.ItemCount + toItemSlot.ItemCount > fromItemSlot.SlotItemData.itemMaxCount)
            {
                uint tempCount = toItemSlot.ItemCount; 
                toItemSlot.ItemCount = (uint)toItemSlot.SlotItemData.itemMaxCount;
                fromItemSlot.ItemCount = (uint)fromItemSlot.SlotItemData.itemMaxCount - tempCount;
                Debug.Log($"{fromItemSlot.SlotItemData.itemName}���� {toItemSlot.SlotItemData.itemName}���� �̵�");
                Debug.Log("���� �ű� �� ����");
            }
        }
        else if(fromItemSlot.SlotItemData != null && fromItemSlot.SlotItemData != toItemSlot.SlotItemData)   //�������� �� ������ ���� ���� Ȯ�� ���غ�
        {
            ItemSlot tempItemSlot;
            tempItemSlot = new ItemSlot();

            tempItemSlot.SlotItemData = toItemSlot.SlotItemData;
            tempItemSlot.ItemCount = toItemSlot.ItemCount;

            toItemSlot.SlotItemData = fromItemSlot.SlotItemData;
            toItemSlot.ItemCount = fromItemSlot.ItemCount;

            fromItemSlot.SlotItemData = tempItemSlot.SlotItemData;
            fromItemSlot.ItemCount = tempItemSlot.ItemCount;
            Debug.Log("�ڸ��ٲ�");
        }
        else
        {
            Debug.Log($"������ �������");
        }
    }

    /// <summary>
    /// ��� ������ ���� �޼���
    /// </summary>
    public void ClearInven()    //Ȯ�� �Ϸ�, ��� ���� ���
    {
        foreach(ItemSlot itemSlot in itemSlots)
        {
            itemSlot.SlotItemData = null;
            itemSlot.ItemCount = 0;
        }
    }

    /// <summary>
    /// Ư�� ���� ������ �ʵ�� ������
    /// </summary>
    /// <param name="dropItemSlot">�������� ���� ����</param>
    /// <param name="dropCount">���� ����</param>
    public void DropItem(ItemSlot dropItemSlot,uint dropCount)  
    {
        if(dropItemSlot.ItemCount <= dropCount)  //������ ������ ���� �������� ���ų� ���ٸ�
        {
            uint tempCount = dropItemSlot.ItemCount;
            dropItemSlot.DecreaseSlotItem(dropCount);   //decrease�Լ��� �Ҵ�� �������� ���� ������ ������ 0���� �ǰ� �����Ǿ�����
            for(int i = 0; i < tempCount; i++)
            {
                ItemFactory.MakeItem(dropItemSlot.SlotItemData.ID, transform.position, Quaternion.identity, true);
            }

        }
        else
        {
            dropItemSlot.DecreaseSlotItem(dropCount);   //decrease�Լ��� �Ҵ�� �������� ���� ������ ������ 0���� �ǰ� �����Ǿ�����
            for (int i = 0; i < dropCount; i++)
            {
                ItemFactory.MakeItem(dropItemSlot.SlotItemData.ID, transform.position, Quaternion.identity, true);
            }
        }
        
    }

    /// <summary>
    /// ���� ������ ���� �� ���� ���� ���� ���� �Ǵ� ����ִ� ���� ���� (�������� �߰��� �� ���)
    /// </summary>
    /// <param name="compareItemData">���� ������ ������</param>
    /// <returns>���� ������ ������ ����</returns>
    public ItemSlot FindSameItemSlotForAddItem(ItemData compareItemData)     
    {
        bool isFindSlot = false;
        ItemSlot returnItemSlot = null;
        for (int i = 0; i < slotCount; i++)
        {
            //ã�� �����۰� ���� ������ ������ ���Կ� �ڸ��� �ִٸ�(�ִ� �������� ���ٸ�)
            if (itemSlots[i].SlotItemData == compareItemData && itemSlots[i].ItemCount < itemSlots[i].SlotItemData.itemMaxCount)    
            {
                Debug.Log($"{itemSlots[i]}�� ���� �������̴�");
                returnItemSlot = itemSlots[i];
                isFindSlot = true;
                return returnItemSlot;
                //break;
            }
            
        }
        if(isFindSlot == false)
        {
            for (int i = 0; i < slotCount; i++)
            {
                //������ ������ ����ִٸ�
                if (itemSlots[i].SlotItemData == null && isFindSlot == false)
                {
                    Debug.Log($"{i}�� ������ ����ִ�");
                    returnItemSlot = itemSlots[i];
                    isFindSlot=true;
                    return returnItemSlot;
                    //break;
                }

            }
        }

        //����ִ� ������ ã�� ���ߴٸ� null���� �����Ѵ�.
        Debug.Log("������ �����ϴ�");
        return returnItemSlot;


    }

    /// <summary>
    /// ���� ������ ���� �� ���� ���� ���� ���� �Ǵ� ����ִ� ���� ���� (�������� ����� �� ���)
    /// </summary>
    /// <param name="compareItemData"></param>
    /// <returns></returns>
    public ItemSlot FindSameItemSlotForUseItem(ItemData compareItemData)
    {
        ItemSlot returnItemSlot = null;
        for (int i = 0; i < slotCount; i++)
        {
            //ã�� �����۰� ���� ������ ������ ���Կ� �ڸ��� �ִٸ�(�ִ� �������� ���ٸ�)
            if (itemSlots[i].SlotItemData.ID == compareItemData.ID)
            {
                Debug.Log($"{itemSlots[i]}�� ���� �������̴�");
                returnItemSlot = itemSlots[i];
                return returnItemSlot;
                //break;
            }

        }

        Debug.Log($"�������� ����");
        return returnItemSlot;
    }

    /// <summary>
    /// �������� �κ��丮�� ������ �˸��� ������ ã�� �Ҵ��� �ִ� �Լ�
    /// </summary>
    /// <param name="takeItemData">���� �����۵�����</param>
    /// <param name="count">���� ����</param>
    public bool TakeItem(ItemData takeItemData, uint count)
    {
        ItemSlot sameItemSlot = FindSameItemSlotForAddItem(takeItemData);
        bool isFindSlot = false;
        //�������� ������ NULL reference�� �ߴµ� findsameitemslot�Լ����� null���� �����ؼ� �ű⼭�� slotItemData�� ������ ���� ������ ���µ�, ������ �ϴ� �۵��� �ߵż� ���߿� ������ ��
        
        if(sameItemSlot != null)
        {
            if (sameItemSlot.SlotItemData != null) //�����Ͱ� null�� �ƴ϶�� => �����Ͱ� ������� ����.
            {
                uint remainCount = (uint)takeItemData.itemMaxCount - sameItemSlot.ItemCount;
                if ( remainCount >= count)
                {
                    sameItemSlot.IncreaseSlotItem(count);
                    isFindSlot = true;
                    return isFindSlot;
                }
                else
                {
                    sameItemSlot.IncreaseSlotItem(remainCount);
                    uint countNew = count - remainCount;
                    isFindSlot = TakeItem(takeItemData, countNew);
                    if(!isFindSlot)     //����߿� �Ұ����� ����� �ٽ� ���󺹱�
                    {
                        sameItemSlot.DecreaseSlotItem(remainCount);
                    }
                    return isFindSlot;
                }
            }
            else if(sameItemSlot.SlotItemData == null)  //�����Ͱ� null�϶�
            {
                uint remainCount = (uint)takeItemData.itemMaxCount;
                if (remainCount >= count)
                {
                    sameItemSlot.AssignSlotItem(takeItemData, count);
                    isFindSlot = true;
                    return isFindSlot;
                }
                else
                {
                    sameItemSlot.AssignSlotItem(takeItemData, remainCount);
                    uint countNew = count - remainCount;
                    isFindSlot = TakeItem(takeItemData, countNew);
                    if(!isFindSlot)
                    {
                        sameItemSlot.ClearSlotItem();
                    }
                    return isFindSlot;
                }
            }
        }
        //else //����ִ� ������ ã�����ߴٸ� ã�������� ǥ��
        //{
        //    Debug.Log("�κ��丮�� �Ҵ��Ҽ�����");
        //    return false;
        //}
        //else if(FindSameItemSlot(takeItemData) == null)  //����ִ� ������ ã�����ߴٸ� ã�������� ǥ��
        //{
        //    Debug.Log("�κ��丮�� �� ���ִ�");
        //}
        Debug.Log("�κ��丮�� �Ҵ��Ҽ�����");
        return isFindSlot;
    }


    // try catch���� �̿��� ������� �Լ� ����
    //public void aaa()
    //{ 
    //    try { } //�߰�ȣ �ȿ��� �Լ��� ������
    //    catch (System.Exception e)  //������ ����
    //    {
    //        e.ToString();
    //    }
    //}

}
