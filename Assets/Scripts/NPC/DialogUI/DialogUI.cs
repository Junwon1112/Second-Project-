using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DialogUI : BasicUIForm_Parent, IPointerClickHandler, IPointerDownHandler
{
    public List<string> dialogs_Print = new List<string>();
    public string npcName;
    public int index = 0;
    public NPC npc;
    public NPC_Trigger npc_Trigger;
    public Button[] buttons;
    public bool isBasicDialog = true;
    public bool isQuestExist = false;
    public bool isQuestContinue = false;
    public bool isQuestDone = false;
    public QuestButtons questButtons;
    bool isPlayerInTrigger;

    


    public override PlayerInput Input_Control { get; set; }
    public override CanvasGroup CanvasGroupOnOff { get; set; }
    public override bool IsUIOnOff { get; set; }
    public override RectTransform RectTransform_UI { get; set; }
    public override Player Player { get; set; }
    public override UI_Player_MoveOnOff UI_OnOff { get; set; }
    public TextMeshProUGUI TextName { get; set; }
    public TextMeshProUGUI Text_Dialog { get; set; }

    public bool IsPlayerInTrigger
    {
        get { return isPlayerInTrigger; }
        set { isPlayerInTrigger = value; }
    }

    bool isEndTalk;

    private void OnEnable()
    {
        Input_Control.StoreUI_Dialog.Enable();
        Input_Control.StoreUI_Dialog.StoreUI_Dialog_OnOff.performed += OnDialogOnoff;
    }


    private void OnDisable()
    {
        Input_Control.StoreUI_Dialog.StoreUI_Dialog_OnOff.performed -= OnDialogOnoff;
        Input_Control.StoreUI_Dialog.Disable();
    }

    private void Awake()
    {
        TextName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Text_Dialog = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        Input_Control = TotalGameManager.Instance.Input;
        RectTransform_UI = GetComponent<RectTransform>();
        UI_OnOff = FindObjectOfType<UI_Player_MoveOnOff>();
        questButtons = GetComponentInChildren<QuestButtons>();
    }

    private void Start()
    {
        IsUIOnOff = true;
        IsPlayerInTrigger = false;
    }
    private void OnDialogOnoff(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsPlayerInTrigger)  //NPCƮ���� ���ζ��
        {
            UIOnOffSetting();
            PrintDialog();
            RectTransform_UI.SetAsLastSibling();
        }
    }

    public override void UIOnOffSetting()
    {
        if (IsUIOnOff)
        {
            IsUIOnOff = false;

            isBasicDialog = true;
            SetText(npc.data.npcName, npc.data.basicScript);
            isQuestExist = CheckQuestExist();

            CanvasGroupOnOff.alpha = 1;
            CanvasGroupOnOff.interactable = true;
            CanvasGroupOnOff.blocksRaycasts = true;

            SoundPlayer.Instance.PlaySound(SoundType_Effect.Sound_UI_Open);

            UI_OnOff.IsUIOnOff();
        }
        else
        {
            IsUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            SoundPlayer.Instance.PlaySound(SoundType_Effect.Sound_UI_Close);

            UI_OnOff.IsUIOnOff();
        }

    }

    /// <summary>
    /// ��ȭ ���
    /// </summary>
    public void PrintDialog()
    {
        TextName.text = npcName;
        Text_Dialog.text = dialogs_Print[index];
    }

    public void PrintNothing()
    {
        TextName.text = "";
        Text_Dialog.text = "";
    }

    /// <summary>
    /// ����� ��ȭ ����
    /// </summary>
    /// <param name="name">npc�̸�</param>
    /// <param name="dialogs">��ȭ ����</param>
    public void SetText(string name, string[] dialogs)
    {
        dialogs_Print.Clear();
        npcName = name;
        for (int i = 0; i < dialogs.Length; i++)
        {
            dialogs_Print.Add(dialogs[i]); 
        }

    }

    public void SetText(string name, List<string> dialogs)
    {
        dialogs_Print.Clear();
        npcName = name;
        for (int i = 0; i < dialogs.Count; i++)
        {
            dialogs_Print.Add(dialogs[i]);
        }

    }

    public void SetDialogClear()
    {
        isQuestContinue = false;
        isQuestDone = false;
        index = 0;
        isBasicDialog = true;
        questButtons.UIOnOffSetting();
        UIOnOffSetting();
    }

    private bool CheckQuestExist()
    {
        for (int i = 0; i < npc.data.quest.Length; i++)
        {
            if (npc.data.quest[i].requireLevel <= InGameManager.Instance.MainPlayer.level && !QuestManager.instance.CheckIsQuestConducted(npc.data.quest[i]))
            {
                return true;
            }
        }
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //1. �⺻ ��ũ��Ʈ ���
        //2. �⺻ ��ũ��Ʈ ���� �� ���� ������ ����Ʈ Ȯ�� �� �����ϸ� ����Ʈ ��ư ����, ����x��� ��ȭ ����
        //3. ��ư �����ϸ� ��ũ��Ʈ�� null�� �ǰ� ��ư Ŭ�� �� �ش��ϴ� ����Ʈ ��ȭâ�� ���
        //4. ��ȭ ���� �� ���� / ���� ��ư ����
        //5. �����ϸ� ����Ʈ ��ǥ�� ī���� ����, �����ϸ� ��ȭâ�� �׳� ����
        
        if(isBasicDialog)
        {
            if (dialogs_Print.Count-1 > index)
            {
                PrintDialog();
                index++;
            }
            else if (dialogs_Print.Count-1 <= index)
            {
                PrintDialog();
                isBasicDialog = false;
                index = -1;
            }
        }
        else
        {
            if(isQuestExist)
            {
                if (index == -1 && questButtons.IsUIOnOff)   //=>NPCƮ���ſ��� ����Ʈ�� ������ isQuestExist�� true�� ����
                {
                    PrintNothing();
                    questButtons.SetQuestButton();
                    if(questButtons.transform.childCount != 0)
                    {
                        questButtons.UIOnOffSetting();
                    }
                    else    //��� ����Ʈ�� �Ϸ����� ��� ��ȭâ�� �ݴ´�
                    {
                        index = 0;
                        isBasicDialog = true;
                        UIOnOffSetting();
                    }
                }
                else if (isQuestExist && index != -1 && dialogs_Print.Count-1 > index)  //����Ʈ ��ȭ ���� ��
                {
                    index++;
                    PrintDialog();
                }
                //������ ��ȭ�� ���
                else if (dialogs_Print.Count-1 <= index && questButtons.IsUIOnOff)  //������ ��ȭ�ΰ� && ����Ʈ��ư UI�� ó�� ������ ���ΰ�
                {
                    if (isQuestContinue)  //�������� ����Ʈ�� �ƴ϶�� ��ư ����
                    {
                        index = 0;
                        isBasicDialog = true;
                        UIOnOffSetting();
                    }
                    else if(isQuestDone)    //�������� ����Ʈ�� �Ϸ���� ���
                    {
                        questButtons.QuestDone();
                    }
                    else    //�������� ����Ʈ��� UI�� �ݴ´�
                    {
                        //����, ����
                        PrintNothing();
                        questButtons.SetAcceptDeclineButton();
                        questButtons.UIOnOffSetting();
                    }
                }
            }
            else
            {
                index = 0;
                isBasicDialog = true;
                UIOnOffSetting();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
