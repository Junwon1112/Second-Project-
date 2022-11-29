using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Object/ItemData_Weapon", order = 3)]
public class ItemData_Weapon : ItemData, IEquipable
{
    public float attackDamage;
    GameObject makedItem = null;
    public void Equip(Player player)
    {
        //장착하는 코드(장착하면 prefab을 플레이어의 장비가 장착되는 위치에 생성시키고, 플레이어 능력치에 장비 능력치만큼의 데이터를 추가 시킴)
        makedItem = Instantiate(itemPrefab, player.transform.position, player.transform.rotation);  //위치는 일단 플레이어 위치로 설정 => 나중에 손위치로 변경 필요
        player.AttackDamage += attackDamage;
    }

    public void UnEquip(Player player)
    {
        //장착하는 코드(장착하면 prefab을 플레이어의 장비가 장착되는 위치에 생성시키고, 플레이어 능력치에 장비 능력치만큼의 데이터를 추가 시킴)
        Destroy(makedItem);  //위치는 일단 플레이어 위치로 설정 => 나중에 손위치로 변경 필요
        player.AttackDamage -= attackDamage;
    }
}
