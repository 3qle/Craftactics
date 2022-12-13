using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;

[System.Serializable]
public class UIResistance
{
    public GameObject ResistanceContainer;
    public ResistanceButton[] ResistanceCards;
    
        public void Show(Character character)
        {
            if (character != null)
            {
                ResistanceCards[0].CreateButton(character.Resistance._resistances[Element.Physic]);
                ResistanceCards[2].CreateButton(character.Resistance._resistances[Element.Water]);
                ResistanceCards[1].CreateButton(character.Resistance._resistances[Element.Fire]);
                ResistanceCards[3].CreateButton(character.Resistance._resistances[Element.Electric]);
                ResistanceCards[4].CreateButton(character.Resistance._resistances[Element.Wave]);
                ResistanceCards[5].CreateButton(character.Resistance._resistances[Element.Mental]);
                ResistanceCards[6].CreateButton(character.Resistance._resistances[Element.Holy]);
                ResistanceCards[7].CreateButton(character.Resistance._resistances[Element.Ill]);
            }
            else
             foreach (var card in ResistanceCards) 
                    card.ClearButton();
        }

        public void ShowResistanceContainer(bool show) =>
            ResistanceContainer.gameObject.transform.localScale =
                show ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);

}
