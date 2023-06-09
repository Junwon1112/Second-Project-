using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Scriptable Object_Quest/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public uint questID;
    public uint[] requireNPC_ID;
    public string questName;
    public int requireLevel;
    public List<string> dialog;
    public QuestData questData;
    public float compensation_Exp;
    public ItemData compensation_Item;
}
