using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance;

    public MonsterData[] monsterDatas;
    public int[] huntCounts;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }



    private void Start()
    {
        huntCounts = new int[monsterDatas.Length];
    }

    //public void CheckMonster_Plus(QuestMonsterBook monsterBook)
    //{
    //    monsterBook._killingCount++;
    //}
}
