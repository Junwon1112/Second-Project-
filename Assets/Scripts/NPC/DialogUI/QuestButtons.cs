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

    int choiceIndex;

    public List<string> tempDialog = new List<string>();
    string tempName;
    QuestData tempQuestData;
    public bool IsUIOnOff { get; set; }
    CanvasGroup CanvasGroupOnOff { get; set; }

    private void Awake()
    {
        dialogUI = FindObjectOfType<DialogUI>();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
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

            //UI_OnOff.IsUIOnOff();
        }

    }

    public void SetQuestButton()
    {
        for(int i = 0; i < dialogUI.npc.data.quest.Length ; i++)
        {
            if (dialogUI.npc.data.quest[i].requireLevel <= InGameManager.Instance.MainPlayer.level)
            {
                GameObject questObj = Instantiate(this.questButton, transform);
                Button questButton = questObj.transform.GetComponent<Button>();
                questButton.onClick.AddListener(() => { StartQuest(npc.data.npcName, npc.data.quest[i].dialog, npc.data.quest[i].questData); });

                TextMeshProUGUI buttonName = questObj.transform.GetComponentInChildren<TextMeshProUGUI>();
                buttonName.text = npc.data.quest[i].questName;
            }


        }
    }
    private void StartQuest(string npc_Name, List<string> dialog, QuestData questData)
    {
        dialogUI.SetText(npc_Name, dialog);
        tempQuestData = questData;
        dialogUI.index = 0;
        UIOnOffSetting();
    }

    public void SetAcceptDeclineButton()
    {
        Instantiate(acceptButton, transform);
        Instantiate(declineButton, transform);
    }

    public void QuestAccept()
    {


        dialogUI.index = 0;
        UIOnOffSetting();
    }

    public void QuestDecline()
    {
        dialogUI.index = 0;
        UIOnOffSetting();
    }
}
