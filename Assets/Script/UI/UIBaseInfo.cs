using System;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    [Serializable]
    public  class UIBaseInfo
    {
        public TextMeshProUGUI HPText, APText, NameText;

      
        public void ShowInfo(int hp, int ap, string name)
        {
            HPText.text = hp.ToString();
            APText.text = ap.ToString();
            NameText.text = name;
        }
    }
}