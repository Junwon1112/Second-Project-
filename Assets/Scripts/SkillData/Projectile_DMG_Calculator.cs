using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_DMG_Calculator : MonoBehaviour , IBattle
{
    Player player;
    bool isCheckExp = false;
    float skillDamage;
    PlayerWeapon weapon;

    public float SkillDamage { get; set; }

    public float AttackDamage { get; set; }

    

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    private void Start()
    {
        //��ų�� ����ϸ� ������ ��ų�������� �����ϰ� �� ������Ʈ�� ��������Ƿ� �������� ������ ����
        SkillDamage = weapon.SkillDamage;
    }

    /// <summary>
    /// Attack������ �����δ� ��ų��������(IBattle�� ���� �ʼ� ����)
    /// </summary>
    /// <param name="target"></param>
    public void Attack(IHealth target)
    {
        if (target.HP >= 0)
        {
            float realTakeDamage = SkillDamage - target.Defence;
            target.HP -= (realTakeDamage);

            DMGTextPlayer.Instance?.CreateDMGText(target.CharacterTransform, target.CharacterTransform.position + new Vector3(0, 1.0f, 0),
                target.CharacterTransform.rotation, realTakeDamage);

            if (target.HP <= 0)
            {
                isCheckExp = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster;
            monster = other.GetComponent<Monster>();

            Attack(monster);

            monster.SetHP();
            if (monster.HP <= 0 && isCheckExp)
            {
                isCheckExp = false;
                player.Exp += monster.giveExp;
                player.SetExp();
                if (player.Exp >= player.MaxExp)
                {
                    player.newDel_LevelUp();    //������ ��������Ʈ
                }
            }
        }
    }
}
