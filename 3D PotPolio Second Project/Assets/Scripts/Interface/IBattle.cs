using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    public float AttackDamage { get; set; }
    //public float Defence { get; set; }
    

    public void Attack(IHealth target);


}
