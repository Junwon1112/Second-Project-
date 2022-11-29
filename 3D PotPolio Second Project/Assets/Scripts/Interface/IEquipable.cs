using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    public void Equip(Player player);
    public void UnEquip(Player player);
}
