using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Shop
{
 public UIShop UIShop;
 public Wallet Wallet;
 public bool inShop;

 public void Initialize(Spawner spawner)
 { 
  UIShop.Initialize(spawner,this);
 }

 public void Show()
 {
  
 }
 
}
