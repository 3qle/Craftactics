using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour, IUIble
{
   public TextMeshProUGUI HPText, APText, NameText, TurnAnnouncement;
   public PopUp PopUpText;
   public TextMeshProUGUI TurnCount;
   public Button EndTurnButton;
   private ICharacter Selected;
   
   public Card[] Buttons;
   public ResistanceCard[] ResistanceCards;
   public List<PopUp> PopUpList;

private int what;
   private void Start()
   {
      CreatePopList();
     // her();
   }

   public void UpdateTurnText(Turn.Turns act)
   {
       StartCoroutine(ShowTurnAnnouncement(act));
      TurnCount.color = act == Turn.Turns.E ? Color.red : Color.green;
      EndTurnButton.gameObject.SetActive(act == Turn.Turns.P); 
   }

   IEnumerator ShowTurnAnnouncement(Turn.Turns act)
   {
      TurnAnnouncement.gameObject.SetActive(true);
      TurnAnnouncement.text = act == Turn.Turns.E ? "Enemy Turn" : "Player Turn";
      yield return new WaitForSeconds(1);
      TurnAnnouncement.gameObject.SetActive(false);
   }
   public void ShowTurnCount(int turns) => TurnCount.text = turns.ToString();
   
   public void ShowPopUp(ElementalResistance.Resistance result, Vector3 pos, int damage) => PopUpList[0].ShowPopUp(result,pos,damage);
   
   public void Reset() => SceneManager.LoadScene(0);
   
   public void ClickWeaponButton(int i) => Selected.PrepareWeapon(i);

   public void her()
   {
      what++;
   }
   public void ShowInfo(ICharacter obj)
   {
      Selected = obj;
      HPText.text = obj.GetStats().side == Character.Fraction.Enemy
         ? obj.GetStats().Health.healthStatus.ToString()
         : obj.GetStats().Health.HealthPoints.ToString();
    
      APText.text = obj.GetStats().side == Character.Fraction.Enemy 
         ? obj.GetStats().AP.ToString()
         : obj.GetStats().AP.ToString();
      NameText.text = obj.GetStats().Name;
      ShowButtons();
      ShowResistance(obj.GetResistanse);
   }

   void ShowResistance(IResistance obj)
   {
      for (int i = 0; i < 5; ++i)
         ResistanceCards[i].CreateButton(obj.GetResistance(i));
   }
   public void ShowButtons()
   {
      for (int i = 0; i < 2; i++)
         Buttons[i].CreateButton(Selected,i);
   }

   void CreatePopList()
   {
      for (int i = 0; i < 35; i++)
      {
         PopUpList.Add(Instantiate(PopUpText,transform.position,quaternion.identity,transform.GetChild(0)));
      }
   }

}