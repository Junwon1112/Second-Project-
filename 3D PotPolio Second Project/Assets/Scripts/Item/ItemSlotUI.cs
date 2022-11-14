using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class ItemSlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler , IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public int slotUIID = -1;   //��ü ���Կ��� ���° �������� �����ִ� �����ִ� ��, UI�� �����͸� �޴� ������ ID�� ����. �Ҵ����� ���� -1�� �Ҵ�

    public ItemData slotUIData;    //Infoâ�� ������ �� slotUI���� �̸��̳� ���� ���� ������ ������ ����ؼ� ����
    public uint slotUICount = 0;       //tempslot�� ���������� ���� ����
    uint splitCount;        //splitUI���� �����ް� ������Ƽ���� �������ִ� ������ ��ȯ

    //�� �̹����� ����� ������ �����͸� �޾��� �� �ش� �������� Icon�� �̹����� ��ȯ
    Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 
    TextMeshProUGUI itemCountText;  //UI�� �ؽ�Ʈ�� ǥ���ϸ� UGUI�� ���µ�..?
    Inventory playerInven;
    InventoryUI playerInvenUI;

    Vector2 mousePos = Vector2.zero;    //����â�̳� tempslotUI�� ���콺 ��ġ���� �������� ���콺 ������ ����


    float infoOpenTime = 1.0f;          //���콺�� �������� �÷����� �� ������ ���� ������ ����

    bool isDrag = false;
    bool isOnPointer = false;   //���콺�� �������� �ö� �ִ���
    bool isInfoOpen = false;

    ItemInfo itemInfo;  //�������� ���콺 �÷����� �� Ȱ����ų ������ ����â ��������
    SplitUI splitUI;
    
   
    private void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
        itemCountText = GetComponentInChildren<TextMeshProUGUI>();
        playerInven = FindObjectOfType<Inventory>();
        playerInvenUI = FindObjectOfType<InventoryUI>();
        itemInfo = FindObjectOfType<ItemInfo>();
        splitUI = FindObjectOfType<SplitUI>();
    }

    private void Start()
    {
        //��ü ���Լ����� �κ��丮UI���� ����
    }

    private void Update()
    {
        InfoInUpdate();
    }


    public void SetSlotWithData(ItemData itemData, uint count)  //������ �����ͷ� ����UI ���� 
    {
        if(itemData != null)    //������ �����Ͱ� �����Ѵٸ�
        {
            slotUIData = itemData;
            slotUICount = count;

            itemImage.color = Color.white;
            itemImage.sprite = itemData.itemIcon;
            itemImage.raycastTarget = true;

            itemCountText.alpha = 1.0f;
            itemCountText.text = count.ToString();
        }
        else
        {
            slotUIData = null;
            itemImage.color = Color.clear;
            itemCountText.alpha = 0;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)  //���콺�� ���Կ� �� ���� �ľ�
    {
        isOnPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)    //���콺�� ���Կ��� ���� ���� �ľ�
    {
        isOnPointer = false;
    }

    public void OnPointerMove(PointerEventData eventData)   //������ ���� 1���̻� ������ �� ������ ���� â ǥ��, ����â�� ǥ�õ� ���¿��� �����̸� ����â �ݱ�
    {
        if (!isDrag && isOnPointer && slotUIData != null) //�巡�� ���� ����(�Ϲ����� ���¿���) �������� ���� �� �����̸� �����ð� �� ����â ���� 
        {
            infoOpenTime = 1.0f;
            
            if(isInfoOpen)
            {
                itemInfo.CloseInfo();
                isInfoOpen = false;
            }
            mousePos = eventData.position;

            //GameObject objOfFindSlot = eventData.pointerCurrentRaycast.gameObject;
            //objOfFindSlot.GetComponent<ItemData>() 



            /*
             * �ڷ�ƾ�� ���콺�� �����϶����� �����ؾ� �ɶ����� �����ؾߵǼ� �δ㽺����Ƿ� update���� Ÿ��üũ
             * ���콺�� ������ ������ infoOpenTime�� 1�ʷ� �ٽ� �ʱ�ȭ
             * 0�ʰ� �Ǹ� ���� ����
             */
        }
    }

    private void SetInfo()  //����â ǥ���� �� ������ �ֵ�
    {
        itemInfo.infoTempSlotUI.itemImage.sprite = slotUIData.itemIcon;
        itemInfo.infoTransform.position = mousePos;
        itemInfo.OpenInfo();
        itemInfo.infoName.text = slotUIData.itemName;
        itemInfo.itemInformation.text = "No Information";
        isInfoOpen = true;
    }

    private void InfoInUpdate() //������Ʈ���� ������ ����â ���°��� �Լ�
    {
        if(!isDrag)
        {
            if (isOnPointer)
            {
                infoOpenTime -= Time.deltaTime;
            }
            if (isOnPointer && !isInfoOpen && infoOpenTime < 0.0f)
            {
                if (slotUIData != null)  //�����Ͱ� �־�� ǥ���Ѵ�.
                {
                    SetInfo();
                }
            }
        }
        
    }

    //Ŭ���� ������ �������� Ŭ���̴�.
    public void OnPointerClick(PointerEventData eventData)  //shift�� ���� ������ ������ ����
    {
        //shift�� �Բ� ������ �� => splitUI����, Keyboard�Լ��� inputsystem�� �־�߸� Ȱ�밡��, ������ ���߿� shiftŬ���� �Ѱ� �ƴ��� Ȯ��
        if (Keyboard.current.leftShiftKey.ReadValue() > 0 && !splitUI.splitTempSlotSplitUI.isSpliting) 
        {
            if (slotUICount < slotUIData.itemMaxCount +1 && slotUICount > 1)    //�����͸� ���������� Ȯ��
            {
                splitUI.splitItemData = slotUIData;
                splitUI.splitPossibleCount = slotUICount;   //���� ������ �������ִ� ������ ������, splitPossibleCount�� splitUI������ ���� ������ splitCount�� ��ȯ
                splitUI.takeID = slotUIID;                  //���� �ش� ������ ID�� ������ � �������� ����
                splitUI.SplitUIOpen();
                //�������

            }


        }
        else if(splitUI.splitTempSlotSplitUI.isSpliting)    //�׳� ������ �� => tempSlot�� Ȱ��ȭ �� �������� Ȯ�� && �ùٸ� ��ġ���� Ȯ��
        {
            if (slotUIData == null)
            {
                slotUIData = splitUI.splitTempSlotSplitUI.tempSlotItemData;     //tempslot�� �����͸� �Ѱܹޱ�
                slotUICount = splitUI.splitTempSlotSplitUI.tempSlotItemCount;   //tempslot�� �������� ������ �Ѱܹޱ�
                playerInven.itemSlots[this.slotUIID].AssignSlotItem(slotUIData, slotUICount);    //���� ���Կ��� �����Ϳ� ���� ����
                splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                playerInvenUI.SetAllSlotWithData(); //���ιٲﰪ ���ΰ�ħ
            }
            else if(slotUIData == splitUI.splitTempSlotSplitUI.tempSlotItemData)  //tempslot�� �����Ϳ� �������� Ȯ��
            {
                //������ ���������Űܿ��� �� ��ģ�� �ִ� �������� ���� ���
                if (splitUI.splitTempSlotSplitUI.tempSlotItemCount + slotUICount < splitUI.splitTempSlotSplitUI.tempSlotItemData.itemMaxCount)   
                {
                    slotUICount += splitUI.splitTempSlotSplitUI.tempSlotItemCount;   //tempslot�� �������� ������ ��ġ��
                    playerInven.itemSlots[this.slotUIID].IncreaseSlotItem(splitUI.splitTempSlotSplitUI.tempSlotItemCount);    //���� ���Կ��� ���� ��ġ��
                    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                    playerInvenUI.SetAllSlotWithData(); //���ιٲﰪ ���ΰ�ħ
                }
                else //������ ���������Űܿ��� �� ��ģ�� �ִ� �������� ���� ���
                {
                    uint remainCount;   //remainCount = �ִ밪���� �ʿ��� ����
                    uint newTempSlotCount;
                    remainCount = (uint)splitUI.splitTempSlotSplitUI.tempSlotItemData.itemMaxCount - slotUICount;   //�ִ밪���� �ʿ��� ���� ����
                    newTempSlotCount = splitUI.splitTempSlotSplitUI.tempSlotItemCount - remainCount;  //�����ִ� �縸ŭ ���ְ� ������ �ش� �� ����
                    slotUICount = (uint)slotUIData.itemMaxCount;       //=> �ִ밪��ŭ ���޹޾ұ� ������ �ִ밪��ŭ ����
                    playerInven.itemSlots[this.slotUIID].IncreaseSlotItem(remainCount); //������ ���Ե� �ִ밪��ŭ ����

                    //�ִ밹���� �ְ� ���� �ִ� ���Կ� ���� ���� �����ִ� �۾�
                    playerInvenUI.slotUIs[splitUI.takeID].slotUICount += newTempSlotCount;      //�����ִ� ����UI�� ���� ���� ������
                    playerInven.itemSlots[splitUI.takeID].IncreaseSlotItem(newTempSlotCount);   //�����ִ� ���Կ� ���� ���� ������
                    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                    playerInvenUI.SetAllSlotWithData(); //���ιٲﰪ ���ΰ�ħ

                }


            }


            //1. �󽽷� �ϰ�� �׳� �ֱ�
            //2. ���� ������ Ÿ���̰� ������ �� maxcount�� �ȳѴ� ���
            //3. ���� ������ Ÿ���̰� ������ �� maxcount�� �Ѵ� ��� -> ���� �� ���� �������� ������.
            //4. �ٸ� ��� �ƹ��ϵ� �Ͼ�� �ʴ´�.

            //���� ���� : �������� �ѹ� ������ �Ҵ��ϴ°� �����ѵ� �������ŷ� �ٽ� ������ ���� ���ø� â����(������� �ߵ�) ok������ ������
            //�ذ�, �ʱ�ȭ �Ҷ� Image�� null�� ���� ���信������
        }


        /*
         * -���� �۾��� ���Ͽ�-
         * splitUI�� ų���� Shift�� �Բ� �������Ѵ�.
         * �׳� Ŭ���� ���� SplitUI���� OK�� ���� �� �ٸ� ������ �����ؾ��ϴ� ��Ȳ���� Ȯ���ϰ� 
         * �ƴ϶�� �ƹ��ϵ� �Ͼ�� �ʰ� 
         * �´ٸ� Ŭ���� ��ġ�� ������ �������� Ȯ���ϰ� �󽽷��̰ų� ���� �������� Ȯ���Ѵ�. 
         * ���� �߸��� �����̰ų� �߸��� ��ġ�� ������ ���� ���Կ� ��������.
         * ������ ������ ������ �������� ������ ���� �������� ���� �Ҵ��ϰų� �߰��Ѵ�.   
         * 
         * -SplitUI�� ����-
         * Ŭ���ϸ� �ش� ������ ������ ������ 2�̻� ���� Ȯ���ϰ� �´ٸ� SplitUI�� Ŀ����ġ�� �����Ų��.
         * ��ǲ �ʵ�� 1�̻� ������ ���� ������ ������ ������ �ȴ�.
         * ok��ư�� ������ SplitUI�� �ݰ� ���� �����Ϳ��� �ش� �ϴ� ����ŭ�� ������ ���� �ش��ϴ� ������ �����͸� ���� tempSlotUI�� �����.
         * cancelButton�� ������ SplitUI�� �ݴ´�.
         */
    }



    public void OnBeginDrag(PointerEventData eventData)     //������ �̵��̳� ��� ���� �ϱ����� �巡�� ���� ���� 
    {
        isDrag = true;
        //���� �巡�� �����ε� Infoâ�̶ߴ� ���� �ذ��ؾ���

        /*
         * �巡�׸� �����ϸ� tempslotUI�� �巡�׸� �����Ѱ��� �̹����� �����ͼ� ���콺 ��ġ�� update�ȴ�.
         */
    }

    public void OnEndDrag(PointerEventData eventData)       //������ �̵��̳� ��� ���� �ϱ� ���� �巡�� ���� ����
    {
        isDrag = false;

        /*
         * 1.
         * ���� tempslotUI�� �̹����� �巡�װ� ������ ���� �̹����� ���ٸ� ������ ������ Ȯ���ؼ� �������� �̵���Ų��.
         * ���� �̵���Ų ������ ������ ���� �ִ� �������� ���ٸ� ��� �̵���ų�� �����ϴ� â�� ����� ���������� �ִ밹������ ���� �����ϰ� �����.
         * �̵���Ų �����۰����� ���� �ִ밹������ ���ٸ� ����� �ʰ� �ٷ� �̵��Ѵ�
         * ������ ������ �����ִٸ� ��ġ�� �ٲ۴�.
         * 
         * 2.���� tempslotUI�� �̹����� �巡�װ� ������ ���� �̹����� �ٸ��� NULL�� �ƴ϶�� �������� ��ġ�� �ٲ۴�.
         *3.���� ������ ���� �̹����� NULL�̶�� �������� �ش� ��ġ�� �̵��Ѵ�.
         *4.���� ������ ���� ������ �ƴϰ� �κ��丮�� ���� �׵θ���� �ƹ��ϵ� �Ͼ�� �ʴ´�.
         *5.���� ������ ���� �����̳� �κ��丮�� ���� �׵θ��� �ƴ϶��(�ܺζ��) ������ ���ø� â�� ��������� �������� ����Ѵ�.
         */
    }





}
