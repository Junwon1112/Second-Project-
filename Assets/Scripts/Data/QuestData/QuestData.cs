using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class QuestData : ScriptableObject
{
    public abstract void QuestCheck(int id, int count = 1);
    //public abstract void AssignQuest();
}
