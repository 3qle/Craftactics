using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;

public class UIResistance : MonoBehaviour
{
    public ResistanceCard[] ResistanceCards;
        void Start() => ElementalResistance.ShowOnUI+= ShowResistance;
        
        void ShowResistance(Dictionary<Element,ResistanceType> resistanceTypes)
        {
            ResistanceCards[0].CreateButton(resistanceTypes[Element.Physic]);
            ResistanceCards[1].CreateButton(resistanceTypes[Element.Fire]);
            ResistanceCards[2].CreateButton(resistanceTypes[Element.Water]);
            ResistanceCards[3].CreateButton(resistanceTypes[Element.Electric]);
            ResistanceCards[4].CreateButton(resistanceTypes[Element.Wave]);
        }
}
