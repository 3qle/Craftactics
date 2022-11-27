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
[Serializable]
public class UI
{
   [Header("Resistance view")]
   public UIResistance UIResistance;
   [Header("Weapon view")]
   public UIWeaponButtons UIWeaponButtonLeft;
   public UIWeaponButtons UIWeaponButtonRight;
   [Header("Basic Stats view")] 
   public UIBaseInfo UIBaseInfo;
   
   public TextMeshProUGUI  TurnAnnouncement;
  
   public TextMeshProUGUI TurnCount;
   [Header("Buttons")]
   public Button EndTurnButton;
  
  
   private Character Selected;

   public GameObject PopUpContainer;

   private Turn _turn;

   private Pool _pool;
   public void Initialize(Turn turn, Pool pool)
   {
      _pool = pool;
      _turn = turn;
      LoadButtons();
   }

   

   public void LoadButtons()
   {
      EndTurnButton.onClick.AddListener(_turn.StartNewTurn);
    
      UIWeaponButtonRight.SetButton();
      UIWeaponButtonLeft.SetButton();
   } 
   
   public void ShowResistances(Dictionary<Element,ResistanceType> resistanceTypes)
      => UIResistance.ShowResistance(resistanceTypes);

   public void ShowLeftWeapon(Character fightable, Weapon weapon, WeaponHand hand) =>
      UIWeaponButtonLeft.ShowButton(fightable, weapon, hand);
   
   public void ShowRightWeapon(Character fightable, Weapon weapon, WeaponHand hand) =>
      UIWeaponButtonRight.ShowButton(fightable, weapon, hand);

   public void ShowBaseInfo(int hp, int ap, string name)
      => UIBaseInfo.ShowInfo(hp, ap, name);
  
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
   public void ShowTurnCount(int turns) => TurnCount.text = turns.ToString();
 
   public void ShowPopUp(ResistanceType result, Vector3 pos, int damage) => _pool.PopUpList[0].ShowPopUp(result,pos,damage);
   
   public void Reset() => SceneManager.LoadScene(0);
   
 

}