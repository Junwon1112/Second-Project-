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
        if(GameManager.Instance.MainPlayer.SkillPoint > 0)
        {
            GameManager.Instance.MainPlayer.SetSkillPointDown();
            skillSlotUI.skillData.SkillLevel++;
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
        if (skillSlotUI.skillData.SkillLevel > 0)
        {
            GameManager.Instance.MainPlayer.SetSkillPointUp();
            skillSlotUI.skillData.SkillLevel--;
            SkillLevelToText();

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
        currentSkillLevel_Text.text = skillSlotUI.skillData.SkillLevel.ToString();
    }
}
