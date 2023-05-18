using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���Ӿ����� �̵���Ű�� Ŭ����
/// </summary>
public class ReStartButton : MonoBehaviour
{
    SceneManager sceneManager;
    Button restartButton;

    private void Awake()
    {
        restartButton = GetComponent<Button>();
    }

    private void Start()
    {
        restartButton.onClick.AddListener(ReStartStage);
    }

    private void ReStartStage()
    {
        if(GameManager.Instance.CurrentScene.name == ("StartVillage"))
        {
            SceneManager.LoadScene(GameManager.Instance.CurrentScene.name);
        }
        else
        {
            GameManager.Instance.ResetDontDestroy();
            SceneManager.LoadScene(GameManager.Instance.CurrentScene.name);
        }
        Time.timeScale = 1.0f;
    }
}
