using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class Flow : Attribute
{
   public override void SetName()
   {
      Name = "Flow";
      start = max;
      attributeType = AtrributeTypes.Flow;
   }
}
