using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
  public class ResistanceButton : MonoBehaviour
    {
        public Image Image, Fill;
        private int ResitanceType;

        public Sprite[] Sprites;
        private float resistAmount;
        
        public void CreateButton( float resistAmount)
        {
            Fill.sprite = Sprites[resistAmount > 0 ? 2 : 0];
            resistAmount = resistAmount < 0 ? -resistAmount : resistAmount;
            Fill.fillAmount = resistAmount/100;
        }

        public void ClearButton()
        {
            Fill.fillAmount = 0;
        }
    }
