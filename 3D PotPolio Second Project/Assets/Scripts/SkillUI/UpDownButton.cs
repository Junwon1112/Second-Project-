using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDownButton : MonoBehaviour
{
    Button upButton;
    Button downButton;
    Image upClickImage;
    Image downClickImage;

    public delegate void PointUpDel();

    private void Awake()
    {
        upButton = transform.Find("UpButton").GetComponent<Button>();
        downButton = transform.Find("DownButton").GetComponent<Button>();
        upClickImage = upButton.transform.GetComponent<Image>();
        downClickImage = downButton.transform.GetComponent<Image>();
    }
    void Start()
    {
        upButton.onClick.AddListener(CurrentSkillPointUp);
        downButton.onClick.AddListener(CurrentSkillPointDown);
    }

    private void CurrentSkillPointUp()
    {
        GameManager.Instance.MainPlayer.SetSkillPointDown();
        StartCoroutine(ClickImage(upClickImage));
    }

    private void CurrentSkillPointDown()
    {
        GameManager.Instance.MainPlayer.SetSkillPointUp();
        StartCoroutine(ClickImage(downClickImage));
    }

    IEnumerator ClickImage(Image _buttonImage)
    {
        _buttonImage.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _buttonImage.color = Color.black;
    }
}
