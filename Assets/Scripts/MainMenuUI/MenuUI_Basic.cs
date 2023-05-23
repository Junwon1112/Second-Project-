using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuUI_Basic : MonoBehaviour
{
    protected abstract CanvasGroup CanvasGroup { get; set; }
    protected abstract bool IsOpen { get; set; }
    protected abstract SideMenuUI[] SideMenuUIs { get; set; }



    public abstract void OnOffMainMenu();


    public abstract void OpenMainMenu();


    public abstract void CloseMainMenu();


    protected abstract bool IsChildMenuOpen();

}
