using System;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class UIBaseInfo: MonoBehaviour
    {
        public TextMeshProUGUI HPText, APText, NameText;

        private void Start()
        {
            global::Character.ShowBaseInfo += ShowInfo;
        }

        public void ShowInfo(int hp, int ap, string name)
        {
            HPText.text = hp.ToString();
            APText.text = ap.ToString();
            NameText.text = name;
        }
    }
}