using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WarningText data", menuName = "Scriptable Object_WarningText Data/WarningText Data", order = 1)]
public class WarningTextData : ScriptableObject
{
    public string warningText;
    public WarningTextName warningTextName;
}

public enum WarningTextName
{
    WarningText_BuyError = 0,
    WarningText_SellError = 1,
}
