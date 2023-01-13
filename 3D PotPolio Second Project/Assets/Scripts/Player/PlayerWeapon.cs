using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IBattle
{
    Player player;
    AllQuickSlotUI allQuickSlotUI;

    float attackDamage;
    float skillDamage;          //��ų�������� �ܺ� Ŭ�������� �������� ����
    float defence;
    
    bool isCheckExp = false;     //���Ͱ� �׾��� �� ��ü������ ����ġ ��ӿö� ó�� �׾��� ���� �������� Attack�Լ����� ü���� 0���� ū���¿��� 0���� �۾����� boolŸ�� �ߵ�

    public float AttackDamage { get; set; }

    public float SkillDamage { get; set; }
    public float Defence { get; set; }


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        allQuickSlotUI = FindObjectOfType<AllQuickSlotUI>();
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

    public void SkillAttack(IHealth target)
    {
        if (target.HP >= 0)
        {

            target.HP -= (SkillDamage - target.Defence);

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

            if(!player.isSkillUsing)
            {
                Attack(monster);
            }
            else
            {
                SkillAttack(monster);
            }
            
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
