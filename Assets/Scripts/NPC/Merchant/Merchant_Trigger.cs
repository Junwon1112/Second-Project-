using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant_Trigger : MonoBehaviour
{
    Merchant merchant;
    CanvasGroup g_ButtonCanvas;

    bool isPlayerInTrigger;

    public bool IsPlayerInTrigger
    {
        get { return isPlayerInTrigger; }
        set { isPlayerInTrigger = value; }
    }

    private void Awake()
    {
        merchant = FindObjectOfType<Merchant>();
        g_ButtonCanvas = FindObjectOfType<FindGButton>().transform.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        //transform.position = merchant.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SetCanvasOn();
            IsPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetCanvasOff();
            IsPlayerInTrigger = false;
        }
    }

    public void SetCanvasOn()
    {
        g_ButtonCanvas.alpha = 1;
        g_ButtonCanvas.blocksRaycasts = true;
        g_ButtonCanvas.interactable = true;
    }
    
    public void SetCanvasOff()
    {
        g_ButtonCanvas.interactable = false;
        g_ButtonCanvas.alpha = 0;
        g_ButtonCanvas.blocksRaycasts = false;
    }
}
