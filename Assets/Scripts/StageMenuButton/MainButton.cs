using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 메인메뉴 씬으로 이동시키는 클래스
/// </summary>
public class MainButton : MonoBehaviour
{
    Button mainButton;

    EventSystem eventSystem;
    LocationReset locationReset;

    private void Awake()
    {
        mainButton = GetComponent<Button>();
        eventSystem = FindObjectOfType<MainEventSystem>().transform.GetComponent<EventSystem>();
        locationReset = FindObjectOfType<LocationReset>();
    }

    private void Start()
    {
        mainButton.onClick.AddListener(BackToMainStage);
    }

    private void BackToMainStage()
    {
        SceneManager.LoadScene("Main");
        TotalGameManager.Instance.ResetDontDestroy();
        Time.timeScale = 1.0f;
    }

    
}
