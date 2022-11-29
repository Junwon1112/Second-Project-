using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IBattle
{
    Player player;

    float attackDamage;
    float defence;
    public float AttackDamage { get; set; }
    public float Defence { get; set; }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        AttackDamage = player.AttackDamage;
        Defence = player.Defence;
    }


    public void Attack(IHealth target)
    {
        target.HP -= (AttackDamage - target.Defence);
    }

    private void OnTriggerEnter(Collider other) //ontriggerenter�� �����ϸ� ���� �ȵȴ�.
    {
        //�÷��̾� Į���ִ� �ö��̴��� Ʈ����
        if (other.CompareTag("Monster"))
        {
            Monster monster;
            monster = other.GetComponent<Monster>();


            Attack(monster);
            monster.SetHP();

        }
    }

}
