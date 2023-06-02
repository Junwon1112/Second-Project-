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
        SceneManager.LoadScene(TotalGameManager.Instance.CurrentScene.name);
        //if (TotalGameManager.Instance.CurrentScene.name != ("Stage1"))
        //{
        //    SceneManager.LoadScene(TotalGameManager.Instance.CurrentScene.name);
        //}
        //else
        //{
        //    TotalGameManager.Instance.ResetDontDestroy();
            
        //}
        Time.timeScale = 1.0f;
    }
}
