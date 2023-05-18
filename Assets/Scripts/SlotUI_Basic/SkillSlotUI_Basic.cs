using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillSlotUI_Basic : MonoBehaviour
{
    public abstract Image SkillImage { get; set; }
    public abstract SkillData SkillData { get; set; }
}
