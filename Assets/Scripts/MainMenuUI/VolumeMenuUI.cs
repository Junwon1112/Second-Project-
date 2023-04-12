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
    /// VolumeMenu�� MainMenu Volume��ư�� ���� ������ ���� ��
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
        StartCoroutine(CoVolumeChangeUpdate());     //IsVolumeChangeComplete�� �ݵ�� false�� �ǰ� �� �� ����Ǿ����

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
