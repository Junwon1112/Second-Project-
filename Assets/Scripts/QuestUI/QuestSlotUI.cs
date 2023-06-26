using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestSlotUI : MonoBehaviour
{
    public Quest quest;

    RectTransform rectTransform;

    [SerializeField]
    GameObject questTextUI;

    TextMeshProUGUI[] monsters_Count;

    public int questIndex;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //questIndex = QuestManager.instance.currentQuests.IndexOf(quest);
        //SetQuestDatas();
    }

    public void SetQuestDatas()
    {
        if (quest.questType == QuestType.MonsterHunt)
        {
            QuestData_HuntMonster quest_Monster = (QuestData_HuntMonster)quest.questData;
            rectTransform.sizeDelta = new Vector2(500, 80 * quest_Monster.monstersData.Length);
            
            monsters_Count = new TextMeshProUGUI[quest_Monster.monstersData.Length];

            for(int i = 0; i < quest_Monster.monstersData.Length; i ++ )
            {
                int index = i;
                GameObject questObject = Instantiate(questTextUI, transform);
                monsters_Count[index] = questObject.transform.GetComponent<TextMeshProUGUI>();

                monsters_Count[index].text = $"{quest_Monster.monstersData[index].monsterName} : " +
                    $"{QuestManager.instance.currentAchievement[questIndex,index]} / {quest_Monster.requireHuntCounts[index]}" ;
            }
        }
        
    }

    public void SetQuestText(int index)
    {
        if (quest.questType == QuestType.MonsterHunt)
        {
            QuestData_HuntMonster quest_Monster = (QuestData_HuntMonster)quest.questData;

            monsters_Count[index].text = $"{quest_Monster.monstersData[index].monsterName} : " +
                    $"{QuestManager.instance.currentAchievement[questIndex, index]} / {quest_Monster.requireHuntCounts[index]}";
        }
    }
}
