using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UICharacter
{
    public List<CharacterButton> Button;
    List<Character> characters = new List<Character>();
    private Character _character;
    private UIShop _shop;
   
    public void Initialize(Controller controller)
    {
        foreach (var button in Button)
         button.Itinialize(controller);
    }

    public void AddCharacterToButton(Character character, CharacterButton button)
    {
        characters.Add(character);
        Button[Button.IndexOf(button)].AttachCharacter(character);
    }

    public void Show()
    {
        foreach (var button in Button) 
            button.Show();
    }


}
