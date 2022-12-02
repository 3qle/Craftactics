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
   public Console Console;
   
   [Header("Resistance view")]
   public UIResistance UIResistance;
   
   [Header("Weapon view")]
   public UIWeaponButtons WeaponButtons;
 
   
   [Header("Basic Stats view")] 
   public UIBaseInfo UIBaseInfo;
   
   [Header("Character view")] 
   public UICharacter UICharacter;

   public TextMeshProUGUI  TurnAnnouncement;

   [Header("Buttons")]
   public Button EndTurnButton;
   
   public GameObject PopUpContainer;

   private Turn _turn;
   private Controller _controller;
   private Pool _pool;
   
   public void Initialize(Turn turn, Pool pool, Controller controller)
   {
      _controller = controller;
      _pool = pool;
      _turn = turn;
      LoadButtons();
   }
   public void LoadButtons()
   {
      EndTurnButton.onClick.AddListener(_turn.StartNewTurn);
      WeaponButtons.Initialize(Console);
      UICharacter.Initialize(_pool.HeroesList,_controller,Console);
   }

   public void ShowHeroesButtons() => UICharacter.Show();
   
   public void ShowResistances(Dictionary<Element,ResistanceType> resistanceTypes)
      => UIResistance.ShowResistance(resistanceTypes);

   public void HighLightCharacterButton(int i, bool _selected) 
      => UICharacter.ShowHighLight(i,_selected);
   public void ShowActiveItems(Character character) =>
      WeaponButtons.ShowButton(character);
   
   
   public void ShowBaseInfo(Character character)
      => UIBaseInfo.ShowInfo(character);
   
   public void ShowPopUp(ResistanceType result, Vector3 pos, int damage) 
      => _pool.PopUpList[0].ShowPopUp(result,pos,damage);
   
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