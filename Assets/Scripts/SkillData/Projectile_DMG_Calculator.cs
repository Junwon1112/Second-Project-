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
        //스킬을 사용하며 무기의 스킬데미지를 설정하고 이 오브젝트가 만들어지므로 문제없을 것으로 예상
        SkillDamage = weapon.SkillDamage;
    }

    /// <summary>
    /// Attack이지만 실제로는 스킬데미지임(IBattle로 인해 필수 구현)
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
                    player.newDel_LevelUp();    //레벨업 델리게이트
                }
            }
        }
    }
}
