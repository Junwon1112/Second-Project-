using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Scriptable Object_Item Data/ItemData_SpecialPotion_Scale", order = 10)]
public class ItemData_SpecialPotion_Scale : ItemData, IConsumable
{
    public float scaleRate;
    public void Use(Player player)
    {
        player.transform.localScale = new Vector3(scaleRate,scaleRate,scaleRate);
    }
}
