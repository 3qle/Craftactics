 using System;

 [Serializable]
public class Wallet
 {
  public int Coins;

  public void Add(int amount) => Coins += amount;

  public void Spend(int amount) => Coins -= amount;

  public bool IsEnough(int cost) => Coins > cost;
 }
