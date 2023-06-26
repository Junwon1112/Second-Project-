using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompensateImages_Create : MonoBehaviour
{
    [SerializeField]
    GameObject compensation_Item;
    public Quest quest; 
    CompensateUI compensateUI;


    private void Awake()
    {
        compensateUI = FindObjectOfType<CompensateUI>();
    }

    public void CreateCompensationIcon()
    {
        if(quest.compensation_Item != null)
        {
            GameObject compensationObj = Instantiate(compensation_Item, transform);
            Image compensation_ItemImage = compensationObj.transform.GetComponent<Image>();
            TextMeshProUGUI compensation_Item_NumText = compensationObj.transform.GetComponentInChildren<TextMeshProUGUI>();

            compensation_ItemImage.sprite = quest.compensation_Item.itemIcon;
            compensation_Item_NumText.text = quest.compensation_Num.ToString();
        }
    }
}
