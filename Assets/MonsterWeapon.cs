using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ͱ� ���ݽ� ���� �������� ����ϴ� Ŭ����, ���� ����� �������� ������ �÷��̾�� ��Ī���� ���� Ŭ���� �̸�
/// </summary>
public class MonsterWeapon : MonoBehaviour, IBattle
{
    Monster_Basic monster;

    float attackDamage;
    float defence;
    public float AttackDamage { get; set; }
    public float Defence { get; set; }
    private void Awake()
    {
        monster = transform.parent.GetComponent<Monster_Basic>();
    }

    private void Start()
    {
        AttackDamage = monster.AttackDamage;
        Defence = monster.Defence;
    }


    public void Attack(IHealth target)
    {
        target.HP -= (AttackDamage - target.Defence);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���⿡�ִ� �ö��̴��� Ʈ����
        if (other.CompareTag("Player"))
        {
            Player player;
            player = other.GetComponent<Player>();


            Attack(player);
            player.SetHP();

        }
    }

}
