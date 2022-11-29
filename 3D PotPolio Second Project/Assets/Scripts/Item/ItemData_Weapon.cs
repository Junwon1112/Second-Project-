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
        //�����ϴ� �ڵ�(�����ϸ� prefab�� �÷��̾��� ��� �����Ǵ� ��ġ�� ������Ű��, �÷��̾� �ɷ�ġ�� ��� �ɷ�ġ��ŭ�� �����͸� �߰� ��Ŵ)
        makedItem = Instantiate(itemPrefab, player.transform.position, player.transform.rotation);  //��ġ�� �ϴ� �÷��̾� ��ġ�� ���� => ���߿� ����ġ�� ���� �ʿ�
        player.AttackDamage += attackDamage;
    }

    public void UnEquip(Player player)
    {
        //�����ϴ� �ڵ�(�����ϸ� prefab�� �÷��̾��� ��� �����Ǵ� ��ġ�� ������Ű��, �÷��̾� �ɷ�ġ�� ��� �ɷ�ġ��ŭ�� �����͸� �߰� ��Ŵ)
        Destroy(makedItem);  //��ġ�� �ϴ� �÷��̾� ��ġ�� ���� => ���߿� ����ġ�� ���� �ʿ�
        player.AttackDamage -= attackDamage;
    }
}
