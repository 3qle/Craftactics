using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;

[System.Serializable]
public class UIResistance
{
    public Transform Container;
    private List<ResistanceButton> Resistances = new List<ResistanceButton>();
    
        public void Show(Character character)
        {
            if (character != null)
            {
                Resistances[0].CreateButton(character.Resistance._resistances[Element.Physic]);
                Resistances[2].CreateButton(character.Resistance._resistances[Element.Water]);
                Resistances[1].CreateButton(character.Resistance._resistances[Element.Fire]);
                Resistances[3].CreateButton(character.Resistance._resistances[Element.Electric]);
                Resistances[4].CreateButton(character.Resistance._resistances[Element.Wave]);
                Resistances[5].CreateButton(character.Resistance._resistances[Element.Mental]);
                Resistances[6].CreateButton(character.Resistance._resistances[Element.Holy]);
                Resistances[7].CreateButton(character.Resistance._resistances[Element.Ill]);
            }
            else
             foreach (var card in Resistances) 
                    card.ClearButton();
        }

        public void ShowResistanceContainer(bool show) =>
            Container.gameObject.transform.localScale =
                show ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);

        public void Initialize()
        {
            for (int i = 0; i < Container.childCount; i++) 
                Resistances.Add(Container.GetChild(i).GetComponent<ResistanceButton>());
        }

}
