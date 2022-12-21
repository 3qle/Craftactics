using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class UICharacter
{
    Transform Container;
  public  List<CharacterButton> Buttons = new List<CharacterButton>();
   [HideInInspector] public List<Character> characters = new List<Character>();

   
    public  UICharacter(Controller controller , Transform container)
    {
        Container = container;
        for (int i = 0; i < Container.childCount; i++)
        {
            Buttons.Add(Container.GetChild(i).GetComponent<CharacterButton>());
            Buttons[i].Itinialize(controller);
        }
    }

    public void AddCharacterToButton(Character character)
    {
        if (!characters.Contains(character))
        {
            characters.Add(character);
            Buttons[characters.IndexOf(character)].AttachCharacter(character);
        }
        else 
        {
            Buttons[characters.IndexOf(character)].AttachCharacter(null); 
            characters.Remove(character); 
        }
       
    }

    public void Show()
    {
        foreach (var button in Buttons) 
            button.Show();
    }


}
