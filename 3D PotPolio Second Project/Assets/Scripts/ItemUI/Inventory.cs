using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //������ ģ����
    /*
     * ������ �κ�
     * -������ ���� => ���� ���� ���ϱ�, ������ �����Ϳ� ����
     * -�پ��� �����۵� => ������ ���丮 ����, ������ ������, �����͸� ���� scriptable Object, ������ ��ü(�������̶� �����͸� ����) 
     * -������ ���Կ� ������ �߰� => �κ����� ����
     * -���Կ��� ������ ���� => �κ����� ����
     * -���Կ��� ������ �ű�� => �κ� ���ο��� �ű�� & â���� �κ����̿��� �ű��?
     * -������ ���� �Ǵ� �Һ� => 
     * -������ ����?
     */

    //�κ����� �ؾ��Ұ� : ������ ���Ե� ������ ��ȣ�ۿ�, ��ü ���Կ� ���� �ۿ� => ���Ի����� ������ �̵�, ��ü ���� Ŭ����

    //������ ���� ���鼭 ���ڸ� ã�� �Լ��� �������� �߰��ϴ� �Լ� �����ؾ� ��


    public ItemSlot[] itemSlots;
    private uint slotCount = 6; //���� ���� 6��

    //������ ����� ����� ������ ������ ���丮
    //ItemFactory itemFactory = new ItemFactory();
    //-----------------
    ItemData potion;

    public ItemSlot this[uint count]    //�ε����� �̿��� ������Ƽ?
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
        itemSlots = new ItemSlot[slotCount]; //������ ���� ������ŭ �Ҵ�, start���� �����ߴٰ� inventoryUI���� �����ϴ� start�Լ����� slot�� �ҷ��;��ؼ� awake�� �ű�

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = new ItemSlot();
            itemSlots[i].slotID = i;
        }
    }

    private void Start()
    {
        

        //potion = new ItemData();

        //potion = GameManager.Instance.ItemManager[ItemIDCode.HP_Potion];





        //�׽�Ʈ��, Ȯ���� ���� ��----------------------



        //itemSlots[2].AssignSlotItem(potion, 4);
        //itemSlots[4].AssignSlotItem(potion, 4);
        //Debug.Log($"{itemSlots[2].SlotItemData.itemName},{itemSlots[2].ItemCount}");
        //Debug.Log($"{itemSlots[4].SlotItemData.itemName},{itemSlots[4].ItemCount}");
        //DropItem(itemSlots[2], 2);
        //FindSameItemSlot(potion);
        //TakeItem(potion, 1);
        //for (int i = 0; i < itemSlots.Length; i++)
        //{
        //    if (itemSlots[i].SlotItemData != null)
        //    Debug.Log($"{itemSlots[i].SlotItemData.itemName},{itemSlots[i].ItemCount}");

        //}
        //Debug.Log($"{itemSlots[2].SlotItemData.itemName},{itemSlots[2].ItemCount}");
        //itemSlots[1].AssignSlotItem(potion,4);
        //itemSlots[3].AssignSlotItem(potion,3);

        //Debug.Log($" 3�� ������ {itemSlots[3].SlotItemData.itemName},{itemSlots[3].ItemCount}��");
        ////1�������� {itemSlots[1].SlotItemData.itemName},{itemSlots[1].ItemCount}�� ,
        //MoveSlotItem(itemSlots[1], itemSlots[3]);
        //Debug.Log($" 3�� ������ {itemSlots[3].SlotItemData.itemName},{itemSlots[3].ItemCount}��");
        ////---------------------------------------------
    }


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

    public void ClearInven()    //Ȯ�� �Ϸ�, ��� ���� ���
    {
        foreach(ItemSlot itemSlot in itemSlots)
        {
            itemSlot.SlotItemData = null;
            itemSlot.ItemCount = 0;
        }
    }

    public void DropItem(ItemSlot dropItemSlot,uint dropCount)  //Ư�� ���� ������ �ʵ�� ������
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

    //���� ������ ���� �� ���� ���� ���� ���� �Ǵ� ����ִ� ���� ����
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

    //�������� �κ��丮�� ������ �˸��� ������ ã�� �Ҵ��� �ִ� �Լ�
    public void TakeItem(ItemData takeItemData, uint count)
    {
        //�������� ������ NULL reference�� �ߴµ� findsameitemslot�Լ����� null���� �����ؼ� �ű⼭�� slotItemData�� ������ ���� ������ ���µ�, ������ �ϴ� �۵��� �ߵż� ���߿� ������ ��
        if(FindSameItemSlotForAddItem(takeItemData).SlotItemData != null) //�����Ͱ� null�� �ƴ϶�� => �����Ͱ� ������� ����.
        {
            FindSameItemSlotForAddItem(takeItemData).IncreaseSlotItem(count);
        }
        else if(FindSameItemSlotForAddItem(takeItemData).SlotItemData == null)  //�����Ͱ� null�϶�
        {
            FindSameItemSlotForAddItem(takeItemData).AssignSlotItem(takeItemData, count);
        }
        else //����ִ� ������ ã�����ߴٸ� ã�������� ǥ��
        {
            Debug.Log("�κ��丮�� �Ҵ��Ҽ�����");
        }
        //else if(FindSameItemSlot(takeItemData) == null)  //����ִ� ������ ã�����ߴٸ� ã�������� ǥ��
        //{
        //    Debug.Log("�κ��丮�� �� ���ִ�");
        //}
    }

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


    //public void aaa()
    //{ 
    //    try { } //�߰�ȣ �ȿ��� �Լ��� ������
    //    catch (System.Exception e)  //������ ����
    //    {
    //        e.ToString();
    //    }
    //}

}