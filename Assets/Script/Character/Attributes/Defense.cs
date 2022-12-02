using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Defense
{
    public int DEF;
    public int Calculate(int input)
    {
        int output = (int)(input - input * (float)DEF/25) ;
        return output;
    }
}
