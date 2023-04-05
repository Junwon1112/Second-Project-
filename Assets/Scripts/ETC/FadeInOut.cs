using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut Instance { get; set; }

    Image fadeInOut;

    /// <summary>
    /// �ڽ� UI���� canvasgroup�� ��Ʈ��, FadeOut�� �� �ڽ� UI�� ����
    /// </summary>
    CanvasGroup childrenCanvasControl;

    float fadeTime = 2.0f;
    float targetFadeInAlpha = 0.0f;
    float targetFadeOutAlpha = 1.0f;

    public float FadeTime
    {
        get { return fadeTime; }
    }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
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



    private void Initialize()
    {
        fadeInOut = GetComponent<Image>();
        childrenCanvasControl = GetComponentInChildren<CanvasGroup>();
    }

    /// <summary>
    /// ���� ���۽� GameManager���� ����
    /// </summary>
    public void FadeIn()
    {
        fadeInOut.CrossFadeAlpha(targetFadeInAlpha, fadeTime, true) ;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void Fadeout()
    {
        fadeInOut.CrossFadeAlpha(targetFadeOutAlpha, FadeTime, true);
        StartCoroutine(CoControlChildrenUI());
    }

    IEnumerator CoControlChildrenUI()
    {
        yield return new WaitForSecondsRealtime(FadeTime);
        childrenCanvasControl.alpha = targetFadeOutAlpha;
        childrenCanvasControl.blocksRaycasts = true;
        childrenCanvasControl.interactable = true;
    }
}
