using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    ItemDataManager itemDataManager;
    SkillDataManager skillDataManager;
    //���߿� InventoryUI �߰��ؾߵ�

    public Player MainPlayer
    {
        get
        {
            return player;
        }
    }

    public ItemDataManager ItemManager
    {
        get
        {
            return itemDataManager;
        }
    }

    public SkillDataManager SkillDataManager
    {
        get
        {
            return skillDataManager;
        }
    }

    public static GameManager Instance;

    public GameManager Inst
    {
        get
        {
            return Instance;
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        Initialize();
    }


    private void Initialize()   //�����ϸ� ������Ʈ ��������
    {
        player = FindObjectOfType<Player>();
        itemDataManager = FindObjectOfType<ItemDataManager>();
        skillDataManager = FindObjectOfType<SkillDataManager>();
    }
}
