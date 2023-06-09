using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Trigger : MonoBehaviour
{
    NPC npc;
    CanvasGroup g_ButtonCanvas;
    DialogUI dialogUI;

    bool isPlayerInTrigger;

    public bool IsPlayerInTrigger
    {
        get { return isPlayerInTrigger; }
        set { isPlayerInTrigger = value; }
    }

    private void Awake()
    {
        npc = transform.parent.GetComponentInChildren<NPC>();
        g_ButtonCanvas = FindObjectOfType<FindGButton>().transform.GetComponent<CanvasGroup>();
        dialogUI = FindObjectOfType<DialogUI>();
    }

    private void Start()
    {
        //transform.position = npc.transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetCanvasOn();
            IsPlayerInTrigger = true;
            dialogUI.npc_Trigger = this;
            dialogUI.npc = this.npc;
            dialogUI.questButtons.npc = this.npc;
            dialogUI.SetText(npc.data.npcName, npc.data.basicScript);
            
            for(int i =0; i < npc.data.quest.Length; i++)
            {
                if (npc.data.quest[i].requireLevel <= InGameManager.Instance.MainPlayer.level)
                {
                    dialogUI.isQuestExist = true;
                    //dialogUI.questButtons.SetText(npc.data.npcName, npc.data.quest[i].dialog);
                    break;
                }
            }
            
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
