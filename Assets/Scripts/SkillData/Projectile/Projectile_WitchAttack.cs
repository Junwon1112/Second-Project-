using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_WitchAttack : MonoBehaviour, IBattle
{
    Player player;
    bool isCheckExp = false;
    float skillDamage;
    PlayerWeapon weapon;
    Vector3 dir;
    float lifetime = 0.5f;

    public float AttackDamage { get; set; }


    private float projectileSpeed = 40.0f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    private void Start()
    {
        AttackDamage = weapon.AttackDamage;
        dir = player.transform.forward;
        transform.localRotation = player.transform.rotation;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += dir * Time.deltaTime * projectileSpeed;
    }

    public void Attack(IHealth target)
    {
        if (target.HP >= 0)
        {
            float realTakeDamage = AttackDamage - target.Defence;
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
            SoundPlayer.Instance?.PlaySound(SoundType.Sound_IceHit);
            ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_IceHit, other.ClosestPoint(transform.position), transform.rotation);

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

            Destroy(this.gameObject);
        }
    }

}
