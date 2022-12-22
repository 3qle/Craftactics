using System;
using System.Collections;
using System.Collections.Generic;
using Script.Managers;
using UnityEngine;

[Serializable]
public class Shop
{
 public Wallet Wallet;
 public EnemyPreview EnemyPreview;
 public UIShop UIShop;
 private Turn _turn;
 private Spawner _spawner;
 
 public bool inShop;
 public Transform ShopContainer;


 public void Initialize(Spawner spawner)
 {
  _turn = spawner.turn;
  _spawner = spawner;
  EnemyPreview.Initialize(spawner);
  OpenShop(true);
 
 }

 public void OpenShop(bool open)
 {
  if ((_spawner.pool.ActiveHeroes.Count > 0 && _spawner.pool.ActiveHeroes[0].Bag.Items.Count > 0 && !open) || open)
  {
   Wallet.Use(0,true);
   UIShop.Initialize(_spawner,this);
   ShopContainer.localScale = open ? new Vector3(1, 1, 1) : Vector3.zero;
   _turn.Act = open ? TurnState.Shop : TurnState.E;
   EnemyPreview.ShowNextEnemy();
   if(!open) _turn.StartNewTurn();
  }
 
 }
 
}
