using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/// <summary>
/// ���� ��ų ������
/// skillId 0~ 9�� swordman, 10~19�� ������
/// </summary>
public class Skill_Implement : MonoBehaviour
{
    public static Skill_Implement Instance;

    Player player;
    PlayerWeapon weapon;

    Animator anim;
    Camera camera;

    PointerEventData pointerEventData;
    RaycastHit hit;

    public bool IsWitchLeafBind_ClickUsing { get; private set; }
    public bool IsFindTarget { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        player = FindObjectOfType<Player>();
        camera = player.gameObject.GetComponentInChildren<Camera>();
        anim = player.transform.GetComponent<Animator>();
    }

    public void TakeWeapon()
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    /// <summary>
    /// ��ų ID�� �Է��ϸ� �ش� ��ų�� ���� �Լ��� �ҷ���
    /// </summary>
    /// <param name="skillID"></param>
    public void PlaySkill_SwordMan(int skillID, SkillData skillData)
    {
        switch (skillID)
        {   
            case 0:
                StartCoroutine(SetAnimTime_SkillDuration(skillData, SkillDataManager.Instance.FindSkill_Duration(skillID).skillDuration));
                SwordMan_Skill_Wheelwind(skillID);
                break;
            case 1:
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                SordMan_Skill_AirSlash(skillID);
                break;
            case 2:
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                Debug.Log("�ִϸ��̼ǿ��� �ߵ�");
                //Skill_DashAttack(skillID);
                break;

            default:
                Debug.Log("Don't Exist SkillID");
                break;
        }
    }

    public void PlaySkill_Witch(int skillID, SkillData skillData)
    {
        switch (skillID)
        {
            case 10:
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                Debug.Log("�ִϸ��̼ǿ��� �ߵ�");
                break;
            case 11:
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                Witch_Skill_BlackHole(skillID);
                break;
            case 12:
                anim.SetTrigger($"IsSkillUse_{skillData.skillName}");
                Witch_Skill_LeafBind(skillID);
                break;

            default:
                Debug.Log("Don't Exist SkillID");
                break;
        }
    }




    private void SwordMan_Skill_Wheelwind(int skillID)
    {
        SkillData_Duration tempSkill_Duration = SkillDataManager.Instance.FindSkill_Duration(skillID);

        float skillUsingTime = tempSkill_Duration.skillDuration;    //��ų���ӽð�

        //��ų ������ ����
        weapon.SkillDamage = tempSkill_Duration.skillDamage * (tempSkill_Duration.skillLevel * 1.0f) + (player.AttackDamage * 0.2f);

        float compensateTime = 0.5f;    //��ƼŬ�� ������ ���ӽð� ������ �ð�
        Quaternion compensateRotaion = Quaternion.Euler(-90.0f, 0.0f, 0.0f);    //��ƼŬ�� ������ ȸ�� ����
        Vector3 compensatePosition = new Vector3(0, -1.5f, 0);  //��ƼŬ�� ������ ��ġ ����

        ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_WheelWind, player.transform,
            player.transform.position + compensatePosition, player.transform.rotation * compensateRotaion, skillUsingTime + compensateTime);
    }

    /// <summary>
    /// ���������� ��ų ���ݽ� �ִϸ��̼� ���ӽð��� ����
    /// </summary>
    /// <param name="skillDuration"></param>
    /// <returns></returns>
    IEnumerator SetAnimTime_SkillDuration(SkillData skillData, float skillDuration) //��ų ���ӽð�
    {
        anim.SetBool($"IsSkillUse_{skillData.skillName}", true);
        yield return new WaitForSeconds(skillDuration);
        anim.SetBool($"IsSkillUse_{skillData.skillName}", false);
    }

    private void SordMan_Skill_AirSlash(int skillID)
    {
        SkillData_Shooting tempSkill_Data = SkillDataManager.Instance.FindSkill_Shooting(skillID);

        //��ų������ ����
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.7f);


        Vector3 compensatePosition = new Vector3(1.0f, 0.5f, 0);  //����ü ������ ��ġ ����

        Instantiate(tempSkill_Data.projectile_Prefab, player.transform.position + compensatePosition, player.transform.rotation);
    }    

    public void SwordMan_Skill_DashAttack(int skillID)   //�÷��̾� �ִϸ��̼ǿ��� ���� �����ؾ� �ؼ� �길 public���� ����
    {
        SkillData_Normal tempSkill_Data = SkillDataManager.Instance.FindSkill_Normal(skillID);

        //��ų������ ����
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.5f);

        
        float moveDistance = 20.0f;

        Vector3 dir = player.transform.forward;


        Vector3 skillRange = player.transform.position + dir * moveDistance;
        float radiusRange = 1.0f;

        int checkMonsterNum = 0;

        for(int effectInterval = 0; effectInterval < moveDistance; effectInterval++)
        {
            ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_DashAttack, player.transform.position + dir * effectInterval, player.transform.rotation);
        }
        


        Collider[] monsterColliders = new Collider[6];
        Monster_Basic[] monsters = new Monster_Basic[6];

        checkMonsterNum = Physics.OverlapCapsuleNonAlloc(player.transform.position, skillRange, radiusRange, monsterColliders, 1 << LayerMask.NameToLayer("Monster"));

        if(checkMonsterNum > 0)
        {
            for(int i = 0; i < checkMonsterNum; i++)
            {
                monsters[i] = monsterColliders[i].transform.parent.GetComponentInChildren<Monster_Basic>();

                SoundPlayer.Instance?.PlaySound(SoundType.Sound_WindHit);

                weapon.SkillAttack(monsters[i]);

                monsters[i].SetHP();
                if (monsters[i].HP <= 0 && weapon.isCheckExp)
                {
                    weapon.isCheckExp = false;
                    player.Exp += monsters[i].GiveExp;
                    player.SetExp();
                    if (player.Exp >= player.MaxExp)
                    {
                        player.newDel_LevelUp();    //������ ��������Ʈ
                    }
                }
            }
        }

        player.transform.position += dir * moveDistance;

    }

    public void Witch_Skill_Meteo(int skillID)   
    {
        SkillData_Shooting tempSkill_Data = SkillDataManager.Instance.FindSkill_Shooting(skillID);

        //��ų������ ����
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.2f);


        Vector3 compensatePosition = player.transform.forward * 6;  //����ü ������ ��ġ ����

        Instantiate(tempSkill_Data.projectile_Prefab, player.transform.position + compensatePosition, player.transform.rotation);
        SoundPlayer.Instance?.PlaySound(SoundType.Sound_Skill_Meteo);
    }

    private void Witch_Skill_BlackHole(int skillID)
    {
        SkillData_Shooting tempSkill_Data = SkillDataManager.Instance.FindSkill_Shooting(skillID);

        //��ų������ ����
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.1f);


        Vector3 compensatePosition = new Vector3(0,1.2f,0) + player.transform.forward * 6;  //����ü ������ ��ġ ����

        Instantiate(tempSkill_Data.projectile_Prefab, player.transform.position + compensatePosition, player.transform.rotation);
        SoundPlayer.Instance?.PlaySound(SoundType.Sound_Skill_BlackHole);
    }

    private void Witch_Skill_LeafBind(int skillID)
    {
        IsWitchLeafBind_ClickUsing = true;
        player.input.Player.Attack.performed -= player.OnAttackInput;
        player.input.Skill_Implement.ClickTarget.performed += OnTargetInput;


        SkillData_Shooting tempSkill_Data = SkillDataManager.Instance.FindSkill_Shooting(skillID);

        //��ų������ ����
        weapon.SkillDamage = tempSkill_Data.skillDamage * (tempSkill_Data.skillLevel * 1.0f) + (player.AttackDamage * 0.1f);

        float skillRange = 20.0f;

        StartCoroutine(CoFindEnemy(skillRange));



        //Instantiate(tempSkill_Data.projectile_Prefab, player.transform.position, player.transform.rotation);
        //SoundPlayer.Instance?.PlaySound(SoundType.Sound_Skill_BlackHole);
    }


    IEnumerator CoFindEnemy(float skillRange)
    {
        while (IsWitchLeafBind_ClickUsing)
        {
            Cursor.SetCursor(CursorManager.Instance.findCursorImage, new Vector2(5, 5), CursorMode.Auto);

            if(Physics.Raycast(camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit, skillRange, 1 << LayerMask.NameToLayer("Monster")))
            {
                Cursor.SetCursor(CursorManager.Instance.targetCursorImage, new Vector2(5, 5), CursorMode.Auto);
                IsFindTarget = true;
            }
            else
            {
                IsFindTarget = false;
            }
            
            yield return new WaitForFixedUpdate();
        }
        
    }

    private void OnTargetInput(InputAction.CallbackContext obj)
    {
        if (IsWitchLeafBind_ClickUsing)
        {
            Transform targetMonster = hit.transform;
            int leafBindSkillID = 12;
            if (targetMonster != null)
            {
                SkillData_Shooting tempSkill_Data = SkillDataManager.Instance.FindSkill_Shooting(leafBindSkillID);
                Instantiate(tempSkill_Data.projectile_Prefab ,targetMonster.transform);
                SoundPlayer.Instance?.PlaySound(SoundType.Sound_Skill_LeafBind);
            }

            IsWitchLeafBind_ClickUsing = false;
            Cursor.SetCursor(CursorManager.Instance.defaultCursorImage, new Vector2(5, 5), CursorMode.Auto);
            player.input.Player.Attack.performed += player.OnAttackInput;
            player.input.Skill_Implement.ClickTarget.performed -= OnTargetInput;
        }
    }

}
