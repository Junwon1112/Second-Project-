using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSlotUIs_Create : MonoBehaviour
{
    [SerializeField]
    GameObject questSlotUI;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateQuestSlot(Quest quest)
    {
        GameObject tempObj = Instantiate(questSlotUI, transform);
        TextMeshProUGUI questContents = tempObj.GetComponent<TextMeshProUGUI>();
        if(quest.questType == QuestType.MonsterHunt)
        {
            QuestData_HuntMonster tempData = (QuestData_HuntMonster)quest.questData;

            questContents.text = $"{tempData.monstersName} :   / {tempData.requireHuntCounts}";
        }
        
    }
}
