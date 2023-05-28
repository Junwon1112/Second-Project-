using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WarningUI : MonoBehaviour
{
    Button Okbutton;
    CanvasGroup canvasGroupOnOff;
    TextMeshProUGUI textWarningInfo;

    [SerializeField]
    WarningTextData[] warnings;

    WarningTextName warningTextName;

    Dictionary<WarningTextName, WarningTextData> warningsDict = new Dictionary<WarningTextName, WarningTextData>();

    public bool IsUIOnOff { get; set; }
    public CanvasGroup CanvasGroupOnOff { get; set; }
    public RectTransform Rect_WarningUI { get; set; }

    private void Awake()
    {
        Okbutton = GetComponentInChildren<Button>();

        warningsDict.Clear();
        for (int i = 0; i < warnings.Length; i++)
        {
            warningsDict.Add((WarningTextName)System.Enum.Parse(typeof(WarningTextName), warnings[i].name), warnings[i]);
        }

        textWarningInfo = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        CanvasGroupOnOff = GetComponent<CanvasGroup>();
        Rect_WarningUI = GetComponent<RectTransform>();
        IsUIOnOff = true;
    }

    private void Start()
    {
        Okbutton.onClick.AddListener(UIOnOffSetting);
    }

    public void UIOnOffSetting()
    {
        if (IsUIOnOff)
        {
            IsUIOnOff = false;

            CanvasGroupOnOff.alpha = 1;
            CanvasGroupOnOff.interactable = true;
            CanvasGroupOnOff.blocksRaycasts = true;

            Rect_WarningUI.SetAsLastSibling();
        }
        else
        {
            IsUIOnOff = true;

            CanvasGroupOnOff.alpha = 0;
            CanvasGroupOnOff.interactable = false;
            CanvasGroupOnOff.blocksRaycasts = false;

        }

    }

    public void SetTextWarningInfo(WarningTextName warningTextEnum)
    {
        textWarningInfo.text = warningsDict[warningTextEnum].warningText;
    }

}   
