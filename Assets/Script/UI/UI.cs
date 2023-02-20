using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Enum;
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
   [Header("Containers")]
   public GameObject PopUpContainer;
   public Transform  ItemButtons;
   
   public TextMeshProUGUI  TurnAnnouncement;
   public Transform Switches;
   public void Initialize(BattleStarter starter)
   {
      _pool = starter.pool;
      _turn = starter.turn;
      _shop = starter.shop;
      
      ItemUI = new UIItem(starter, ItemButtons,Switches);
     
      InfoUI.Initialize();
      LoadButtons();
      
 Console.Clear();
      Status.ShowText += ShowPopUp;

   }

   public void InitOnAwake()
   {
      Console.Initialize(this);
   }
   public void LoadButtons()
   {
      EndTurnButton.onClick.AddListener(_turn.StartNewTurn);
      ResetButton.onClick.AddListener(Reset);
     
      
    
   }

   public void ShowSwitches(bool show)
   {
      Switches.gameObject.SetActive(show);
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
   
   public void ShowPopUp( Status status, Vector2 pos) 
      => _pool.PopUpList[0].ShowPopUp(status, pos, _pool.PopUpList);
   
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