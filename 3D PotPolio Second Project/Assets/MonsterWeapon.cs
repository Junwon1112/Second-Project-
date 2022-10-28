using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour, IBattle
{
    Monster monster;

    float attackDamage;
    float defence;
    public float AttackDamage { get; set; }
    public float Defence { get; set; }
    private void Awake()
    {
        monster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Monster>();
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
        // 무기에있는 컬라이더의 트리거
        if (other.CompareTag("Player"))
        {
            Player player;
            player = other.GetComponent<Player>();


            Attack(player);
            player.SetHP();

        }
    }

}
