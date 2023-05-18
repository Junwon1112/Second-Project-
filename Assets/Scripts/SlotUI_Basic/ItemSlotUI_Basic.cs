using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlotUI_Basic : MonoBehaviour
{
    public abstract Image ItemImage { get; set; }
    public abstract ItemData ItemData { get; set; }
    public abstract uint SlotUICount { get; set; }
}
