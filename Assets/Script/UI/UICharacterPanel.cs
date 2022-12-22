using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
    public  class UICharacterPanel
    {
        private List<ResistanceButton> Resistances = new List<ResistanceButton>();
        private List<ExperienceButton> Stats = new List<ExperienceButton>();
        private List<CharacterButton> Characters = new List<CharacterButton>();
        
        public TextMeshProUGUI HealthText, StaminaText, NameText;
        public Image HealthBar, StaminaBar;
        
        [Header("Containers")]
        public Transform ResistanceContainer, StatsContainer, CharacterButtonsContainer;
        
        public void Initialize()
        {
            for (int i = 0; i < ResistanceContainer.childCount; i++) 
                Resistances.Add(ResistanceContainer.GetChild(i).GetComponent<ResistanceButton>());
         
            for (int i = 0; i < StatsContainer.childCount-2 ; i++) 
                Stats.Add(StatsContainer.GetChild(i).GetComponent<ExperienceButton>());
        }

        public void AddCharacterToButton(Character character, int i)
        {
            Characters.Add(CharacterButtonsContainer.GetChild(i).GetComponent<CharacterButton>());
            Characters[i].Initialize(character);
        }
        
        public void Show(Character character)
        {
            ShowStamina( character.Attributes.stamina); 
            ShowHealth(character.Attributes.health); 
            ShowResistance(character.Resistance.ResistanceClasses); 
            ShowStats(character);
            ShowCharacters();
            NameText.text = character.Name;
        }

        void ShowStamina(Attribute stamina)
        {
            StaminaText.text =  stamina.current.ToString();
            StaminaBar.fillAmount = stamina.current / stamina.max;
        }
        void ShowHealth(Attribute health)
        {
            HealthText.text = health.current.ToString();
            HealthBar.fillAmount = health.current / health.max;
        }
        public void ShowResistance(ResistanceClass[] list)
        {
            for (int i = 0; i < list.Length; i++) 
                    Resistances[i].SetResistanceAmount(list[i].amount);
        }

        public void ShowStats(Character character)
        {
            for (int i = 0; i < Stats.Count; i++) 
                Stats[i].UpdateText(character?character.Attributes.AttributesList[i]: null);
        }
        
        public void ShowCharacters()
        {
            foreach (var button in Characters) 
                button.Show();
        }
    }
