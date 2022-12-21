using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class Accuracy  : Attribute
{
   

   public bool TryEvade() => Random.Range(0,25) < current ;


   public override void SetName()
   {
      Name = "ACC";
      start = max;
      attributeType = AtrributeTypes.Accuracy;
   }
}
