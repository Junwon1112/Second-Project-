using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BasicUIForm_Parent : MonoBehaviour
{
    /// <summary>
    /// iŰ�� ����Ű������ ��ǲ�ý��ۿ� ����
    /// </summary>
    public abstract PlayerInput Input_Control { get; set; }
    /// <summary>
    /// ���� Ű�°� canvasGroup�� �̿��� ����
    /// </summary>
    public abstract CanvasGroup CanvasGroupOnOff { get; set; }
    /// <summary>
    /// �κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����
    /// </summary>
    public abstract bool IsUIOnOff { get; set; }
    public abstract RectTransform RectTransform_UI { get; set; }
    public abstract Player Player { get; set; }
    public abstract UI_Player_MoveOnOff UI_OnOff { get; set; }

    public abstract void UIOnOffSetting();
}
