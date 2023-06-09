using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Scriptable Object_NPC/NPC", order = 7)]
public class NPCData : ScriptableObject
{
    public string npcName;
    public uint npcID;
    public string[] basicScript;
    public Quest[] quest;
}
