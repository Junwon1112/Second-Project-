using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IBattle
{
    Player player;

    float attackDamage;
    float defence;
    bool isCheckExp = false;     //���Ͱ� �׾��� �� ��ü������ ����ġ ��ӿö� ó�� �׾��� ���� �������� Attack�Լ����� ü���� 0���� ū���¿��� 0���� �۾����� boolŸ�� �ߵ�
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

    private void OnTriggerEnter(Collider other) //ontriggerenter�� �����ϸ� ���� �ȵȴ�.
    {
        //�÷��̾� Į���ִ� �ö��̴��� Ʈ����
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
