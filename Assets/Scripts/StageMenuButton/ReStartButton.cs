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
        restartButton.onClick.AddListener(StartStage);
    }

    private void StartStage()
    {
        SceneManager.LoadScene(GameManager.Instance.CurrentScene.name);
    }
}
