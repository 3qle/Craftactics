using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Managers;
using Script.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class UI
{
   private Turn _turn;
   private Pool _pool;
   private Shop _shop;
   public Console Console;
   public UIItem ItemUI;
   public UICharacterPanel InfoUI;
   public UIScore ScoreUI;
   [Header("Buttons")] 
   public Button EndTurnButton;
   public Button ResetButton;
   public Button BuyButton;
   public Button ShopButton;
   public Button SellButton;
   [Header("Containers")]
   public GameObject PopUpContainer;
   public Transform  ItemButtons;
   
   public TextMeshProUGUI  TurnAnnouncement;
   
   public void Initialize(BattleStarter starter)
   {
      _pool = starter.pool;
      _turn = starter.turn;
      _shop = starter.shop;
      
      ItemUI = new UIItem(starter, ItemButtons);
     
      InfoUI.Initialize();
      LoadButtons();
    
   }
   public void LoadButtons()
   {
      EndTurnButton.onClick.AddListener(_turn.StartNewTurn);
      ResetButton.onClick.AddListener(Reset);
      BuyButton.onClick.AddListener(() =>_shop.UIShop.Buy(true));
      ShopButton.onClick.AddListener(() => _shop.OpenShop(false));
      SellButton.onClick.AddListener(() => _shop.UIShop.Buy(false));
   }

  

   public void ShowInfoOnUpdate(Character character)
   {
      if (character == null) return;
      InfoUI.Show(character);
      ItemUI.UpdateButtons(character);
      _shop.Show(character);
    }

   public void ShowInfoOnSelect(Character character)
   {
      ItemUI.ChangeCharacter(character);
     _shop.UIShop.ChangeCategories(character);
   }
   
   public void ShowPopUp(AttackResult result, Vector3 pos, float damage) 
      => _pool.PopUpList[Random.Range(0,_pool.PopUpList.Count)].ShowPopUp(result,pos,damage);
   
   public void Reset()
      => SceneManager.LoadScene(0);
   
   public void UpdateTurnText(TurnState act)
   {
      // StartCoroutine(ShowTurnAnnouncement(act));
//      TurnCount.color = act == TurnState.E ? Color.red : Color.green;
      EndTurnButton.gameObject.SetActive(act == TurnState.P); 
   }

   IEnumerator ShowTurnAnnouncement(TurnState act)
   {
      TurnAnnouncement.gameObject.SetActive(true);
      TurnAnnouncement.text = act == TurnState.E ? "Enemy Turn" : "Player Turn";
      yield return new WaitForSeconds(1);
      TurnAnnouncement.gameObject.SetActive(false);
   }
 
  

}