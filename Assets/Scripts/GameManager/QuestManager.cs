using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public bool isQuestComplete = false;

    [SerializeField]
    public List<Quest> quests = new List<Quest>();

    public List<Quest> currentQuests = new List<Quest>();
    public List<int> currentAchievement = new List<int>();

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

        for(int i = 0; i < quests.Count; i++)
        {
            questsDict.Add((int)quests[i].questID, quests[i]);
        }


        //questBook = new QuestMonsterBook[15];
    }

    

    public void CheckQuest_Monster(int monsterID)
    {


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
