using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 스킬 구현부
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
    /// 스킬 ID를 입력하면 해당 스킬의 구현 함수를 불러옴
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

        //스킬 데미지 설정
        weapon.SkillDamage = tempSkill_Duration.skillDamage * (tempSkill_Duration.skillLevel * 0.5f) + (player.AttackDamage * 0.2f);

        float skillUsingTime = tempSkill_Duration.skillDuration;    //스킬지속시간

        float compensateTime = 0.5f;    //파티클용 프리팹 지속시간 보정용 시간
        Quaternion compensateRotaion = Quaternion.Euler(-90.0f, 0.0f, 0.0f);    //파티클용 프리팹 회전 보정
        Vector3 compensatePosition = new Vector3(0, -1.5f, 0);  //파티클용 프리팹 위치 보정

        ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_WheelWind, player.transform,
            player.transform.position + compensatePosition, player.transform.rotation * compensateRotaion, skillUsingTime + compensateTime);
    }

    private void Skill_AirSlash(int skillID)
    {
        SkillData_Shooting tempSkill_Data = GameManager.Instance.SkillDataManager.FindSkill_Shooting(skillID);

        //스킬데미지 설정
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.7f);


        Vector3 compensatePosition = new Vector3(1.0f, 0.5f, 0);  //투사체 프리팹 위치 보정

        Instantiate(tempSkill_Data.projectile_Prefab, player.transform.position + compensatePosition, player.transform.rotation);
    }    


}
