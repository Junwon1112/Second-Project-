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
        if (IsPlayerInTrigger)  //NPC트리거 내부라면
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
    /// 대화 출력
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
    /// 출력할 대화 세팅
    /// </summary>
    /// <param name="name">npc이름</param>
    /// <param name="dialogs">대화 내용</param>
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
        //1. 기본 스크립트 출력
        //2. 기본 스크립트 종료 후 수행 가능한 퀘스트 확인 후 존재하면 퀘스트 버튼 생성, 존재x라면 대화 종료
        //3. 버튼 생성하며 스크립트는 null이 되고 버튼 클릭 후 해당하는 퀘스트 대화창을 띄움
        //4. 대화 종료 후 수락 / 거절 버튼 생성
        //5. 수락하면 퀘스트 목표를 카운팅 시작, 거절하면 대화창을 그냥 닫음
        
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
                if (index == -1 && questButtons.IsUIOnOff)   //=>NPC트리거에서 퀘스트가 있으면 isQuestExist를 true로 만듬
                {
                    PrintNothing();
                    questButtons.SetQuestButton();
                    if(questButtons.transform.childCount != 0)
                    {
                        questButtons.UIOnOffSetting();
                    }
                    else    //모든 퀘스트를 완료했을 경우 대화창을 닫는다
                    {
                        index = 0;
                        isBasicDialog = true;
                        UIOnOffSetting();
                    }
                }
                else if (isQuestExist && index != -1 && dialogs_Print.Count-1 > index)  //퀘스트 대화 진행 중
                {
                    index++;
                    PrintDialog();
                }
                //마지막 대화일 경우
                else if (dialogs_Print.Count-1 <= index && questButtons.IsUIOnOff)  //마지막 대화인가 && 퀘스트버튼 UI가 처음 켜지는 것인가
                {
                    if (isQuestContinue)  //진행중인 퀘스트가 아니라면 버튼 생성
                    {
                        index = 0;
                        isBasicDialog = true;
                        UIOnOffSetting();
                    }
                    else if(isQuestDone)    //진행중인 퀘스트가 완료됐을 경우
                    {
                        questButtons.QuestDone();
                    }
                    else    //진행중인 퀘스트라면 UI를 닫는다
                    {
                        //수락, 거절
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
