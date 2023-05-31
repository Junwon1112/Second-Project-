using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    /// <summary>
    /// �÷��̾�� �Ŵ����� ��ũ���ͺ������Ʈ�� ����
    /// </summary>
    [SerializeField]
    public ScriptableObj_JobData jobData;
    

    LightSetting lightSetting;

    Button selectButton;

    MakePlayer makePlayer;

    private void Awake()
    {
        selectButton = GetComponent<Button>();
        lightSetting = FindObjectOfType<LightSetting>();
        makePlayer = FindObjectOfType<MakePlayer>();
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
        makePlayer.PlayerMaking();
        StartCoroutine(CoLoadNextScene());
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

    IEnumerator CoLoadNextScene()
    {
        Player player = FindObjectOfType<Player>();

        while(player == null)
        {
            FindObjectOfType<Player>();
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(TotalGameManager.Instance.CurrentScene.buildIndex + 1);

    }
}
