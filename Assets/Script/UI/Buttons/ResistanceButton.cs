using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
  public class ResistanceButton : MonoBehaviour
    {
        private  Image  ResisatanceAmount;

        private void Awake() => ResisatanceAmount = GetComponent<Image>();
        public void ClearResistanceAmount() => ResisatanceAmount.fillAmount = 0;
        public void SetResistanceAmount(float resistAmount)
        {
            ResisatanceAmount.color = resistAmount > 0 ? new Color(0.12f,0.7f,0.24f) : new Color(.7f,.12f,.13f);
            resistAmount = resistAmount < 0 ? -resistAmount : resistAmount;
            ResisatanceAmount.fillAmount = resistAmount / 100;
        }
    }
