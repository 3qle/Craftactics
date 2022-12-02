using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;

[System.Serializable]
public class UIResistance
{
    public ResistanceCard[] ResistanceCards;
       
        
        public void ShowResistance(Dictionary<Element,ResistanceType> resistanceTypes)
        {
            ResistanceCards[0].CreateButton(resistanceTypes[Element.Physic]);
            ResistanceCards[2].CreateButton(resistanceTypes[Element.Water]);
            ResistanceCards[1].CreateButton(resistanceTypes[Element.Fire]);
            ResistanceCards[3].CreateButton(resistanceTypes[Element.Electric]);
            ResistanceCards[4].CreateButton(resistanceTypes[Element.Wave]);
            ResistanceCards[5].CreateButton(resistanceTypes[Element.Mental]);
            ResistanceCards[6].CreateButton(resistanceTypes[Element.Holy]);
            ResistanceCards[7].CreateButton(resistanceTypes[Element.Ill]);
        }
}
