using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMenuUI : MonoBehaviour
{
    Slider volumeSlider;

    CanvasGroup canvasGroup;

    bool isVolumeChangeComplete = true;

    public bool IsVolumeChangeComplete { get; set; }

    private void Awake()
    {
        volumeSlider = GetComponentInChildren<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// VolumeMenu가 MainMenu Volume버튼에 의해 눌리면 실행 됨
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoVolumeChangeUpdate()
    {
        while(IsVolumeChangeComplete)
        {
            yield return new WaitForFixedUpdate();
            VolumeChange();
        }

    }

    private void VolumeChange()
    {
        SoundPlayer.Instance.VolumeChange(volumeSlider.value);
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
