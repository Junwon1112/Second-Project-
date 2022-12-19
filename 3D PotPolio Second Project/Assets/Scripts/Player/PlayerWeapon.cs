using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IBattle
{
    Player player;

    float attackDamage;
    float defence;
    bool isCheckExp = false;     //몬스터가 죽었을 때 시체때리면 경험치 계속올라서 처음 죽었을 때만 오르도록 Attack함수에서 체력이 0보다 큰상태에서 0보다 작아지면 bool타입 발동
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
        if(target.HP >= 0)
        {
            target.HP -= (AttackDamage - target.Defence);
            if (target.HP <= 0)
            {
                isCheckExp = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) //ontriggerenter는 복붙하면 실행 안된다.
    {
        //플레이어 칼에있는 컬라이더의 트리거
        if (other.CompareTag("Monster"))
        {
            Monster monster;
            monster = other.GetComponent<Monster>();


            Attack(monster);
            monster.SetHP();
            if(monster.HP <= 0 && isCheckExp)
            {
                isCheckExp = false;
                player.Exp += monster.giveExp;
                player.SetExp();
                if(player.Exp >= player.MaxExp)
                {
                    player.LevelUp();
                }
            }

        }
    }

}
