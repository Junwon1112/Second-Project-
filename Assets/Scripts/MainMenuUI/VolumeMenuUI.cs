using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMenuUI : SideMenuUI
{
    Slider bgmVolumeSlider;
    Slider effectVolumeSlider;

    protected override CanvasGroup SideCanvasGroup { get; set; }

    bool isSideUIChangeComplete;

    public override bool IsSideUIChangeComplete { get; set; }

    private void Awake()
    {
        bgmVolumeSlider = transform.GetChild(1).GetComponentInChildren<Slider>();
        effectVolumeSlider = transform.GetChild(2).GetComponentInChildren<Slider>();
        SideCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        bgmVolumeSlider.value = SoundPlayer.Instance.BGMCurrentVolume;
        effectVolumeSlider.value = SoundPlayer.Instance.EffectCurrentVolume;
        IsSideUIChangeComplete = true;
    }

    /// <summary>
    /// VolumeMenu가 MainMenu Volume버튼에 의해 눌리면 실행 됨
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoVolumeChangeUpdate()
    {
        while (!IsSideUIChangeComplete)
        {
            BGMVolumeControl();
            EffectVolumeControl();
            yield return new WaitForSecondsRealtime(0.01f);
        }

    }

    private void BGMVolumeControl()
    {
        SoundPlayer.Instance.BGMVolumeChange(bgmVolumeSlider.value);
    }

    private void EffectVolumeControl()
    {
        SoundPlayer.Instance.EffectVolumeChange(effectVolumeSlider.value);
    }

    public override void SetWindow()
    {
        if (SideCanvasGroup.interactable == true)
        {
            SideCanvasGroup.alpha = 0;
            SideCanvasGroup.blocksRaycasts = false;
            SideCanvasGroup.interactable = false;

            IsSideUIChangeComplete = true;

            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;

            IsSideUIChangeComplete = false;
            StartCoroutine(CoVolumeChangeUpdate());     //IsVolumeChangeComplete가 반드시 false가 되고 난 후 실행되어야함

            SideCanvasGroup.alpha = 1;
            SideCanvasGroup.blocksRaycasts = true;
            SideCanvasGroup.interactable = true;
        }
    }

}
