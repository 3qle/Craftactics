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
            Fill.color = resistAmount > 0 ? new Color(0.12f,0.7f,0.24f) : new Color(.7f,.12f,.13f);
            resistAmount = resistAmount < 0 ? -resistAmount : resistAmount;
            Fill.transform.localScale = new Vector3(resistAmount/100, transform.localScale.y,transform.localScale.z);
        }

        public void ClearButton()
        {
            Fill.fillAmount = 0;
        }
    }
