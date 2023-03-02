using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ����� ���� ���� ���õ� Ŭ����
/// </summary>
public class PlayerWeapon : MonoBehaviour, IBattle
{
    Player player;

    private float attackStopEffectTime = 0.05f;

    /// <summary>
    /// ���Ͱ� �׾��� �� ��ü������ ����ġ ��ӿö� ó�� �׾��� ���� �������� Attack�Լ����� ü���� 0���� ū���¿��� 0���� �۾����� boolŸ�� �ߵ�
    /// </summary>
    bool isCheckExp = false;     

    public float AttackDamage { get; set; }

    public float SkillDamage { get; set; }
    public float Defence { get; set; }

    public float AttackStopEffectTime
    {
        get
        {
            return attackStopEffectTime;
        }
        set
        {
            attackStopEffectTime = value;
        }
    }

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
    /// ���� ����� ����� �޼���, Ihealth�� �������̽��� ������ �ִ� ������ ��ȣ�ۿ�
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
    /// ��ų ���ݽ� ���� �޼���, Ihealth�� �������̽��� ������ �ִ� ������ ��ȣ�ۿ�
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
    /// ������ Ʈ���Ű� �۵��� ����� �޼���
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) //ontriggerenter�� �����ϸ� ���� �ȵȴ�.
    {
        //�÷��̾� Į���ִ� �ö��̴��� Ʈ����
        if (other.CompareTag("Monster"))
        {
            SoundPlayer.Instance?.PlaySound(SoundType.Sound_Hit);

            Monster monster;
            monster = other.GetComponent<Monster>();

            if(!player.isSkillUsing)
            {
                Attack(monster);
            }
            else
            {
                StartCoroutine(AttackStopEffect());
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

    IEnumerator AttackStopEffect()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(AttackStopEffectTime);
        Time.timeScale = 1.0f;
    }

}
