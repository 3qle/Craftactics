using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Strenght  : Attribute
{
    public override void SetName()
    {
        Name = "Strength";
        start = max;
        attributeType = AtrributeTypes.Strength;
    }
}
