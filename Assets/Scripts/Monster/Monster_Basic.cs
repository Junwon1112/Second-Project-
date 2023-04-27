using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster_Basic : MonoBehaviour, IHealth
{
    public abstract Transform CharacterTransform { get; }
    public abstract float AttackDamage { get; set; }
    public abstract float AttackDelay { get; set; }
    public abstract float Defence { get; set; }
    public abstract float MoveSpeed { get; set; }
    public abstract float MaxHP { get; set; }
    public abstract float HP { get; set; }
    public abstract float GiveExp { get; set; }
    public abstract void SetMonsterState(MonsterState mon);
    public abstract void SetHP();
    public abstract void MoveSlow(float slowRate, float time);
}
