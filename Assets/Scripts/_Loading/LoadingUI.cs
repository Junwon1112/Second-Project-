using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LoadingUI : MonoBehaviour
{
    PlayerInput input;

    TextMeshProUGUI loadingText;

    const int MAXNUM = 10000;
    string loadingText_Basic = "Loading";

    Slider loadingBar;

    AsyncOperation async;

    float loadingProgress = 0.0f;

    bool loadingCompleted = false;

    /// <summary>
    /// PrintLoading �ڷ�ƾ�� �ܺο��� ���߰� �ϱ� ���� ���� ����
    /// </summary>
    IEnumerator PrintLoadingText;

    private void Awake()
    {
        loadingText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        loadingBar = GetComponentInChildren<Slider>();
        
    }


    private void Start()
    {
        input = TotalGameManager.Instance.Input;
        input.LoadingUI.Enable();
        input.LoadingUI.GoToNextScene.performed += OnGoToNextStage;

        PrintLoadingText = PrintLoading();
        StartCoroutine(PrintLoadingText);

        StartCoroutine(LoadScene());
    }

    private void OnDisable()
    {
        input.LoadingUI.GoToNextScene.performed -= OnGoToNextStage;
        input.LoadingUI.Disable();
    }


    /// <summary>
    /// Ŭ���ϸ� ���� ���������� �Ѿ���� �Լ�
    /// </summary>
    /// <param name="obj"></param>
    private void OnGoToNextStage(InputAction.CallbackContext obj)
    {
        if (loadingCompleted)
        {
            async.allowSceneActivation = true;
        }
    }

    /// <summary>
    /// �ε� �ؽ�Ʈ�� �ٲٴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator PrintLoading()
    {
        for(int i = 0; i < MAXNUM; i++)
        {
            loadingText.text = loadingText_Basic;
            loadingText_Basic += ".0";
            i %= 4;

            if(i == 0)
            {
                loadingText_Basic = "Loading";
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(TotalGameManager.Instance.CurrentScene.buildIndex + 1);
        async.allowSceneActivation = false;

        float tempNum;

        while(loadingProgress != 1.0)
        {
            tempNum = loadingProgress;
            loadingProgress = Mathf.Lerp(tempNum, async.progress + 0.1f, 0.1f);

            if(loadingProgress > 0.9f)
            {
                loadingProgress = 1.0f;
            }
            loadingBar.value = loadingProgress;

            yield return null;
        }

        loadingCompleted = true;
        StopCoroutine(PrintLoadingText);

        loadingText.text = "Please Click The Button";

    }
}
