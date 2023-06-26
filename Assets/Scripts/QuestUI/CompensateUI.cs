using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompensateUI : BasicUIForm_Parent
{
    Button okButton;
    TextMeshProUGUI Exp_text;
    ItemData compensation_ItemData;
    float compensation_Exp;
    public Quest targetQuest;

    CompensateImages_Create compensateImages_Create;


    public override PlayerInput Input_Control { get; set; }
    public override CanvasGroup CanvasGroupOnOff { get; set; }
    public override bool IsUIOnOff { get; set; }
    public override RectTransform RectTransform_UI { get; set; }
    public override Player Player { get; set; }
    public override UI_Player_MoveOnOff UI_OnOff { get; set; }

    private void Awake()
    {
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        compensateImages_Create = FindObjectOfType<CompensateImages_Create>();
        okButton = GetComponentInChildren<Button>();
        Exp_text = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        okButton.onClick.AddListener(UIOnOffSetting);
        IsUIOnOff = true;
    }

    public override void UIOnOffSetting()
    {
        if (IsUIOnOff)
        {
            compensateImages_Create.quest = targetQuest;
            SetCompensationUI();

            IsUIOnOff = false;

            CanvasGroupOnOff.alpha = 1;
            CanvasGroupOnOff.interactable = true;
            CanvasGroupOnOff.blocksRaycasts = true;

        }
        else
        {
            IsUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

        }
    }

    private void SetCompensationUI()
    {
        compensation_ItemData = targetQuest.compensation_Item;
        compensation_Exp = targetQuest.compensation_Exp;

        compensateImages_Create.CreateCompensationIcon();
        SetExpText();
    }

    private void SetExpText()
    {
        Exp_text.text = "Exp +" + compensation_Exp.ToString();
    }


}
