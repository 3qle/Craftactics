using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    [Serializable]
    public  class UIBaseInfo
    {
        public TextMeshProUGUI HPText, APText, NameText;
        public Image HpBar, SpBar;
      
        public void ShowInfo(global::Character character)
        {
           HPText.text =  character.Attributes.health.HP.ToString();
            APText.text =character.Attributes.stamina.SP.ToString();
            NameText.text = character.Name;
            HpBar.fillAmount = (float)character.Attributes.health.HP / character.Attributes.Health;
            SpBar.fillAmount = (float)character.Attributes.stamina.SP / character.Attributes.Stamina;
        }
    }
}