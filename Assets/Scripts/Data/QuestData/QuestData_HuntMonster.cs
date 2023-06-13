using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Data_HuntMonster", menuName = "Scriptable Object_QuestData/QuestData_HuntMonster", order = 1)]
public class QuestData_HuntMonster : QuestData
{
    public string[] monstersName;
    public int[] monstersID;
    public int[] requireHuntCounts;
    //public int[] currentHuntCounts;
    


    public override void QuestCheck(int monsterID, int huntCount = 1)
    {
        for(int i = 0; i < monstersID.Length; i++)
        {
            if(monstersID[i] == monsterID)
            {
             //   currentHuntCounts[i] += huntCount;
            }
        }
        

    }

    //public override void AssignQuest()
    //{
    //    QuestContents();    
    //}

}
