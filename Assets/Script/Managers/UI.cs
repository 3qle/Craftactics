using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour, IUIble
{
   public TextMeshProUGUI  TurnAnnouncement;
   public PopUp PopUpText;
   public TextMeshProUGUI TurnCount;
   public Button EndTurnButton;
   private ICharacter Selected;
   
  
   
   public List<PopUp> PopUpList;


   private void Start()
   {
      CreatePopList();
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
 
   public void ShowPopUp(ResistanceType result, Vector3 pos, int damage) => PopUpList[0].ShowPopUp(result,pos,damage);
   
   public void Reset() => SceneManager.LoadScene(0);
   
  
   
  

   
  

   void CreatePopList()
   {
      for (int i = 0; i < 35; i++)
      {
         PopUpList.Add(Instantiate(PopUpText,transform.position,quaternion.identity,transform.GetChild(0)));
      }
   }

}