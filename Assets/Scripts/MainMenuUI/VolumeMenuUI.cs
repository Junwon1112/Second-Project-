using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMenuUI : MonoBehaviour
{
    Slider BGMVolumeSlider;
    Slider EffectVolumeSlider;


    CanvasGroup canvasGroup;

    bool isVolumeChangeComplete;



    public bool IsVolumeChangeComplete { get; set; }

    private void Awake()
    {
        BGMVolumeSlider = transform.GetChild(1).GetComponentInChildren<Slider>();
        EffectVolumeSlider = transform.GetChild(2).GetComponentInChildren<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// VolumeMenu가 MainMenu Volume버튼에 의해 눌리면 실행 됨
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoVolumeChangeUpdate()
    {
        while (!IsVolumeChangeComplete)
        {
            BGMVolumeControl();
            EffectVolumeControl();
            yield return new WaitForSecondsRealtime(0.01f);
        }

    }

    private void BGMVolumeControl()
    {
        SoundPlayer.Instance.BGMVolumeChange(BGMVolumeSlider.value);
    }

    private void EffectVolumeControl()
    {
        SoundPlayer.Instance.EffectVolumeChange(EffectVolumeSlider.value);
    }

    public void OpenVolumeMenu()
    {
        Time.timeScale = 0;

        IsVolumeChangeComplete = false;
        StartCoroutine(CoVolumeChangeUpdate());     //IsVolumeChangeComplete가 반드시 false가 되고 난 후 실행되어야함

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        
    }

    public void CloseVolumeMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        IsVolumeChangeComplete = true;

        Time.timeScale = 1;
    }
}
