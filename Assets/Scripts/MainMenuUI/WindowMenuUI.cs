using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMenuUI : SideMenuUI
{
    bool isSideUIChangeComplete;

    protected override CanvasGroup SideCanvasGroup { get; set; }
    public override bool IsSideUIChangeComplete { get; set; }

    private void Awake()
    {
        SideCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        IsSideUIChangeComplete = true;
    }

    public override void SetWindow()
    {
        if (SideCanvasGroup.interactable == true)
        {
            SideCanvasGroup.alpha = 0;
            SideCanvasGroup.blocksRaycasts = false;
            SideCanvasGroup.interactable = false;

            IsSideUIChangeComplete = true;

            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;

            IsSideUIChangeComplete = false;

            SideCanvasGroup.alpha = 1;
            SideCanvasGroup.blocksRaycasts = true;
            SideCanvasGroup.interactable = true;
        }
    }

}