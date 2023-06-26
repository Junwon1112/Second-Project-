using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSlotUIs_Create : MonoBehaviour
{
    [SerializeField]
    GameObject questSlotUI;


    public void CreateQuestSlot(Quest quest)
    {
        if(transform.childCount == 0)
        {
            GameObject obj = Instantiate(questSlotUI, transform);
            QuestSlotUI tempQuestSlotUI = obj.GetComponent<QuestSlotUI>();
            tempQuestSlotUI.quest = quest;
            tempQuestSlotUI.questIndex = QuestManager.instance.currentQuests.IndexOf(quest);
            tempQuestSlotUI.SetQuestDatas();
            int firstSlotPostion_y = 100;

            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, firstSlotPostion_y, obj.transform.localPosition.z);
        }
        else
        {
            int childNum = transform.childCount;
            Vector3 finalPosition = transform.GetChild(childNum-1).localPosition;
            GameObject obj = Instantiate(questSlotUI, transform);
            QuestSlotUI tempQuestSlotUI = obj.GetComponent<QuestSlotUI>();
            tempQuestSlotUI.quest = quest;
            tempQuestSlotUI.questIndex = QuestManager.instance.currentQuests.IndexOf(quest);
            tempQuestSlotUI.SetQuestDatas();

            float slotInterval = 100.0f;

            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, finalPosition.y - slotInterval, obj.transform.localPosition.z);
        }

        //TextMeshProUGUI questContents = tempObj.GetComponent<TextMeshProUGUI>();
        //if(quest.questType == QuestType.MonsterHunt)
        //{
        //    QuestData_HuntMonster tempData = QuestManager.instance.cu;

        //    for(int i = 0; i < tempData.monstersName.Length; i++)
        //    {
        //        questContents.text = $"{tempData.monstersName} :  / {tempData.requireHuntCounts[i]}";

        //    }
        //}
        
    }
}
