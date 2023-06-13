using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestUI : BasicUIForm_Parent
{
    public override PlayerInput Input_Control { get; set; }
    public override CanvasGroup CanvasGroupOnOff { get; set; }
    public override bool IsUIOnOff { get; set; }
    public override RectTransform RectTransform_UI { get; set; }
    public override Player Player { get; set; }
    public override UI_Player_MoveOnOff UI_OnOff { get; set; }



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
