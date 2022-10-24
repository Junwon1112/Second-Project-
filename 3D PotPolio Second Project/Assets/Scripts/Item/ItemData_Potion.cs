using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Scriptable/ItemData", order = 2)]

public class ItemData_Potion : ItemData, IConsumable
{
    public void Use(Player player)
    {
        // 플레이어 체력을 먼저 구현 후에 처리해야됨
    }
}
