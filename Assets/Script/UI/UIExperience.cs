using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UIExperience
{
    public ExperienceButton[] Buttons;
    public Image ExpBar;
    
    public TextMeshProUGUI  exp;
    private Character _character;

   
    void SetBar(Experience experience)
    {
        ExpBar.fillAmount = (float)experience.points/experience.pointsGoal;
        exp.text = $"{experience.points.ToString()}/{experience.pointsGoal.ToString()}";
    }

    void ClearBar()
    {
        ExpBar.fillAmount =0;
        exp.text = "";
    }

    public void Show(Character character)
    {
        if (character != null)
        {
            _character = character;
            SetBar(_character.Experience);
        }
        else
        {
            ClearButtons();
            ClearBar();
        }
    }

    public void ClearButtons()
    {
        foreach (var button in Buttons) 
            button.UpdateText();
    }
    public void SetButtons(Character character)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetButton(character,i,this);
        }
    }

    public void ActivateButtons(bool show)
    {
        foreach (var button in Buttons) 
            button.Activate(show);
    }

   

    
}
