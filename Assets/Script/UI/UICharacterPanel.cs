using System;
using System.Collections.Generic;
using Script.UI.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
    public  class UICharacterPanel
    {
        private List<ResistanceButton> Resistances = new List<ResistanceButton>();
        private List<ExperienceButton> Stats = new List<ExperienceButton>();
        private List<CharacterButton> Characters = new List<CharacterButton>();
        private List<StatusButton> Statuses = new List<StatusButton>();
        public TextMeshProUGUI HealthText, StaminaText, NameText;
        public Image HealthBar, StaminaBar, Face;
        
        [Header("Containers")]
        public Transform ResistanceContainer, StatsContainer, CharacterButtonsContainer, StatusContainer;
        
        public void Initialize()
        {
            for (int i = 0; i < ResistanceContainer.childCount; i++) 
                Resistances.Add(ResistanceContainer.GetChild(i).GetComponent<ResistanceButton>());
         
            for (int i = 0; i < StatsContainer.childCount-2 ; i++) 
                Stats.Add(StatsContainer.GetChild(i).GetComponent<ExperienceButton>());
            
            for (int i = 0; i < StatusContainer.childCount ; i++) 
                Statuses.Add(StatusContainer.GetChild(i).GetComponent<StatusButton>());
            
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
            ShowStatus(character);
            NameText.text = character.Name;
            Face.sprite = character.Face;
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
                    Resistances[i].SetResistanceAmount(list[i].current);
        }

        public void ShowStats(Character character)
        {
            for (int i = 0; i < Stats.Count; i++) 
                Stats[i].UpdateText(character?character.Attributes.AttributesList[i]: null);
        }

        public void ShowStatus(Character character)
        {
            for (int i = 0; i < character.Attributes.StatusList.Count; i++) 
                Statuses[i].ShowStatus(character.Attributes.StatusList[i].Icon,character.Attributes.StatusList[i]._duration);
            for (int i = character.Attributes.StatusList.Count; i < Statuses.Count; i++)
                Statuses[i].Hide();
        }
        
        public void ShowCharacters()
        {
            foreach (var button in Characters) 
                button.Show();
        }
    }
