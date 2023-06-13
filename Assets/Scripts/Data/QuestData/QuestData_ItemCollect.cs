using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Data_ItemCollect", menuName = "Scriptable Object_QuestData/QuestData_ItemCollect", order = 1)]
public class QuestData_ItemCollect : QuestData
{
    public int[] itemsID;
    public int[] requireItemCounts;
    //public int[] currentItemCounts;
    


    public override void QuestCheck(int itemID, int getCount = 1)
    {
        for(int i = 0; i < itemsID.Length; i++)
        {
            if(itemsID[i] == itemID)
            {
                //currentItemCounts[i] += getCount;
            }
        }
        

    }

    //public override void AssignQuest()
    //{
    //    QuestContents();    
    //}

}
