using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestSlotUI : MonoBehaviour
{
    Quest quest;

    RectTransform rectTransform;

    [SerializeField]
    GameObject questTextUI;

    int questIndex;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        questIndex = QuestManager.instance.currentQuests.IndexOf(quest);
        SetQuestDatas();
    }

    private void SetQuestDatas()
    {
        if (quest.questType == QuestType.MonsterHunt)
        {
            QuestData_HuntMonster quest_Monster = (QuestData_HuntMonster)quest.questData;
            rectTransform.sizeDelta = new Vector2(500, 80 * quest_Monster.monstersData.Length);
            
            for(int i = 0; i < quest_Monster.monstersData.Length; i ++ )
            {
                GameObject questObject = Instantiate(questTextUI, transform);
                TextMeshProUGUI monster_Count = questObject.transform.GetComponent<TextMeshProUGUI>();
                
                monster_Count.text = $"{quest_Monster.monstersData[i].monsterName} : {QuestManager.instance.currentAchievement[questIndex,i]} / {quest_Monster.requireHuntCounts}" ;
            }
        }
        
    }
}
