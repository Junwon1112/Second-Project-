using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

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
    }

    //����Ʈ�� ������ ���� ����Ʈ Ŭ������ ���� �����Ϳ� ���� ��
    MonsterData monsterData;

    public int[] countQuestMonster;

    

    public void CheckMonster_Plus(QuestMonsterBook monsterBook)
    {
        monsterBook._killingCount++;
    }
}

public struct QuestMonsterBook
{
    public int _monsterID;
    public int _killingCount;
}
