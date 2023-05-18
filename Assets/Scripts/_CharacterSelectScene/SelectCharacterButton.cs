using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterButton : MonoBehaviour
{
    /// <summary>
    /// 이 버튼을 누를시 선택되는 잡타입
    /// </summary>
    [SerializeField]
    JobType jobType;

    Button selectCharacterButton;
    Animator anim;
    SelectButton select_Confirm;
    Light selectlightFront_SwordMan;
    Light selectlightFront_Witch;
    


    private void Awake()
    {
        selectCharacterButton = GetComponent<Button>();
        select_Confirm = FindObjectOfType<SelectButton>();
        selectlightFront_SwordMan = GameObject.Find($"StageSwordMan").transform.GetChild(4).GetChild(0).GetComponent<Light>();
        selectlightFront_Witch = GameObject.Find($"StageWitch").transform.GetChild(4).GetChild(0).GetComponent<Light>();
    }

    private void Start()
    {
        anim = GameObject.Find($"CharacterSelect_{jobType.ToString()}").transform.GetComponent<Animator>();
        selectCharacterButton.onClick.AddListener(SelectChracter);
        
    }

    private void SelectChracter()
    {
        anim.SetTrigger("isClick");
        select_Confirm.jobData.jobType = jobType;
        select_Confirm.SetButton();
        if(jobType == JobType.SwordMan)
        {
            selectlightFront_SwordMan.intensity = 10.0f;
            selectlightFront_Witch.intensity = 0.0f;
        }
        else if(jobType == JobType.Witch)
        {
            selectlightFront_SwordMan.intensity = 0.0f;
            selectlightFront_Witch.intensity = 10.0f;
        }

    }
}
