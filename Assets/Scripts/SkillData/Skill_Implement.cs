using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��ų ������
/// </summary>
public class Skill_Implement : MonoBehaviour
{
    Player player;
    PlayerWeapon weapon;
    Vector3 dir;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        dir = player.transform.forward;
        Debug.DrawRay(player.transform.position, dir, Color.red);
    }

    public void TakeWeapon()
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    /// <summary>
    /// ��ų ID�� �Է��ϸ� �ش� ��ų�� ���� �Լ��� �ҷ���
    /// </summary>
    /// <param name="skillID"></param>
    public void PlaySkill(int skillID)
    {
        switch (skillID)
        {   
            case 0:
                Skill_Wheelwind(skillID);
                break;
            case 1:
                Skill_AirSlash(skillID);
                break;
            case 2:
                Skill_DashAttack(skillID);
                break;

            default:
                Debug.Log("Don't Exist SkillID");
                break;
        }
    }


    private void Skill_Wheelwind(int skillID)
    {
        SkillData_Duration tempSkill_Duration = GameManager.Instance.SkillDataManager.FindSkill_Duration(skillID);

        //��ų ������ ����
        weapon.SkillDamage = tempSkill_Duration.skillDamage * (tempSkill_Duration.skillLevel * 0.5f) + (player.AttackDamage * 0.2f);

        float skillUsingTime = tempSkill_Duration.skillDuration;    //��ų���ӽð�

        float compensateTime = 0.5f;    //��ƼŬ�� ������ ���ӽð� ������ �ð�
        Quaternion compensateRotaion = Quaternion.Euler(-90.0f, 0.0f, 0.0f);    //��ƼŬ�� ������ ȸ�� ����
        Vector3 compensatePosition = new Vector3(0, -1.5f, 0);  //��ƼŬ�� ������ ��ġ ����

        ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_WheelWind, player.transform,
            player.transform.position + compensatePosition, player.transform.rotation * compensateRotaion, skillUsingTime + compensateTime);
    }

    private void Skill_AirSlash(int skillID)
    {
        SkillData_Shooting tempSkill_Data = GameManager.Instance.SkillDataManager.FindSkill_Shooting(skillID);

        //��ų������ ����
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.7f);


        Vector3 compensatePosition = new Vector3(1.0f, 0.5f, 0);  //����ü ������ ��ġ ����

        Instantiate(tempSkill_Data.projectile_Prefab, player.transform.position + compensatePosition, player.transform.rotation);
    }    

    private void Skill_DashAttack(int skillID)
    {
        SkillData_Normal tempSkill_Data = GameManager.Instance.SkillDataManager.FindSkill_Normal(skillID);

        //��ų������ ����
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.5f);

        
        float moveDistance = 20.0f;

        RaycastHit raycastHit;

        if(Physics.Raycast(player.transform.position, dir, out raycastHit, moveDistance, LayerMask.NameToLayer("Monster")))
        {
            player.transform.LookAt(raycastHit.transform.position);
            player.transform.position = Vector3.Lerp(player.transform.position, raycastHit.transform.position , 0.95f);
        }
        

        player.transform.position += dir * moveDistance;
        

    }
}
