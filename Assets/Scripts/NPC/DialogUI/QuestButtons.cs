using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestButtons : MonoBehaviour
{
    [SerializeField]
    GameObject questButton;

    [SerializeField]
    GameObject acceptButton;

    [SerializeField]
    GameObject declineButton;

    public NPC npc;

    DialogUI dialogUI;
    CompensateUI compensateUI;

    QuestSlotUIs_Create questCreate;
    CompensateImages_Create compensate_ImageCreate;
    WarningUI warningUI;

    int choiceIndex;

    public List<string> tempDialog = new List<string>();
    string tempName;
    public Quest tempQuest;    //퀘스트를 시작하는 경우나 완료했을 경우 해당 퀘스트 데이터를 UI들에 전해주기 위해 존재
    public bool IsUIOnOff { get; set; }
    CanvasGroup CanvasGroupOnOff { get; set; }

    private void Awake()
    {
        dialogUI = FindObjectOfType<DialogUI>();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        questCreate = FindObjectOfType<QuestSlotUIs_Create>();
        compensate_ImageCreate = FindObjectOfType<CompensateImages_Create>();
        compensateUI = FindObjectOfType<CompensateUI>();
        warningUI = FindObjectOfType<WarningUI>();
        acceptButton.GetComponent<Button>().onClick.AddListener(QuestAccept);
        declineButton.GetComponent<Button>().onClick.AddListener(QuestDecline);
        
    }
    private void Start()
    {
        IsUIOnOff = true;
    }

    public void UIOnOffSetting()
    {
        if (IsUIOnOff)
        {
            IsUIOnOff = false;

            CanvasGroupOnOff.alpha = 1;
            CanvasGroupOnOff.interactable = true;
            CanvasGroupOnOff.blocksRaycasts = true;

            //UI_OnOff.IsUIOnOff();
        }
        else
        {
            IsUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

            ClearButtons();
            //UI_OnOff.IsUIOnOff();
        }

    }

    public void SetQuestButton()
    {
        npc = dialogUI.npc;

        for (int i = 0; i < dialogUI.npc.data.quest.Length ; i++)
        {
            int value = i;  // 얘를 안쓰고 i를 쓰면 AddListner내부의 람다식에서 정확한 i값을 인지하지 못해 에러가 남

            // 수행할 수 있는 레벨의 퀘스트인지 확인
            if (dialogUI.npc.data.quest[value].requireLevel <= InGameManager.Instance.MainPlayer.level)
            {
                bool isQuestDone;
                // 이미 진행 중인지, 처음인지, 완료인지 확인
                if(IsQuestContinue(dialogUI.npc.data.quest[value], out isQuestDone)) //진행중인 퀘스트일 경우
                {
                    if(!isQuestDone) //진행중인 퀘스트라면
                    {
                        GameObject questObj = Instantiate(this.questButton, transform);
                        Button questButton = questObj.transform.GetComponent<Button>();
                        questButton.onClick.AddListener(() => { CheckQuest(npc.data.npcName, npc.data.quest[value]); });

                        TextMeshProUGUI buttonName = questObj.transform.GetComponentInChildren<TextMeshProUGUI>();
                        buttonName.text = npc.data.quest[i].questName + "(Not Conducted)";
                    }
                    else //진행중인 퀘스트를 완료했다면
                    {
                        GameObject questObj = Instantiate(this.questButton, transform);
                        Button questButton = questObj.transform.GetComponent<Button>();

                        questButton.onClick.AddListener(() => { SuccessQuest(npc.data.npcName, npc.data.quest[value]); });

                        TextMeshProUGUI buttonName = questObj.transform.GetComponentInChildren<TextMeshProUGUI>();
                        buttonName.text = npc.data.quest[i].questName + "(Success)";
                    }
                    
                }
                else if (!IsQuestConducted(dialogUI.npc.data.quest[value]))  //진행 중x && 완료 X 일 경우(새로운 퀘스트일 경우)
                {
                    GameObject questObj = Instantiate(this.questButton, transform);
                    Button questButton = questObj.transform.GetComponent<Button>();
                    questButton.onClick.AddListener(() => { StartQuest(npc.data.npcName, npc.data.quest[value]); });

                    TextMeshProUGUI buttonName = questObj.transform.GetComponentInChildren<TextMeshProUGUI>();
                    buttonName.text = npc.data.quest[i].questName;
                }
                
            }


        }
    }

    /// <summary>
    /// 현재 진행 중인지 확인하는 메서드
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public bool IsQuestContinue(Quest quest, out bool isQuestDone)
    {
        for(int i = 0; i < QuestManager.instance.currentQuests.Count; i++)
        {
            if(QuestManager.instance.currentQuests[i] == quest)
            {
                isQuestDone = false;
                //퀘스트 타입이 몬스터인 경우
                if (quest.questType == QuestType.MonsterHunt)
                {
                    QuestData_HuntMonster questData_Monster = (QuestData_HuntMonster)QuestManager.instance.currentQuests[i].questData;

                    for (int j = 0; j < questData_Monster.requireHuntCounts.Length; j++)
                    {
                        if (QuestManager.instance.currentAchievement[i,j] >= questData_Monster.requireHuntCounts[j])
                        {
                            isQuestDone = true;
                            break;
                        }
                    }
                        
                }
                //퀘스트 타입이 아이템 수집인 경우
                else if(quest.questType == QuestType.ItemCollect)
                {
                    QuestData_ItemCollect questData_Item = (QuestData_ItemCollect)QuestManager.instance.currentQuests[i].questData;

                    isQuestDone = false;
                }
                
                return true;
            }
        }

        isQuestDone = false;
        return false;
    }



    /// <summary>
    /// 이미 퀘스트 수행 후 완료된 건지 확인
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public bool IsQuestConducted(Quest quest)
    {
        for (int i = 0; i < QuestManager.instance.conductedQuests.Count; i++)
        {
            if (QuestManager.instance.conductedQuests[i] == quest)
            {
                return true;
            }
        }

        return false;
    }
    private void StartQuest(string npc_Name, Quest quest)
    {
        dialogUI.SetText(npc_Name, quest.questConductDialog);
        tempQuest = quest;
        dialogUI.index = 0;
        dialogUI.PrintDialog();
        UIOnOffSetting();
    }

    private void CheckQuest(string npc_Name, Quest quest)
    {
        dialogUI.SetText(npc_Name, quest.questContinueDialog);
        dialogUI.isQuestContinue = true;
        dialogUI.index = 0;
        dialogUI.PrintDialog();
        UIOnOffSetting();
    }

    private void SuccessQuest(string npc_Name, Quest quest)
    {
        dialogUI.SetText(npc_Name, quest.questDoneDialog);
        tempQuest = quest;
        dialogUI.isQuestContinue = false;
        dialogUI.isQuestDone = true;
        dialogUI.index = 0;
        dialogUI.PrintDialog();
        UIOnOffSetting();
    }

    public void SetAcceptDeclineButton()
    {
        GameObject questObj_Accept = Instantiate(this.acceptButton, transform);
        Button questButton_Accept = questObj_Accept.transform.GetComponent<Button>();
        questButton_Accept.onClick.AddListener(QuestAccept);

        GameObject questObj_Decline = Instantiate(this.declineButton, transform);
        Button questButton_Decline = questObj_Decline.transform.GetComponent<Button>();
        questButton_Decline.onClick.AddListener(QuestDecline);
    }

    public void ClearButtons()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void QuestAccept()
    {
        QuestManager.instance.currentQuests.Add(tempQuest);
        questCreate.CreateQuestSlot(tempQuest);

        dialogUI.SetDialogClear();
    }

    public void QuestDecline()
    {
        dialogUI.SetDialogClear();
    }

    public void QuestDone()
    {
        //보상을 받을 수 있는 경우
        if(QuestManager.instance.CheckTakeCompensation(tempQuest))
        {
            InGameManager.Instance.MainPlayer.Exp += tempQuest.compensation_Exp;

            QuestManager.instance.conductedQuests.Add(tempQuest);
            QuestManager.instance.currentQuests.Remove(tempQuest);

            compensateUI.targetQuest = tempQuest;
            compensateUI.UIOnOffSetting();

            dialogUI.SetDialogClear();
        }
        else //보상을 받을 수 없는 경우
        {
            dialogUI.index = 0;
            dialogUI.isBasicDialog = true;
            dialogUI.questButtons.UIOnOffSetting();
            dialogUI.UIOnOffSetting();
            UIOnOffSetting();

            //퀘스트를 받을 수없다는 경고창만 띄움
            warningUI.UIOnOffSetting();
            warningUI.SetTextWarningInfo(WarningTextName.WarningText_CheckEmptySlot);
        }


        

        //보상UI생성 및 인벤토리에 아이템 추가 + 경험치
        //만약 인벤에 자리없으면 다시 퀘스트 받도록 만듬
        //퀘스트 슬롯에서 해당 슬롯 삭제
    }
}
