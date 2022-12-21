using System;
using System.Collections;
using System.Collections.Generic;
using Script.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UIExperience
{
    public Transform Container;
    private Turn _turn;
    private List<ExperienceButton> Buttons = new List<ExperienceButton>();
    private Character _character;

    public UIExperience(Spawner starter, Transform container)
    {
        _turn = starter.turn;
        Container = container;
        for (int i = 0; i < Container.childCount -2 ; i++) 
            Buttons.Add(Container.GetChild(i).GetComponent<ExperienceButton>());
    }
    
    public void Show(Character character)
    {
        if (character != null) 
            _character = character;
        else
          ClearButtons();
    }

    public void ClearButtons()
    {
        foreach (var button in Buttons) 
            button.UpdateText();
    }
    public void SetButtons(Character character)
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].SetButton(character,i,this);
        }
    }

    
    
}
