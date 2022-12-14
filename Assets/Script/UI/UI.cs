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
   public Console Console;
   
   [Header("Resistance view")]
   public UIResistance UIResistance;

   [Header("Experience view")] public UIExperience UIExperience;
   [Header("Item view")]
   public UIItem UIItem;
   
   [Header("Basic Stats view")] 
   public UIBaseInfo UIBaseInfo;
   
   [Header("Character view")] 
   public UICharacter UICharacter;

   public TextMeshProUGUI  TurnAnnouncement;

   [Header("Buttons")] 
   public Button EndTurnButton;
   public Button ResetButton;
   public Button StatusButton;
   public Button StatsButton;
   
   public GameObject PopUpContainer;

   private Turn _turn;
   private Controller _controller;
   private Pool _pool;
   
   public void Initialize(Turn turn, Pool pool, Controller controller, UIShop shop)
   {
      _controller = controller;
      _pool = pool;
      _turn = turn;
      LoadButtons();
    
   }
   public void LoadButtons()
   {
      EndTurnButton.onClick.AddListener(_turn.StartNewTurn);
      ResetButton.onClick.AddListener(Reset);
      StatsButton.onClick.AddListener(ShowStats);
      StatusButton.onClick.AddListener(ShowStatus);
      UIItem.Initialize(Console);
      UICharacter.Initialize(_controller);
  
   }

   void ShowStatus()
   {
      UIResistance.ShowResistanceContainer(true);
      UIBaseInfo.ShowStatsContainer(false);
   }
   void ShowStats()
   {
      UIResistance.ShowResistanceContainer(false);
      UIBaseInfo.ShowStatsContainer(true);
   }

   public void ShowInfoOnUpdate(Character character)
   { 
      UICharacter.Show();
      UIResistance.Show(character);
      UIBaseInfo.Show(character);
      UIExperience.Show(character);
      UIItem.UpdateButtons(character);
      UIExperience.SetButtons(character);
   }

   public void ShowInfoOnSelect(Character character)
   {
      UIItem.ChangeItems(character);
   }
   
   public void ShowPopUp(AttackResult result, Vector3 pos, int damage) 
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