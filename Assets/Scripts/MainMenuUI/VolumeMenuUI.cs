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
    /// VolumeMenu�� MainMenu Volume��ư�� ���� ������ ���� ��
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

            SoundPlayer.Instance.PlaySound(SoundType_Effect.Sound_UI_Close);

            IsSideUIChangeComplete = true;

            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;

            IsSideUIChangeComplete = false;
            StartCoroutine(CoVolumeChangeUpdate());     //IsVolumeChangeComplete�� �ݵ�� false�� �ǰ� �� �� ����Ǿ����

            SoundPlayer.Instance.PlaySound(SoundType_Effect.Sound_UI_Open);

            SideCanvasGroup.alpha = 1;
            SideCanvasGroup.blocksRaycasts = true;
            SideCanvasGroup.interactable = true;
        }
    }

}
