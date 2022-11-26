using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Health 
{
  
    public enum HealthEnum { Healthy, Wounded, Weaken, AtDead }
    public HealthEnum healthStat;

    public int HP;
    public int MaxHP;
    
    public void SetEnemyHealthStatus()
    {
    //    if (HP > (float)MaxHP - (float)MaxHP/4) healthStatus = HealthEnum.Healthy;
    //    if (HP <= MaxHP - (float)MaxHP / 4 && HP > (float)MaxHP / 2 ) healthStatus = HealthEnum.Wounded;
    //    if (HP <= (float)MaxHP / 2 && HP > (float)MaxHP / 4) healthStatus = HealthEnum.Weaken;
    //    if (HP <=(float) MaxHP / 4) healthStatus = HealthEnum.AtDead;
    }
    
    public bool isOver => HP < 0;
    public HealthEnum healthStatus => healthStat;

    public int DecreaseHealth(int damage)
    {
        HP -= damage;
        return damage;
    }
    public void SetMaxHP() => HP = MaxHP;
    
    public int HealthPoints => HP;

  
}
