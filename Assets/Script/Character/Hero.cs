using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{

    protected  void Awake()
    {
        Icon = GetComponent<SpriteRenderer>();
    }


 

}
