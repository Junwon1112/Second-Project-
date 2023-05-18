using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �̱��� ������ �̿��� �÷��̾�, ������, ��ų ���� �����͸� ������ �� �ִ� GameManager Ŭ����
/// </summary>
public class GameManager : MonoBehaviour
{
    Player player;
    ItemDataManager itemDataManager;

    Scene currentScene;

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
    public static GameManager Instance;

    /// <summary>
    /// Instance�� ���� ������Ƽ
    /// </summary>
    public GameManager Inst
    {
        get
        {
            return Instance;
        }
    }

    public Scene CurrentScene
    {
        get
        {
            return currentScene;
        }
        private set
        {
            currentScene = value;
        }
    }

    GameObject[] testObjs;

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
        player = GameObject.Find($"Player_{jobData.jobType.ToString()}").GetComponent<Player>();
        itemDataManager = FindObjectOfType<ItemDataManager>();
        CurrentScene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        FadeInOut.Instance.FadeIn();
        
    }

    private void OnLevelWasLoaded(int level)
    {
        CurrentScene = SceneManager.GetActiveScene();
    }

    public void ResetDontDestroy()
    {
        for (int i = 0; i < DontDestroyOnLoad_Manager.Instance.objs_DontDestroy.Count; i++)
        {
            Destroy(DontDestroyOnLoad_Manager.Instance.objs_DontDestroy[i]);
        }

        Destroy(UI_Player_MoveOnOff.instance.gameObject);
        Destroy(Skill_Implement.Instance.gameObject);
        Destroy(SkillDataManager.Instance.gameObject);
        Destroy(SoundPlayer.Instance.gameObject);
        Destroy(ParticlePlayer.Instance.gameObject);
        Destroy(DMGTextPlayer.Instance.gameObject);
        Destroy(CursorManager.Instance.gameObject);
        Destroy(MainCamera_PlayerPos.instance.gameObject);
        Destroy(MiniMapCamera.instance.gameObject);
        foreach(GameObject DontDestroyObj in DontDestroyOnLoad_Manager.Instance.objs_DontDestroy)
        {
            Destroy(DontDestroyObj);
        }

        Destroy(GameManager.Instance.MainPlayer.gameObject);
        Destroy(GameManager.Instance.gameObject);

    }
}
