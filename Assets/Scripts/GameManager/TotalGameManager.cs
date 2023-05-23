using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalGameManager : MonoBehaviour
{
    public static TotalGameManager Instance;

    Scene currentScene;

    PlayerInput input;

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

    public PlayerInput Input { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        Input = new PlayerInput();
        CurrentScene = SceneManager.GetActiveScene();
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
        Destroy(ParticlePlayer.Instance.gameObject);
        Destroy(DMGTextPlayer.Instance.gameObject);
        Destroy(MainCamera_PlayerPos.instance.gameObject);
        Destroy(MiniMapCamera.instance.gameObject);
        foreach (GameObject DontDestroyObj in DontDestroyOnLoad_Manager.Instance.objs_DontDestroy)
        {
            Destroy(DontDestroyObj);
        }

        Destroy(InGameManager.Instance.MainPlayer.gameObject);
        Destroy(InGameManager.Instance.gameObject);

        //얘네는 첫번째 씬에서 실행되므로 따로 건들일 필요가 없다.
        //Destroy(SoundPlayer.Instance.gameObject);
        //Destroy(CursorManager.Instance.gameObject);
    }
}
