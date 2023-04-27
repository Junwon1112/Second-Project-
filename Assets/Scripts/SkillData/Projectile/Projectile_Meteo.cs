using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Meteo : MonoBehaviour, IBattle
{
    Player player;
    bool isCheckExp = false;
    float skillDamage;
    PlayerWeapon weapon;
    float lifetime = 4.0f;
    float rewindTime = 0.2f;

    public float AttackDamage { get; set; }

    public float SkillDamage { get; set; }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    private void Start()
    {
        SkillDamage = weapon.SkillDamage;
        Destroy(gameObject, lifetime);
    }

    //이름은 Attack이지만 내부에서는 스킬데미지로 적용한다
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

    private void OnTriggerStay(Collider other)
    {
        rewindTime -= Time.deltaTime;
        if(rewindTime < 0)
        {
            rewindTime = 0.2f;  //원래값으로 되돌림

            if (other.CompareTag("Monster"))
            {
                SoundPlayer.Instance?.PlaySound(SoundType.Sound_FireHit);
                ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_FireHit, other.ClosestPoint(transform.position), transform.rotation);

                Monster_Basic monster;
                monster = other.GetComponent<Monster_Basic>();

                Attack(monster);

                monster.SetHP();
                if (monster.HP <= 0 && isCheckExp)
                {
                    isCheckExp = false;
                    player.Exp += monster.GiveExp;
                    player.SetExp();
                    if (player.Exp >= player.MaxExp)
                    {
                        player.newDel_LevelUp();    //레벨업 델리게이트
                    }
                }
            }

            
        }

    }


}
