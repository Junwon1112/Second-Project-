using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    ItemDataManager itemDataManager;
    //���߿� InventoryUI �߰��ؾߵ�

    public Player MainPlyer
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
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()   //�����ϸ� ������Ʈ ��������
    {
        player = FindObjectOfType<Player>();
        itemDataManager = FindObjectOfType<ItemDataManager>();
    }
}
