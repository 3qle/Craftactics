 using System;
 using TMPro;

 [Serializable]
public class Wallet
 {
  public int Coins;
  public TextMeshProUGUI CoinsText;
  public void Add(int amount) => Coins += amount;

  public void Use(int amount, bool spend)
  {
   Coins = spend ? Coins -= amount : Coins += (int)(amount * 0.65f);
   CoinsText.text = GetCoins();
  } 

  public bool IsEnough(int cost) => Coins >= cost;

  public string GetCoins() => Coins.ToString();
 
 }
