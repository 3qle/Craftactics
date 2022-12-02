using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UICharacter
{
    public CharacterButton[] Button;
    private Console _console;

    public void Initialize(List<Character> list,Controller controller, Console console)
    {
        _console = console;
        for (int j = 0; j < list.Count; j++)
          Button[j].Itinialize(list[j],controller);
    }

    public void Show()
    {
        foreach (var button in Button)
        {
            button.Show();
        }
    }

    public void ShowHighLight(int i, bool _selected)
    {
        Button[i].HighLightButton(_selected);
    }
}
