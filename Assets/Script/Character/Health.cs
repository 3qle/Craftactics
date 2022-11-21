using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Health : IHealth
{
    public Health(int hp)
   {
       HP = hp;
   }
    public enum HealthEnum { Healthy, Wounded, Weaken, AtDead }
    public HealthEnum healthStat;

    public int HP;
    
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
    public void SetHP(int hp) => HP = hp;
    public int HealthPoints => HP;

    public void Visualize(IHealthView view)
    {
        view.DisplayHealth(HP);
    }
}
