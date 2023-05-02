using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_AirSlash : MonoBehaviour, IBattle
{
    Player player;
    bool isCheckExp = false;
    float skillDamage;
    PlayerWeapon weapon;
    Vector3 dir;
    float lifetime = 3.0f;

    public float SkillDamage { get; set; }

    public float AttackDamage { get; set; }


    private float moveSpeed = 20.0f;
    private float rotateSpeed = 30.0f;

    Quaternion rotate;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    private void Start()
    {
        SkillDamage = weapon.SkillDamage;
        rotate = Quaternion.Euler(0, Time.deltaTime * rotateSpeed, 0);
        dir = player.transform.forward;
        transform.localRotation = player.transform.rotation;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += dir * Time.deltaTime * moveSpeed;
            //new Vector3(0, 0, moveSpeed_Z * Time.deltaTime);
        transform.rotation *=  rotate;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            SoundPlayer.Instance?.PlaySound(SoundType.Sound_ElectroHit);
            ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_ElectroHit, other.ClosestPoint(transform.position), transform.rotation);

            Monster_Basic monster;
            monster = other.GetComponent<Monster_Basic>();

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

            Destroy(this.gameObject, 0.05f);
        }
    }

}
