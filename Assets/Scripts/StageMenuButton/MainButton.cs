using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ���θ޴� ������ �̵���Ű�� Ŭ����
/// </summary>
public class MainButton : MonoBehaviour
{
    Button mainButton;

    EventSystem eventSystem;

    private void Awake()
    {
        mainButton = GetComponent<Button>();
        eventSystem = FindObjectOfType<MainEventSystem>().transform.GetComponent<EventSystem>();
    }

    private void Start()
    {
        mainButton.onClick.AddListener(BackToMainStage);
    }

    private void BackToMainStage()
    {
        SceneManager.LoadScene("Main");
        ResetDontDestroy();
    }

    private void ResetDontDestroy()
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
        Destroy(eventSystem.gameObject);

        Destroy(GameManager.Instance.MainPlayer.gameObject);
        Destroy(GameManager.Instance.gameObject);
        
    }
}
