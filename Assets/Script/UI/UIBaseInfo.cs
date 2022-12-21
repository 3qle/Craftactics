using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

 [Serializable]
    public  class UIBaseInfo
    {
        public TextMeshProUGUI HPText, APText, NameText, LevelText;
        public Image HpBar, SpBar;
       
        public void Show(Character character)
        {
            HPText.text = character == null? "": character.Attributes.health.current.ToString();
            APText.text =character == null? "": character.Attributes.stamina.current.ToString();
            NameText.text = character == null? "": character.Name;
          
            
            HpBar.fillAmount =character == null? 0: (float)character.Attributes.health.current / character.Attributes.health.max;
            SpBar.fillAmount = character == null? 0: (float)character.Attributes.stamina.current / character.Attributes.stamina.max;
        }
        
       
    }
