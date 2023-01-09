using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    ItemDataManager itemDataManager;
    //나중에 InventoryUI 추가해야됨

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


    private void Initialize()   //시작하면 오브젝트 가져오기
    {
        player = FindObjectOfType<Player>();
        itemDataManager = FindObjectOfType<ItemDataManager>();
    }
}
