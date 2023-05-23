using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// ������ ���� UI�� ���õ� �޼���
/// </summary>
public class ItemSlotUI : ItemSlotUI_Basic, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler , IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    /// <summary>
    /// ��ü ���Կ��� ���° �������� �����ִ� �����ִ� ��, UI�� �����͸� �޴� ������ ID�� ����. �Ҵ����� ���� -1�� �Ҵ�
    /// </summary>
    public int slotUIID = -1;

    /// <summary>
    /// Infoâ�� ������ �� slotUI���� �̸��̳� ���� ���� ������ ������ ����ؼ� ����
    /// </summary>
    ItemData itemData;

    /// <summary>
    /// tempslot�� ���������� ���� ����
    /// </summary>
    uint slotUICount = 0;       
    uint splitCount;        //splitUI���� �����ް� ������Ƽ���� �������ִ� ������ ��ȯ

    //�� �̹����� ����� ������ �����͸� �޾��� �� �ش� �������� Icon�� �̹����� ��ȯ
    Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 
    TextMeshProUGUI itemCountText;  //UI�� �ؽ�Ʈ�� ǥ���ϸ� UGUI�� ���
    Inventory playerInven;
    InventoryUI playerInvenUI;

    /// <summary>
    /// ����â�̳� tempslotUI�� ���콺 ��ġ���� �������� ���콺 ������ ����
    /// </summary>
    Vector2 mousePos = Vector2.zero;

    /// <summary>
    /// ���콺�� �������� �÷����� �� ������ ���� ������ ����
    /// </summary>
    float infoOpenTime = 1.0f;          

    bool isDrag = false;
    bool isOnPointer = false;   //���콺�� �������� �ö� �ִ���

    ItemInfoUI itemInfo;  //�������� ���콺 �÷����� �� Ȱ����ų ������ ����â ��������
    SplitUI splitUI;
    DropUI dropUI;
    
    TempSlotSplitUI tempSlotSplitUI;

    public override Image ItemImage { get => itemImage; set => itemImage = value; }
    public override ItemData ItemData { get => itemData; set => itemData = value; }
    public override uint SlotUICount { get => slotUICount; set => slotUICount = value; }


    private void Awake()
    {
        ItemImage = GetComponentInChildren<Image>();
        itemCountText = GetComponentInChildren<TextMeshProUGUI>();
        itemInfo = transform.parent.parent.parent.GetComponentInChildren<ItemInfoUI>();
        splitUI = GameObject.Find("SplitUI").GetComponent<SplitUI>();  //dropUI�� splitUI�� ��ӹ޾Ҵµ� findobjectoftype���� �������� dropUI�� �޾ƿü����ִ�.
        dropUI = GameObject.Find("DropUI").GetComponent<DropUI>();
        //tempSlotSplitUI = FindObjectOfType<TempSlotSplitUI>();    //��Ȱ��ȭ üũ�س����� Awake���� ã�Ƶ� ��ã�´�. ��Ȱ��ȭ Ÿ�̹��� Awake���� ������ ����. �׷��� �Ʒ�ó�� ã�´�. 
        tempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //Ȱ��ȭ�� ������Ʈ ã���� ������ �����ϰ�
    }

    private void Start()
    {
        playerInven = InGameManager.Instance.MainPlayer.transform.GetComponentInChildren<Inventory>();
        playerInvenUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
    }

    private void Update()
    {
        InfoInUpdate();
    }

    /// <summary>
    /// ������ �����ͷ� ����UI ���� 
    /// </summary>
    /// <param name="itemData">������ ������ ������</param>
    /// <param name="count">������ ����</param>
    public void SetSlotWithData(ItemData itemData, uint count)  
    {
        if(itemData != null && count > 0)    //������ �����Ͱ� �����Ѵٸ�
        {
            this.itemData = itemData;
            SlotUICount = count;

            ItemImage.color = Color.white;
            ItemImage.sprite = itemData.itemIcon;

            ItemImage.raycastTarget = true;
            itemCountText.alpha = 1.0f;
            itemCountText.text = count.ToString();
        }
        else
        {
            this.itemData = null;
            SlotUICount = count;
            ItemImage.color = Color.clear;
            itemCountText.alpha = 0;
        }
        
    }
    /// <summary>
    /// ���콺�� ���Կ� �� ���� �ľ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) 
    {
        isOnPointer = true;
    }

    /// <summary>
    /// ���콺�� ���Կ��� ���� ���� �ľ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)    
    {
        isOnPointer = false;
    }

    /// <summary>
    /// ������ ���� 1���̻� ������ �� ������ ���� â ǥ��, ����â�� ǥ�õ� ���¿��� �����̸� ����â �ݱ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerMove(PointerEventData eventData)  
    {
        if (!isDrag && isOnPointer && ItemData != null) //�巡�� ���� ����(�Ϲ����� ���¿���) �������� ���� �� �����̸� �����ð� �� ����â ���� 
        {
            infoOpenTime = 1.0f;
            
            if(itemInfo.isInfoOpen)
            {
                itemInfo.CloseInfo();
                itemInfo.isInfoOpen = false;
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

    /// <summary>
    /// ����â ǥ���� �� ������ �ֵ�
    /// </summary>
    //private void SetInfo()  
    //{
    //    itemInfo.InfoTempSlotUI.itemImage.sprite = slotUIData.itemIcon;
    //    itemInfo.infoTransform.position = mousePos;
    //    itemInfo.OpenInfo();
    //    itemInfo.infoName.text = slotUIData.itemName;
    //    itemInfo.itemInformation.text = "No Information";
    //    isInfoOpen = true;
    //}

    /// <summary>
    /// ������Ʈ���� ������ ����â ���°��� �Լ�
    /// </summary>
    private void InfoInUpdate() 
    {
        if(!isDrag)
        {
            if (isOnPointer)
            {
                infoOpenTime -= Time.deltaTime;
            }
            if (isOnPointer && !itemInfo.isInfoOpen && infoOpenTime < 0.0f)
            {
                if (ItemData != null)  //�����Ͱ� �־�� ǥ���Ѵ�.
                {
                    itemInfo.SetInfo(ItemData, mousePos);
                }
            }
        }
        
    }

    /// <summary>
    /// Ŭ���� ������ �������� Ŭ���̴�, ������ Ŭ���� �����ų �޼���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)  
    {
        //shift�� ���� ������ ������ ����
        //shift�� �Բ� ������ �� => splitUI����, Keyboard�Լ��� inputsystem�� �־�߸� Ȱ�밡��, ������ ���߿� shiftŬ���� �Ѱ� �ƴ��� Ȯ��
        if (Keyboard.current.leftShiftKey.ReadValue() > 0 && !splitUI.isSplitting) 
        {
            if (SlotUICount < ItemData.itemMaxCount +1 && SlotUICount > 1)    //�����͸� ���������� Ȯ��
            {
                splitUI.ItemData = ItemData;
                splitUI.splitPossibleCount = SlotUICount;   //���� ������ �������ִ� ������ ������, splitPossibleCount�� splitUI������ ���� ������ splitCount�� ��ȯ
                splitUI.TakeID = slotUIID;                  //���� �ش� ������ ID�� ������ � �������� ����
                splitUI.NumUIOpen();
                splitUI.RectTransform.SetAsLastSibling();
                //�������

            }

        }
        //------------------Split�� OK�� ���� ���콺�� TempSlot�� Ȱ��ȭ�� ����----------------------------------------------------
        else if(splitUI.isSplitting)    //�׳� ������ �� => tempSlot�� Ȱ��ȭ �� �������� Ȯ�� && �ùٸ� ��ġ���� Ȯ��
        {
            if (ItemData == null)
            {
                ItemData = splitUI.splitTempSlotSplitUI.ItemData;     //tempslot�� �����͸� �Ѱܹޱ�
                SlotUICount = splitUI.splitTempSlotSplitUI.SlotUICount;   //tempslot�� �������� ������ �Ѱܹޱ�
                playerInven.itemSlots[this.slotUIID].AssignSlotItem(itemData, SlotUICount);    //���� ���Կ��� �����Ϳ� ���� ����
                splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                splitUI.isSplitting = false;
                splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                playerInvenUI.SetAllSlotWithData(); //���ιٲﰪ ���ΰ�ħ
            }
            else if(ItemData == splitUI.splitTempSlotSplitUI.ItemData)  //tempslot�� �����Ϳ� �������� Ȯ��
            {
                //������ ���������Űܿ��� �� ��ģ�� �ִ� �������� ���� ���
                if (splitUI.splitTempSlotSplitUI.SlotUICount + SlotUICount < splitUI.splitTempSlotSplitUI.ItemData.itemMaxCount)   
                {
                    SlotUICount += splitUI.splitTempSlotSplitUI.SlotUICount;   //tempslot�� �������� ������ ��ġ��
                    playerInven.itemSlots[this.slotUIID].IncreaseSlotItem(splitUI.splitTempSlotSplitUI.SlotUICount);    //���� ���Կ��� ���� ��ġ��
                    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    splitUI.isSplitting = false;
                    splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                    playerInvenUI.SetAllSlotWithData(); //���ιٲﰪ ���ΰ�ħ
                }
                else //������ ���������Űܿ��� �� ��ģ�� �ִ� �������� ���� ���
                {
                    uint remainCount;   //remainCount = �ִ밪���� �ʿ��� ����
                    uint newTempSlotCount;
                    remainCount = (uint)splitUI.splitTempSlotSplitUI.ItemData.itemMaxCount - slotUICount;   //�ִ밪���� �ʿ��� ���� ����
                    newTempSlotCount = splitUI.splitTempSlotSplitUI.SlotUICount - remainCount;  //�����ִ� �縸ŭ ���ְ� ������ �ش� �� ����
                    SlotUICount = (uint)ItemData.itemMaxCount;       //=> �ִ밪��ŭ ���޹޾ұ� ������ �ִ밪��ŭ ����
                    playerInven.itemSlots[this.slotUIID].IncreaseSlotItem(remainCount); //������ ���Ե� �ִ밪��ŭ ����

                    //�ִ밹���� �ְ� ���� �ִ� ���Կ� ���� ���� �����ִ� �۾�
                    playerInvenUI.slotUIs[splitUI.TakeID].SlotUICount += newTempSlotCount;      //�����ִ� ����UI�� ���� ���� ������
                    playerInven.itemSlots[splitUI.TakeID].IncreaseSlotItem(newTempSlotCount);   //�����ִ� ���Կ� ���� ���� ������
                    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    splitUI.isSplitting = false;
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


    //------------------OnDrag�� ������� �ʴ��� �������̽� ����� �ȹ����� BiginDrag�� �۵����� �ʴ´�.-----------------------------

    /// <summary>
    /// ������ �̵��̳� ��� ���� �ϱ����� �巡�� ���� ���� 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)     
    {
        if(ItemData != null)
        {
            isDrag = true;

            //FindObjectOfType<TempSlotSplitUI>().gameObject.SetActive(true);   //��Ȱ��ȭ�ż� �ٷ� ã�°� �ȵ�, �Ʒ�ó�� �θ� ã���� �� �ڽ��� ã���������� ã�ƾ���

            GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true); //tempSlot�� ��Ȱ��ȭ ���״� �θ������Ʈ�� ���� ã�Ƽ� Ȱ��ȭ ��ų���̴�.

            tempSlotSplitUI.SetTempSlotWithData(ItemData, SlotUICount);       //�̵��� ������ tempslot�� �����ϰ�
            tempSlotSplitUI.rectTransform_TempSlotSplit.SetAsLastSibling();

            playerInven.itemSlots[slotUIID].ClearSlotItem();             //UI�� ���� �����Ϳ����� �����Ϳ� ������ ��
            SlotUICount = 0;

            playerInvenUI.SetAllSlotWithData();

        }

        /*
         * �巡�׸� �����ϸ� tempslotUI�� �巡�׸� �����Ѱ��� �̹����� �����ͼ� ���콺 ��ġ�� update�ȴ�.
         */
    }

    /// <summary>
    /// �̶� ȣ��Ǵ� OnEndDrag�Լ��� ó���� OnBiginDrag�� �ִ� ������ �Լ���.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)       //������ �̵��̳� ��� ���� �ϱ� ���� �巡�� ���� ����
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj != null)    //�ϴ� ���콺�� �� ���� ����ĳ��Ʈ�� �ִ°� = �κ��丮 ������ ���
        {
            //Debug.Log($"{obj.name}");
            ItemSlotUI targetItemSlotUI = obj.GetComponent<ItemSlotUI>();

            if (targetItemSlotUI != null) //������ ������Ʈ�� ������ ����UI�� �ִ°��̶�� = ItemSlotUI���
            {
                if(targetItemSlotUI.ItemData == null)    //���� ������ �󽽷��̶��
                {
                    targetItemSlotUI.SetSlotWithData(tempSlotSplitUI.ItemData, tempSlotSplitUI.SlotUICount);
                    playerInven.itemSlots[targetItemSlotUI.slotUIID].AssignSlotItem(tempSlotSplitUI.ItemData, tempSlotSplitUI.SlotUICount);//���� ���Կ��� �����Ϳ� ���� ����
                    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                    playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ���� �����ͷ� ������ �ٲ��ش�
                }
                else    //�󽽷��� �ƴϰ� �������� �����ϴ� �����̶��
                {
                    if(targetItemSlotUI.ItemData == tempSlotSplitUI.ItemData &&   //�ű�� ������ ������ ���� //2���� ���� maxCount���� �۰ų� ������쿣 ��ģ��
                        targetItemSlotUI.SlotUICount + tempSlotSplitUI.SlotUICount < targetItemSlotUI.ItemData.itemMaxCount +1 ) 
                    {
                        targetItemSlotUI.SlotUICount += tempSlotSplitUI.SlotUICount;
                        playerInven.itemSlots[targetItemSlotUI.slotUIID].IncreaseSlotItem(tempSlotSplitUI.SlotUICount);    //���� ���Կ��� �����Ϳ� ���� ����
                        targetItemSlotUI.SetSlotWithData(targetItemSlotUI.ItemData, targetItemSlotUI.SlotUICount);    //�ڱ� �ڽ��� ������ �ٲ������ �ش絥���ͷ� UIǥ�� �ֽ�ȭ

                        splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                        splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                        playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ����  �����ͷ� ������ �ٲ��ش�
                    }
                    // ���� �ٸ� ������ ������ ��ġ�ٲٴ� ������ ���� ���簡 �ż� ��� ������ �����Ͱ� ����ȭ �Ǿ� ��� ����
                    //���� ����� ���� �ʿ�
                    else
                    {
                        SetSlotWithData(tempSlotSplitUI.ItemData, tempSlotSplitUI.SlotUICount);
                        playerInven.itemSlots[this.slotUIID].AssignSlotItem(ItemData, SlotUICount);    //���� ���Կ��� �����Ϳ� ���� ����
                        splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                        splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                        playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ����
                    }
                    

                    //else    //�ű�� ������ ������ �ٸ� �����̰ų� ������ ���ľ��ϴµ� 2���� ���� maxCount���� ���� ��� => �����͸� ���� �ٲ۴�
                    //{
                    //    ItemData tempNewItemData = new ItemData();
                    //    tempNewItemData = targetItemSlotUI.ItemData;
                    //    uint tempCount = targetItemSlotUI.SlotUICount;

                    //    //�� �������� �ڸ� �ű��
                    //    //playerInven.itemSlots[this.slotUIID].AssignSlotItem(targetItemSlotUI.ItemData, targetItemSlotUI.SlotUICount);    //���� ���Կ� �����Ϳ� ���� ����
                    //    //SetSlotWithData(targetItemSlotUI.ItemData, targetItemSlotUI.SlotUICount);  //temp�������� �����͸� ���� ����ִ� �ڱ� �ڽſ� ��� ������ �� ���� �Ҵ�

                    //    playerInven.itemSlots[this.slotUIID].AssignSlotItem(tempNewItemData, tempCount);    //���� ���Կ� �����Ϳ� ���� ����
                    //    SetSlotWithData(tempNewItemData, tempCount);

                    //    //���� ���Կ� �����Ϳ� ���� ����
                    //    playerInven.itemSlots[targetItemSlotUI.slotUIID].AssignSlotItem(tempSlotSplitUI.ItemData, tempSlotSplitUI.SlotUICount);    
                    //    targetItemSlotUI.SetSlotWithData(tempSlotSplitUI.ItemData, tempSlotSplitUI.SlotUICount);
                        
                    //    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    //    splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                    //    playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ���� �����ͷ� ������ �ٲ��ش�
                    //}
                }

            }
            else    //������ ����UI�� �ƴ϶��
            {
                SetSlotWithData(tempSlotSplitUI.ItemData, tempSlotSplitUI.SlotUICount);
                playerInven.itemSlots[this.slotUIID].AssignSlotItem(ItemData, SlotUICount);    //���� ���Կ��� �����Ϳ� ���� ����
                splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ����
            }
        }
        else //raycast�޴� ���� ������Ʈ�� �ƿ����� �� => Inventory�ۿ� ������ �� = ������ ���
        {

            dropUI.ItemData = tempSlotSplitUI.ItemData;
            dropUI.splitPossibleCount = tempSlotSplitUI.SlotUICount;   //���� ������ �������ִ� ������ ������, splitPossibleCount�� splitUI������ ���� ������ splitCount�� ��ȯ
            dropUI.TakeID = slotUIID;                                      //���� �ش� ������ ID�� ������ � �������� ����
            splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
            splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����
            
            dropUI.NumUIOpen();
            dropUI.RectTransform.SetAsLastSibling();
            //�������

            
        }




        //Debug.Log($"{slotUIID}");



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
         *4.���� ������ ���� ������ �ƴϰ� �κ��丮�� ���� �׵θ���� �ƹ��ϵ� �Ͼ�� �ʴ´�.(���� ������ġ�� ���ư���.)
         *5.���� ������ ���� �����̳� �κ��丮�� ���� �׵θ��� �ƴ϶��(�ܺζ��) ������ ���ø� â�� ��������� �������� ����Ѵ�.
         */
    }

    /// <summary>
    /// onbigindrag�� onenddrag�� �۵��Ǳ� ���ؼ��� ondrag�� �ʿ��ϴ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        
    }


}
