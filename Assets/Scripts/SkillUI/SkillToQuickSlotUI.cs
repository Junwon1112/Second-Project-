using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillToQuickSlotUI : BasicUIForm_Parent
{
    
    //---------------������Ƽ�ִ� ���� + �߻�ȭ�Ȱ� ������ ����-------------------
    PlayerInput input_Control;
    CanvasGroup canvasGroupOnOff;
    bool isUIOnOff = true;
    UI_Player_MoveOnOff ui_OnOff;
    RectTransform rectTransform_UI;
    Player player;

    Button[] button_HotKeys;

    SkillData skillData;    //skillSlotUI���� ������ �� �Ҵ�����

    //---------------������Ƽ ���� ����----------------------
    [SerializeField]
    Sprite defaultButtonImage;
    AllQuickSlotUI allQuickSlotUI;
    

    public override PlayerInput Input_Control { get => input_Control; set => input_Control = value; }
    public override CanvasGroup CanvasGroupOnOff { get => canvasGroupOnOff; set => canvasGroupOnOff = value; }
    public override bool IsUIOnOff { get => isUIOnOff; set => isUIOnOff = value; }
    public override RectTransform RectTransform_UI { get => rectTransform_UI; set => rectTransform_UI = value; }
    public override Player Player { get => player; set => player = value; }
    public override UI_Player_MoveOnOff UI_OnOff { get => ui_OnOff; set => ui_OnOff = value; }
    public Button[] Button_HotKeys { get => button_HotKeys; set => button_HotKeys = value; }
    public SkillData SkillData { get => skillData; set => skillData = value; }

    private void Awake()
    {
        Input_Control = new PlayerInput();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        RectTransform_UI = GetComponent<RectTransform>();
        UI_OnOff = GetComponentInParent<UI_Player_MoveOnOff>();
        Button_HotKeys = transform.GetChild(2).GetComponentsInChildren<Button>();

        allQuickSlotUI = FindObjectOfType<AllQuickSlotUI>();
    }

    private void Start()
    {
        for (int i = 0; i < Button_HotKeys.Length; i++)
        {
            //�̷��� �ӽ� ���� ����� �Ű������� �־�� 0~5�� �Ҵ�ǰ�, �׳� i�� ������ 6�� 6������! �ǰ� �߿��� ���ε�
            int tempIndex = i;  
            Button_HotKeys[i].onClick.AddListener( () => AssignHotKey(tempIndex) );
        }

        IsUIOnOff = true;
    }

    private void AssignHotKey(int _index)
    {
        for(int i = 0; i < Button_HotKeys.Length; i++)
        {
            if(SkillData == allQuickSlotUI.quickSlotUIs[i].quickSlotSkillData)  //�ٸ� ���Կ� ���� ������ ������ null������ �ʱ�ȭ ����
            {
                allQuickSlotUI.quickSlotUIs[i].QuickSlotSetData();
                Button_HotKeys[i].image.sprite = defaultButtonImage;
            }
        }
        allQuickSlotUI.quickSlotUIs[_index].QuickSlotSetData(SkillData);
        Button_HotKeys[_index].image.sprite = SkillData.skillIcon;
        UIOnOffSetting();
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
            isUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            UI_OnOff.IsUIOnOff();
        }
    }
}
