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
    ScriptableObj_JobData jobData;

    /// <summary>
    /// 이 버튼을 누를시 선택되는 잡타입
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
