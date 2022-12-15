using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UICharacter
{
    public Transform Container;
    List<CharacterButton> Buttons = new List<CharacterButton>();
    List<Character> characters = new List<Character>();

   
    public void Initialize(Controller controller)
    {
        for (int i = 0; i < Container.childCount; i++)
        {
            Buttons.Add(Container.GetChild(i).GetComponent<CharacterButton>());
            Buttons[i].Itinialize(controller);
        }
    }

    public void AddCharacterToButton(Character character, CharacterButton button)
    {
        characters.Add(character);
        Buttons[Buttons.IndexOf(button)].AttachCharacter(character);
    }

    public void Show()
    {
        foreach (var button in Buttons) 
            button.Show();
    }


}
