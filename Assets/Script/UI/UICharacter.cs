using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UICharacter
{
    public CharacterButton[] Button;
   
    private Character _character;
   
    public void Initialize(List<Character> list, Controller controller)
    {
       
      for (int j = 0; j < list.Count; j++)
          Button[j].Itinialize(list[j],controller);
    }

    public void Show()
    {
        foreach (var button in Button) 
            button.Show();
    }

    public void HighLightButton(Character character)
    {
        if (_character != null) 
            Button[_character.Index].HighLightButton(false);
        
        _character =  character;
        
        if (_character != null && character.side == Character.Fraction.Hero)
            Button[_character.Index].HighLightButton(true);
         
    }
}
