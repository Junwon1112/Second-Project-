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
    ScriptableObj_JobData jobData;

    /// <summary>
    /// �� ��ư�� ������ ���õǴ� ��Ÿ��
    /// </summary>
    [SerializeField]
    JobType jobType;

    Button selectButton;
    

    private void Awake()
    {
        selectButton = GetComponent<Button>();
    }

    private void Start()
    {
        selectButton.onClick.AddListener(SelectChracter);
    }

    private void SelectChracter()
    {
        jobData.jobType = jobType;
        SceneManager.LoadScene("Stage1");
    }
}
