using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Buttons
{
    public class StatusButton : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI _duration;

      
        public void ShowStatus(Sprite image, int duration)
        {
            Icon.sprite = image;
            Icon.enabled =true;
            _duration.text = duration > 0 ? duration.ToString() : "";
        }

        public void Hide()
        {
            Icon.enabled = false;
            _duration.text = "";
        }
       
    }
}