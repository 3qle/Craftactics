using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
  public class ResistanceCard : MonoBehaviour
    {
        public Image CardShell;
        public Image Image;
        public TextMeshProUGUI Name, Cost, Dam;
        private int ResitanceType;

        public Sprite[] Sprites;

        public void CreateButton( ElementalResistance.Resistance type)
        {
            ResitanceType = type switch
            {
                ElementalResistance.Resistance.Absorb => 3,
                ElementalResistance.Resistance.Weak => 0,
                ElementalResistance.Resistance.Block => 2,
                ElementalResistance.Resistance.Neutral => 1,

            };
            Image.sprite = Sprites[ResitanceType];
            
        }
        
    }
