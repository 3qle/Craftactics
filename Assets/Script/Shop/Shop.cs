using System;
using System.Collections;
using System.Collections.Generic;
using Script.Managers;
using UnityEngine;
[Serializable]
public class Shop
{
 public bool inShop;
 public Transform ShopContainer;
 
 public UIShop UIShop;
 public Wallet Wallet;
 
 private Turn _turn;
 private Spawner _spawner;

 public void Initialize(Spawner spawner)
 {
  _turn = spawner.turn;
  _spawner = spawner;
  OpenShop(true);
  
 }

 public void OpenShop(bool open)
 {
  
  UIShop.Initialize(_spawner,this);
  ShopContainer.localScale = open ? new Vector3(1, 1, 1) : Vector3.zero;
  _turn.Act = open ? TurnState.Shop : TurnState.E;
  if(!open) _turn.StartNewTurn();
 }
 
}
