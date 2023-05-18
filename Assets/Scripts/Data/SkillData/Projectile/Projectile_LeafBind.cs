using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_LeafBind : MonoBehaviour, IBattle
{
    Player player;
    bool isCheckExp = false;
    float skillDamage;
    PlayerWeapon weapon;
    float lifetime = 3.0f;
    float damageInvterval = 0.2f;

    Monster_Basic monster;
    float bindTime = 3.0f;
    bool isEnd = false;
    
    public float AttackDamage { get; set; }
    public float SkillDamage { get; set; }


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<PlayerWeapon>();
        monster = transform.parent.GetComponent<Monster_Basic>();
    }

    private void Start()
    {
        SkillDamage = weapon.SkillDamage;
        StartCoroutine(CoGiveDamage());

        monster.MoveSlow(0, bindTime);

        Destroy(gameObject, lifetime);
        
    }
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

    IEnumerator CoGiveDamage()
    {
        while(!isEnd)
        {
            lifetime -= damageInvterval;
            if(lifetime <= 0)
            {
                isEnd = true;
            }

            SoundPlayer.Instance?.PlaySound(SoundType.Sound_LeafHit);

            Attack(monster);

            monster.SetHP();
            if (monster.HP <= 0 && isCheckExp)
            {
                isCheckExp = false;
                player.Exp += monster.GiveExp;
                //player.SetExp();
                //if (player.Exp >= player.MaxExp)
                //{
                //    player.newDel_LevelUp();    //레벨업 델리게이트
                //}
            }
            yield return new WaitForSeconds(damageInvterval);
        }
        
    }

    
}
