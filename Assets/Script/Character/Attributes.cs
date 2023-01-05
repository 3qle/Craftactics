using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attributes
{
    public Health health;
    public Stamina stamina;
    public Accuracy accuracy;
    public Support support;
    public Magic magic;
    public Strenght strength;
    public Flow flow;
    
    public List<Attribute> AttributesList;
    public List<Status> StatusList = new List<Status>();
    List<Status> RemoveList = new List<Status>();
    public void Initialize()
    {
        SetAttributes();
    }

    public void SetStatus(Status status)
    {
        if(!StatusList.Contains(status))
         StatusList.Add(status);
        
    }
    public void SetAttributes()
    {
        AttributesList = new List<Attribute> {strength,accuracy,magic,support,flow, health, stamina};
        foreach (var stat in AttributesList)
        {
            stat.Initialize(StatusList);
        } 
        stamina.Initialize(StatusList); 
        health.Initialize(StatusList);
    }

    public void CheckActiveModifiers()
    {
        for (int i = 0; i < StatusList.Count; i++)
        {
            StatusList[i].CheckDuration();
            if(StatusList[i].Disable) RemoveList.Add(StatusList[i]);
        }

        foreach (var status in RemoveList)
            StatusList.Remove(status);
        RemoveList.Clear();
    }

    public void ResetPassives()
    {
       
    }
}
