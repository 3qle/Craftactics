using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Health  : Attribute
{
  
    public enum HealthEnum { Healthy, Wounded, Weaken, AtDead }
    HealthEnum healthStat;

   
   
    
    public void SetEnemyHealthStatus()
    {
    //    if (HP > (float)MaxHP - (float)MaxHP/4) healthStatus = HealthEnum.Healthy;
    //    if (HP <= MaxHP - (float)MaxHP / 4 && HP > (float)MaxHP / 2 ) healthStatus = HealthEnum.Wounded;
    //    if (HP <= (float)MaxHP / 2 && HP > (float)MaxHP / 4) healthStatus = HealthEnum.Weaken;
    //    if (HP <=(float) MaxHP / 4) healthStatus = HealthEnum.AtDead;
    }
    
    public bool isOver => current <= 0;
    public HealthEnum healthStatus => healthStat;

    public int Loose(int damage)
    {
        current -= damage;
        return damage;
    }


    public override void SetName()
    {
        Name = "HLTH";
    }
}
