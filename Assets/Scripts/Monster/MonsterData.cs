using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Monster Data"), menuName = ("Scriptable Object_Monster Data/Monster Data"), order = 1)]
public class MonsterData : ScriptableObject
{
    public string monsterName;

    public MonsterType monsterType;
    public int monsterID;

    public int monsterLV;

    public float moveSpeed;
    public float maxHP;
    public float exp;
    public float attackDamage;
    public float defence;
    public float attackDelay;
}

public enum MonsterType
{
    TurtleShell = 0,
    Slime,
}

/// <summary>
/// 몬스터 상태 체크용 enum
/// </summary>
public enum MonsterState
{
    patrol = 0,
    chase,
    combat,
    die
}
