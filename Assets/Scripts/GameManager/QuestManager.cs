using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public bool isQuestComplete = false;

    [SerializeField]
    public List<Quest> totalQuests = new List<Quest>();

    public List<Quest> currentQuests = new List<Quest>();
    public int[,] currentAchievement;

    public Dictionary<int, Quest> questsDict = new Dictionary<int, Quest>();

    //public QuestMonsterBook[] questBook;

    //퀘스트를 수락한 순간 퀘스트 클래스가 몬스터 데이터에 값을 줌
    MonsterData monsterData;

    public int[] countQuestMonster;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        for(int i = 0; i < totalQuests.Count; i++)
        {
            questsDict.Add((int)totalQuests[i].questID, totalQuests[i]);
        }


        //questBook = new QuestMonsterBook[15];
    }

    private void Start()
    {
        currentAchievement = new int[totalQuests.Count, 10];
    }

    public void CheckQuest_Monster(int monsterID)
    {
        for(int i = 0; i < currentQuests.Count; i ++)
        {
            if (currentQuests[i].questType == QuestType.MonsterHunt)
            {
                QuestData_HuntMonster questData_Monster = (QuestData_HuntMonster)currentQuests[i].questData;
                for (int j = 0; j< questData_Monster.monstersData.Length; j++)
                {
                    if (questData_Monster.monstersData[j].monsterID == monsterID)
                    {
                        currentAchievement[i, j]++;
                    }
                }
            }
        }

        //if (questsDict[questID].questType == QuestType.MonsterHunt)
        //{
            
        //}
        //else if(questsDict[questID].questType == QuestType.ItemCollect)
        //{

        //}

    }
    
}

//public struct QuestMonsterBook
//{
//    public int[] _monstersID;
//    public int _killingCount;
//    public int _requireCount;
//}
