using System;
using System.Collections.Generic;
using Script.Enum;
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
        private int _maxDefence = 8, _maxStats = 5;
        [Header("Containers")]
        public Transform ResistanceContainer, StatsContainer, CharacterButtonsContainer, StatusContainer;
        
        public void Initialize()
        {
            for (int i = 0; i < ResistanceContainer.childCount; i++) 
                Resistances.Add(ResistanceContainer.GetChild(i).GetComponent<ResistanceButton>());
         
            for (int i = 0; i < StatsContainer.childCount ; i++) 
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
            ShowStamina( character.Attributes.Get(Trait.Stamina)); 
            ShowHealth(character.Attributes.Get(Trait.Health)); 
            ShowResistance(character); 
            ShowStats(character);
            ShowCharacters();
            ShowStatus(character);
            NameText.text = character.Name;
            Face.sprite = character.entityType == EntityType.Hero
                ? character.Faces[character.Attributes.Get(Trait.Health).IsMore(0)
                    ?character.Attributes.isWounded
                        ?1
                        :2
                    :0]
                : character.Face;
        }

        void ShowStamina(Attribute stamina)
        {
            StaminaText.text =  stamina.current.ToString();
            StaminaBar.fillAmount = stamina.current / stamina.startPoint;
        }
        void ShowHealth(Attribute health)
        {
            HealthText.text = health.current.ToString();
            HealthBar.fillAmount = health.current / health.startPoint;
        }
        public void ShowResistance(Character character)
        {
            for (int i = _maxStats; i < _maxDefence + _maxStats; i++)
                Resistances[i-_maxStats].SetResistanceAmount(character.Attributes.TraitList[i].current);
        }

        public void ShowStats(Character character)
        {
            for (int i = 0; i < _maxStats; i++) 
              Stats[i].UpdateText(character?character.Attributes.TraitList[i]: null);
        }

        public void ShowStatus(Character character)
        {
            for (int i = 0; i < character.Attributes.StatusList.Count; i++) 
                Statuses[i].ShowStatus(character.Attributes.StatusList[i].Icon,character.Attributes.StatusList[i].Duration);
            for (int i = character.Attributes.StatusList.Count; i < Statuses.Count; i++)
                Statuses[i].Hide();
        }
        
        public void ShowCharacters()
        {
            foreach (var button in Characters) 
                button.Show();
        }
    }
