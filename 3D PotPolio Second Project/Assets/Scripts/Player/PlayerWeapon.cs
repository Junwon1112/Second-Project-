using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 무기와 전투 담당과 관련된 클래스
/// </summary>
public class PlayerWeapon : MonoBehaviour, IBattle
{
    Player player;

    /// <summary>
    /// 몬스터가 죽었을 때 시체때리면 경험치 계속올라서 처음 죽었을 때만 오르도록 Attack함수에서 체력이 0보다 큰상태에서 0보다 작아지면 bool타입 발동
    /// </summary>
    bool isCheckExp = false;     

    public float AttackDamage { get; set; }

    public float SkillDamage { get; set; }
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

    /// <summary>
    /// 공격 실행시 실행될 메서드, Ihealth를 인터페이스로 가지고 있는 대상과만 상호작용
    /// </summary>
    /// <param name="target"></param>
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

    /// <summary>
    /// 스킬 공격시 사용될 메서드, Ihealth를 인터페이스로 가지고 있는 대상과만 상호작용
    /// </summary>
    /// <param name="target"></param>
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

    /// <summary>
    /// 무기의 트리거가 작동시 실행될 메서드
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) //ontriggerenter는 복붙하면 실행 안된다.
    {
        //플레이어 칼에있는 컬라이더의 트리거
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
