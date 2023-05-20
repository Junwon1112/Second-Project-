using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public static StoreUI instance;
    private Merchant merchant;

    [SerializeField]
    GameObject storeSlotUIObjects;

    private RectTransform storeSlotUIs_Rect;

    private void Awake()
    {
        Initialize();
        merchant = FindObjectOfType<Merchant>();
        storeSlotUIs_Rect = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
    }

    private void Start()
    {
        SetStoreSlotUIs();
    }

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void SetStoreSlotUIs()
    {
        SetStoreSlotUIsHeight();

        for(int i = 0; i < merchant.sellingItems.Length; i++)
        {
            GameObject slotUIObj = Instantiate(storeSlotUIObjects, storeSlotUIs_Rect);
            StoreSlotUI storeSlotUI = slotUIObj.GetComponentInChildren<StoreSlotUI>();
            storeSlotUI.sellingItemData = merchant.sellingItems[i];
            storeSlotUI.SetItem(storeSlotUI.sellingItemData.itemIcon, storeSlotUI.sellingItemData.itemName, storeSlotUI.sellingItemData.itemValue);
        }
    }

    private void SetStoreSlotUIsHeight()
    {
        float height = merchant.sellingItems.Length * 200.0f + 150;
        storeSlotUIs_Rect.sizeDelta = new Vector2(storeSlotUIs_Rect.rect.width, height); 
    }

    private void OnLevelWasLoaded(int level)
    {
        merchant = FindObjectOfType<Merchant>();
        SetStoreSlotUIs();
    }
}
