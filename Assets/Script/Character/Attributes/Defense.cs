using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Defense : Attribute
{
    
    public int Calculate(int input)
    {
        int output = (int)(input - input * (float)current/25) ;
        return output;
    }


    public override void SetName()
    {
        Name = "DEF";
    }
}
