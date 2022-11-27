using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
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

        private void Start()
        {
        }

        
        public void CreateButton( ResistanceType type)
        {
            ResitanceType = type switch
            {
                ResistanceType.Absorb => 3,
                ResistanceType.Weak => 0,
                ResistanceType.Block => 2,
                ResistanceType.Neutral => 1,

            };
            Image.sprite = Sprites[ResitanceType];
            
        }

        void Show()
        {
            Debug.Log("show");
        }
        
    }
