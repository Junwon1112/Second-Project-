using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    /// <summary>
    /// 플레이어와 매니저의 스크립터블오브젝트에 적용
    /// </summary>
    [SerializeField]
    public ScriptableObj_JobData jobData;
    LightSetting lightSetting;

    Button selectButton;
    

    private void Awake()
    {
        selectButton = GetComponent<Button>();
        lightSetting = FindObjectOfType<LightSetting>();
    }

    private void Start()
    {
        selectButton.onClick.AddListener(SelectChracter);
        jobData.jobType = JobType.Everyone;
        SetButton();
    }

    private void SelectChracter()
    {
        lightSetting.SetLight();
        SceneManager.LoadScene("Stage1");
    }

    public void SetButton()
    {
        if (jobData.jobType == JobType.Everyone)
        {
            selectButton.interactable = false;
        }
        else
        {
            selectButton.interactable = true;
        }
    }
}
