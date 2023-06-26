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
    public Quest tempQuest;    //����Ʈ�� �����ϴ� ��쳪 �Ϸ����� ��� �ش� ����Ʈ �����͸� UI�鿡 �����ֱ� ���� ����
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
            int value = i;  // �긦 �Ⱦ��� i�� ���� AddListner������ ���ٽĿ��� ��Ȯ�� i���� �������� ���� ������ ��

            // ������ �� �ִ� ������ ����Ʈ���� Ȯ��
            if (dialogUI.npc.data.quest[value].requireLevel <= InGameManager.Instance.MainPlayer.level)
            {
                bool isQuestDone;
                // �̹� ���� ������, ó������, �Ϸ����� Ȯ��
                if(IsQuestContinue(dialogUI.npc.data.quest[value], out isQuestDone)) //�������� ����Ʈ�� ���
                {
                    if(!isQuestDone) //�������� ����Ʈ���
                    {
                        GameObject questObj = Instantiate(this.questButton, transform);
                        Button questButton = questObj.transform.GetComponent<Button>();
                        questButton.onClick.AddListener(() => { CheckQuest(npc.data.npcName, npc.data.quest[value]); });

                        TextMeshProUGUI buttonName = questObj.transform.GetComponentInChildren<TextMeshProUGUI>();
                        buttonName.text = npc.data.quest[i].questName + "(Not Conducted)";
                    }
                    else //�������� ����Ʈ�� �Ϸ��ߴٸ�
                    {
                        GameObject questObj = Instantiate(this.questButton, transform);
                        Button questButton = questObj.transform.GetComponent<Button>();

                        questButton.onClick.AddListener(() => { SuccessQuest(npc.data.npcName, npc.data.quest[value]); });

                        TextMeshProUGUI buttonName = questObj.transform.GetComponentInChildren<TextMeshProUGUI>();
                        buttonName.text = npc.data.quest[i].questName + "(Success)";
                    }
                    
                }
                else if (!IsQuestConducted(dialogUI.npc.data.quest[value]))  //���� ��x && �Ϸ� X �� ���(���ο� ����Ʈ�� ���)
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
    /// ���� ���� ������ Ȯ���ϴ� �޼���
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
                //����Ʈ Ÿ���� ������ ���
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
                //����Ʈ Ÿ���� ������ ������ ���
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
    /// �̹� ����Ʈ ���� �� �Ϸ�� ���� Ȯ��
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
        //������ ���� �� �ִ� ���
        if(QuestManager.instance.CheckTakeCompensation(tempQuest))
        {
            InGameManager.Instance.MainPlayer.Exp += tempQuest.compensation_Exp;

            QuestManager.instance.conductedQuests.Add(tempQuest);
            QuestManager.instance.currentQuests.Remove(tempQuest);

            compensateUI.targetQuest = tempQuest;
            compensateUI.UIOnOffSetting();

            dialogUI.SetDialogClear();
        }
        else //������ ���� �� ���� ���
        {
            dialogUI.index = 0;
            dialogUI.isBasicDialog = true;
            dialogUI.questButtons.UIOnOffSetting();
            dialogUI.UIOnOffSetting();
            UIOnOffSetting();

            //����Ʈ�� ���� �����ٴ� ���â�� ���
            warningUI.UIOnOffSetting();
            warningUI.SetTextWarningInfo(WarningTextName.WarningText_CheckEmptySlot);
        }


        

        //����UI���� �� �κ��丮�� ������ �߰� + ����ġ
        //���� �κ��� �ڸ������� �ٽ� ����Ʈ �޵��� ����
        //����Ʈ ���Կ��� �ش� ���� ����
    }
}
