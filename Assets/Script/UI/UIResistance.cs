using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;


public class UIResistance
{
    public Transform Container;
    private List<ResistanceButton> Resistances = new List<ResistanceButton>();
    
        public void Show(Character character)
        {
            if (character != null)
            {
                for (int i = 0; i < character.Resistance.ResistanceClasses.Length; i++)
                {
                    Resistances[i].CreateButton(character.Resistance.ResistanceClasses[i].amount);
                }
            }
            else
             foreach (var card in Resistances) 
                    card.ClearButton();
        }

        public void ShowResistanceContainer(bool show) =>
            Container.gameObject.transform.localScale =
                show ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);

        public UIResistance(Transform container)
        {
            Container = container;
            for (int i = 0; i < Container.childCount; i++) 
                Resistances.Add(Container.GetChild(i).GetComponent<ResistanceButton>());
        }

}
