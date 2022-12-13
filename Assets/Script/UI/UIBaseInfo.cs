﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    [Serializable]
    public  class UIBaseInfo
    {
        public GameObject StatsContainer;
        public TextMeshProUGUI HPText, APText, NameText, LevelText;
        public Image HpBar, SpBar;
       
        public void Show(global::Character character)
        {
            HPText.text = character == null? "": character.Attributes.health.current.ToString();
            APText.text =character == null? "": character.Attributes.stamina.current.ToString();
            NameText.text = character == null? "": character.Name;
            LevelText.text = character == null? "": character.Experience.level.ToString();
            
            HpBar.fillAmount =character == null? 0: (float)character.Attributes.health.current / character.Attributes.health.max;
            SpBar.fillAmount = character == null? 0: (float)character.Attributes.stamina.current / character.Attributes.stamina.max;
        }
        
        public void ShowStatsContainer(bool show) => 
            StatsContainer.gameObject.transform.localScale =
            show ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);
    }
}