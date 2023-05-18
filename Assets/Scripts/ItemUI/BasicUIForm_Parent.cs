using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BasicUIForm_Parent : MonoBehaviour
{
    /// <summary>
    /// i키로 껐다키기위한 인풋시스템용 변수
    /// </summary>
    public abstract PlayerInput Input_Control { get; set; }
    /// <summary>
    /// 껐다 키는걸 canvasGroup을 이용한 변수
    /// </summary>
    public abstract CanvasGroup CanvasGroupOnOff { get; set; }
    /// <summary>
    /// 인벤토리가 꺼져있는지 켜져있는지 확인하기 위한 변수
    /// </summary>
    public abstract bool IsUIOnOff { get; set; }
    public abstract RectTransform RectTransform_UI { get; set; }
    public abstract Player Player { get; set; }
    public abstract UI_Player_MoveOnOff UI_OnOff { get; set; }

    public abstract void UIOnOffSetting();
}
