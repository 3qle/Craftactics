using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ExperienceButton : MonoBehaviour
{
    public Sprite[] Sprites;
    Button button;
    private TextMeshProUGUI ButtonText;
    private int _index;
    private Attributes _attributes;
    private UIExperience ui;
    private Image _image;
    private string points, name;
    private void Awake()
    {
        ButtonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _image = GetComponent<Image>();
       
    }

    public void SetButton(Character character, int index, UIExperience uiExperience)
    {
        ui = uiExperience;
        _index = index;
        _attributes = character?.Attributes; UpdateText();
        Activate();
   
    }

    public void UpdateText()
    {
        var stat = _attributes?.AttributesList[_index];
        if (stat != null)
        {
            ButtonText.color = stat.current > stat.max
                ? Color.green
                : stat.current == stat.max
                    ? Color.white
                    : Color.red;
            
            points = _attributes == null ? "" : _attributes.AttributesList[_index].current.ToString();
            name = _attributes == null ? "" : _attributes.AttributesList[_index].Name;
            ButtonText.text = points;
        }
        
       
         
    }

   

  public  void Activate()
    {
        if(_attributes != null)
        _image.sprite = Resources.Load<Sprite>("Sprites/Stats/" + name);
    }
    
}
