using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Shop
{
 public UIShop UIShop;
 public Wallet Wallet;

 public void Initialize(Pool pool)
 {
  UIShop.Initialize(pool);
 }
 
}
