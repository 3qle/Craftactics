using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Magic  : Attribute
{
    public override void SetName()
    {
        Name = "MAG";
        start = max;
        attributeType = AtrributeTypes.Magic;
    }
}
