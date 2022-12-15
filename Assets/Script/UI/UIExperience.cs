using System;
using System.Collections;
using System.Collections.Generic;
using Script.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UIExperience
{
    public Transform Container; 
    public TextMeshProUGUI  exp;
    public Image ExpBar;
    private Turn _turn;
    private List<ExperienceButton> Buttons = new List<ExperienceButton>();
    private Character _character;

    public void Initialize(Spawner starter)
    {
        _turn = starter.turn;
        for (int i = 0; i < Container.childCount ; i++) 
            Buttons.Add(Container.GetChild(i).GetComponent<ExperienceButton>());
        
    }
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
        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].SetButton(character,i,this);
        }
        ActivateButtons(character?.Experience.freePoint > 0 && _turn.Act == TurnState.Shop);
    }

    public void ActivateButtons(bool show)
    {
        foreach (var button in Buttons) 
            button.Activate(show);
    }

   

    
}
