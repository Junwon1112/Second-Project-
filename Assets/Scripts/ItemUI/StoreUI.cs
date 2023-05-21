using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : BasicUIForm_Parent
{
    public static StoreUI instance;
    private Merchant merchant;
    private Merchant_Trigger merchant_Trigger;

    [SerializeField]
    GameObject storeSlotUIObjects;

    /// <summary>
    /// iŰ�� ����Ű������ ��ǲ�ý��ۿ� ����
    /// </summary>
    PlayerInput input_Control;
    /// <summary>
    /// ���� Ű�°� canvasGroup�� �̿��� ����
    /// </summary>
    CanvasGroup canvasGroupOnOff;
    /// <summary>
    /// �κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����
    /// </summary>
    bool isUIOnOff;

    RectTransform rectTransform_UI;     //StoreUI�� ��Ʈ Ʈ������

    Player player;
    UI_Player_MoveOnOff ui_OnOff;

    private RectTransform storeSlotUIs_Rect;    //���� UI���� �θ��� ��Ʈ Ʈ������

    public override PlayerInput Input_Control { get => input_Control; set => input_Control = value; }
    public override CanvasGroup CanvasGroupOnOff { get => canvasGroupOnOff; set => canvasGroupOnOff = value; }
    public override bool IsUIOnOff { get => isUIOnOff; set => isUIOnOff = value; }
    public override RectTransform RectTransform_UI { get => rectTransform_UI; set => rectTransform_UI = value; }
    public override Player Player { get => player; set => player = value; }
    public override UI_Player_MoveOnOff UI_OnOff { get => ui_OnOff; set => ui_OnOff = value; }

    private void OnEnable()
    {
        input_Control.StoreUI.Enable();
        input_Control.StoreUI.StoreUIOnOff.performed += OnStoreUIOnoff;
    }


    private void OnDisable()
    {
        input_Control.StoreUI.StoreUIOnOff.performed -= OnStoreUIOnoff;
        input_Control.StoreUI.Disable();
    }

    private void Awake()
    {
        Initialize();

        Input_Control = new PlayerInput();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        RectTransform_UI = GetComponent<RectTransform>();
        UI_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();

        merchant = FindObjectOfType<Merchant>();
        merchant_Trigger = FindObjectOfType<Merchant_Trigger>();
        storeSlotUIs_Rect = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
    }


    private void Start()
    {
        Player = GameManager.Instance.MainPlayer;
        SetStoreSlotUIs();
    }

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// GŰ�� �̿��� ����� UI�� Ŵ, (���ξտ�����)
    /// </summary>
    /// <param name="obj"></param>
    private void OnStoreUIOnoff(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(merchant_Trigger.IsPlayerInTrigger)  //����Ʈ���� ���ζ��
        {
            UIOnOffSetting();
            RectTransform_UI.SetAsLastSibling();
        }
    }

    private void SetStoreSlotUIs()
    {
        SetStoreSlotUIsHeight();

        for(int i = 0; i < merchant.sellingItems.Length; i++)
        {
            GameObject slotUIObj = Instantiate(storeSlotUIObjects, storeSlotUIs_Rect);
            StoreSlotUI_Buy storeSlotUI = slotUIObj.GetComponentInChildren<StoreSlotUI_Buy>();
            storeSlotUI.ItemData = merchant.sellingItems[i];
            storeSlotUI.SetItem(storeSlotUI.ItemData.itemIcon, storeSlotUI.ItemData.itemName, storeSlotUI.ItemData.itemValue);
        }
    }

    private void SetStoreSlotUIsHeight()
    {
        float height = merchant.sellingItems.Length * 200.0f + 150;
        storeSlotUIs_Rect.sizeDelta = new Vector2(storeSlotUIs_Rect.rect.width, height); 
    }

    private void OnLevelWasLoaded(int level)
    {
        merchant = FindObjectOfType<Merchant>();
        SetStoreSlotUIs();
    }

    public override void UIOnOffSetting()
    {
        if (IsUIOnOff)
        {
            IsUIOnOff = false;

            CanvasGroupOnOff.alpha = 1;
            CanvasGroupOnOff.interactable = true;
            CanvasGroupOnOff.blocksRaycasts = true;

            UI_OnOff.IsUIOnOff();
        }
        else
        {
            IsUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            UI_OnOff.IsUIOnOff();
        }
    }
}
