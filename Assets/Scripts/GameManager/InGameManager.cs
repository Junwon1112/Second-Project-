using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �̱��� ������ �̿��� �÷��̾�, ������, ��ų ���� �����͸� ������ �� �ִ� GameManager Ŭ����
/// </summary>
public class InGameManager : MonoBehaviour
{
    Player player;
    ItemDataManager itemDataManager;

    [SerializeField]
    ScriptableObj_JobData jobData;

    /// <summary>
    /// player�� ���� ������Ƽ
    /// </summary>
    public Player MainPlayer
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }

    /// <summary>
    /// ItemManager�� ���� ������Ƽ
    /// </summary>
    public ItemDataManager ItemManager
    {
        get
        {
            return itemDataManager;
        }
    }

    /// <summary>
    /// static�� ����� �ܺο����� ���ο� Ŭ������ ������ �ʰ� �ٷ� ������ �� ����, �ϳ��� �ν��Ͻ��� ����
    /// </summary>
    public static InGameManager Instance;

    /// <summary>
    /// Instance�� ���� ������Ƽ
    /// </summary>
    public InGameManager Inst
    {
        get
        {
            return Instance;
        }
    }


    /// <summary>
    /// �̱������Ͽ��� �� 1���� GameManager�� �����ϰ� �ϱ����� ���� �ٲ� ������ 1���� �ν��Ͻ��� �����ϴ��� Ȯ���ϴ� ����
    /// 1�� �̻� ���� �� �ڱ��ڽ��� �ı�
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        //player = GameObject.Find($"Player_{jobData.jobType.ToString()}").GetComponent<Player>();
        //player = FindObjectOfType<Player>();
        //player�� PlayerŬ�������� ���� ������
        itemDataManager = FindObjectOfType<ItemDataManager>();
        player = FindObjectOfType<Player>();  
    }

    private void Start()
    {
        FadeInOut.Instance.FadeIn();
    }


    
}
