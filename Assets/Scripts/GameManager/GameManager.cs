using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 싱글톤 패턴을 이용해 플레이어, 아이템, 스킬 관련 데이터를 가져올 수 있는 GameManager 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    Player player;
    ItemDataManager itemDataManager;

    Scene currentScene;

    [SerializeField]
    ScriptableObj_JobData jobData;

    /// <summary>
    /// player에 대한 프로퍼티
    /// </summary>
    public Player MainPlayer
    {
        get
        {
            return player;
        }
    }

    /// <summary>
    /// ItemManager에 대한 프로퍼티
    /// </summary>
    public ItemDataManager ItemManager
    {
        get
        {
            return itemDataManager;
        }
    }

    /// <summary>
    /// static을 사용해 외부에서도 새로운 클래스를 만들지 않고 바로 가져올 수 있음, 하나의 인스턴스만 존재
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// Instance에 대한 프로퍼티
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
    /// 싱글톤패턴에서 단 1개의 GameManager만 존재하게 하기위해 씬이 바뀔 때마다 1개의 인스턴스만 존재하는지 확인하는 패턴
    /// 1개 이상 존재 시 자기자신을 파괴
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


    private void Initialize()   //시작하면 오브젝트 가져오기
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
