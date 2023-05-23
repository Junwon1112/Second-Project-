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
    /// ��ư ������ �ϴ� �÷��̾� ��ų����Ʈ ��ȯ�ϰ� skillslotui�� ��ų���� ����
    /// ��ũ���ͺ� ������Ʈ�� �����ʹ� ��Ÿ���߿� ���� �ٲ�� ������ �����ص� ��� ����Ǿ��ִ� ���°� ��ӵǰ�, ���� ������ ���� ����ȭ �� �ʿ䰡 ����
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
    /// ��ư ������ �ϴ� �÷��̾� ��ų����Ʈ ��ȯ�ϰ� skillslotui�� ��ų���� ����
    /// ��ũ���ͺ� ������Ʈ�� �����ʹ� ��Ÿ���߿� ���� �ٲ�� ������ �����ص� ��� ����Ǿ��ִ� ���°� ��ӵǰ�, ���� ������ ���� ����ȭ �� �ʿ䰡 ����
    /// </summary>
    private void CurrentSkillPointDown()
    {
        if (skillSlotUI.SkillData.SkillLevel > 0)
        {
            InGameManager.Instance.MainPlayer.SetSkillPointUp();
            skillSlotUI.SkillData.SkillLevel--;
            SkillLevelToText();

            //��ų������ 0�� �Ǹ� ���������� �ڵ����� ������ ��
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
