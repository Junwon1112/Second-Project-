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

    DialogUI dialogUI;

    public NPC npc;

    QuestSlotUIs_Create questCreate;

    int choiceIndex;

    public List<string> tempDialog = new List<string>();
    string tempName;
    Quest tempQuest;
    public bool IsUIOnOff { get; set; }
    CanvasGroup CanvasGroupOnOff { get; set; }

    private void Awake()
    {
        dialogUI = FindObjectOfType<DialogUI>();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        questCreate = FindObjectOfType<QuestSlotUIs_Create>();
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

            if (dialogUI.npc.data.quest[value].requireLevel <= InGameManager.Instance.MainPlayer.level)
            {
                
                GameObject questObj = Instantiate(this.questButton, transform);
                Button questButton = questObj.transform.GetComponent<Button>();
                questButton.onClick.AddListener(() => { StartQuest(npc.data.npcName, npc.data.quest[value].dialog, npc.data.quest[value]); });

                TextMeshProUGUI buttonName = questObj.transform.GetComponentInChildren<TextMeshProUGUI>();
                buttonName.text = npc.data.quest[i].questName;
            }


        }
    }
    private void StartQuest(string npc_Name, List<string> dialog, Quest quest)
    {
        dialogUI.SetText(npc_Name, dialog);
        tempQuest = quest;
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

        dialogUI.index = 0;
        UIOnOffSetting();
    }

    public void QuestDecline()
    {
        dialogUI.index = 0;
        UIOnOffSetting();
        dialogUI.UIOnOffSetting();
    }
}
