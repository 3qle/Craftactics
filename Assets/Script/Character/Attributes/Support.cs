using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Support  : Attribute
{
    public override void SetName()
    {
        Name = "Spirit";
        start = max;
        attributeType = AtrributeTypes.Spirit;
    }
}
