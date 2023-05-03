using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class InfoUI_Parent : MonoBehaviour
{
    public abstract TextMeshProUGUI InfoName { get; set; }
    public abstract TextMeshProUGUI ItemInformation { get; set; }
    public abstract CanvasGroup InfoCanvasGroup { get; set; }
    public abstract RectTransform InfoTransform { get; set; }
    public abstract Image InfoImage   { get; set; }

    public virtual void SetInfo(ItemData itemData, Vector3 infoPos) { }

    public virtual void SetInfo(SkillData skillData, Vector3 infoPos) { }

}
