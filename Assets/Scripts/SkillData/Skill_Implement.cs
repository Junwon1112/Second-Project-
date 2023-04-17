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

    private void Awake()
    {
        player = FindObjectOfType<Player>();
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


}
