using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    
    public string[] basicScript;
    DialogUI dialogUI;


    private void Awake()
    {
        dialogUI = FindObjectOfType<DialogUI>();
    }


}
