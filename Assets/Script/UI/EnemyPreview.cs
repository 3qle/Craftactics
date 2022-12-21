  using System;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
[Serializable]
  public class EnemyPreview
    {
      public GameObject  PreviewContainer;
      private List<Button> buttons = new List<Button>();
      private Spawner spawner;


      public void Initialize(Spawner starter)
      { 
        spawner = starter; 
        LoadEnemyButtons();
        
      }
      public void LoadEnemyButtons()
      {
        for (int i = 0; i < PreviewContainer.transform.childCount; i++)
         buttons.Add(PreviewContainer.transform.GetChild(i).GetComponent<Button>());
      }

      public void ShowNextEnemy()
      {
        foreach (var button in  buttons)
          button.gameObject.SetActive(false);
        for (int i = 0; i < spawner.pool.EnemiesList.Count; i++)
        {
          buttons[i].gameObject.SetActive(true);
          var enemy = spawner.pool.EnemiesList[i];
          buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = enemy.Icon.sprite;
          buttons[i].onClick.AddListener(() => SelectEnemyPreview(enemy));
        }
      }

      void SelectEnemyPreview(Character enemy)
      {
        spawner.controller.SelectByMouse(enemy);
      }  
    }
