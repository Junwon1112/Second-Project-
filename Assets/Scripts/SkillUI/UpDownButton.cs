using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpDownButton : MonoBehaviour
{
    Button upButton;
    Button downButton;
    Image upClickImage;
    Image downClickImage;
    TextMeshProUGUI currentSkillLevel_Text;

    public delegate void PointUpDel();

    SkillSlotUI skillSlotUI;

    AllQuickSlotUI allQuickSlotUI;

    private void Awake()
    {
        upButton = transform.Find("UpButton").GetComponent<Button>();
        downButton = transform.Find("DownButton").GetComponent<Button>();
        upClickImage = upButton.transform.GetComponent<Image>();
        downClickImage = downButton.transform.GetComponent<Image>();
        skillSlotUI = transform.parent.GetChild(0).GetComponent<SkillSlotUI>();
        currentSkillLevel_Text = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        allQuickSlotUI = FindObjectOfType<AllQuickSlotUI>();
    }
    void Start()
    {
        upButton.onClick.AddListener(CurrentSkillPointUp);
        downButton.onClick.AddListener(CurrentSkillPointDown);
    }

    /// <summary>
    /// 버튼 누르면 일단 플레이어 스킬포인트 변환하고 skillslotui의 스킬레벨 적용
    /// 스크립터블 오브젝트의 데이터는 런타임중에 값이 바뀌면 게임을 중지해도 계속 변경되어있는 상태가 계속되고, 따로 퀵슬롯 값을 동기화 할 필요가 없다
    /// </summary>
    private void CurrentSkillPointUp()
    {
        if(InGameManager.Instance.MainPlayer.SkillPoint > 0)
        {
            InGameManager.Instance.MainPlayer.SetSkillPointDown();
            skillSlotUI.SkillData.SkillLevel++;
            SkillLevelToText();

            StartCoroutine(ClickImage(upClickImage));
        }
    }

    /// <summary>
    /// 버튼 누르면 일단 플레이어 스킬포인트 변환하고 skillslotui의 스킬레벨 적용
    /// 스크립터블 오브젝트의 데이터는 런타임중에 값이 바뀌면 게임을 중지해도 계속 변경되어있는 상태가 계속되고, 따로 퀵슬롯 값을 동기화 할 필요가 없다
    /// </summary>
    private void CurrentSkillPointDown()
    {
        if (skillSlotUI.SkillData.SkillLevel > 0)
        {
            InGameManager.Instance.MainPlayer.SetSkillPointUp();
            skillSlotUI.SkillData.SkillLevel--;
            SkillLevelToText();

            //스킬레벨이 0이 되면 퀵슬롯으로 자동으로 빠지게 함
            if (skillSlotUI.SkillData.SkillLevel == 0)
            {
                for (int i = 0; i < allQuickSlotUI.quickSlotUIs.Length; i++)
                {
                    if(allQuickSlotUI.quickSlotUIs[i].quickSlotSkillData == skillSlotUI.SkillData)
                    {
                        allQuickSlotUI.quickSlotUIs[i].QuickSlotSetData();

                    }

                }
            }

            

            StartCoroutine(ClickImage(downClickImage));
        }
    }

    IEnumerator ClickImage(Image _buttonImage)
    {
        _buttonImage.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _buttonImage.color = Color.black;
    }

    public void SkillLevelToText()
    {
        currentSkillLevel_Text.text = skillSlotUI.SkillData.SkillLevel.ToString();
    }
}
