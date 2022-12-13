using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ExperienceButton : MonoBehaviour
{
    Button button;
    private TextMeshProUGUI ButtonText;
    private int _index;
    private Attributes _attributes;
    private UIExperience ui;
    private Image _image;
    private string points, name;
    private void Start()
    {
        button = GetComponent<Button>();
        ButtonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _image = GetComponent<Image>();
    }

    public void SetButton(Character character, int index, UIExperience uiExperience)
    {
        ui = uiExperience;
        _index = index;
        _attributes = character?.Attributes;
       button.onClick.AddListener(Click);
       UpdateText();
       Activate(false);
    }

    public void UpdateText()
    {
        points = _attributes == null ? "" : _attributes.AttributesList[_index].max.ToString();
        name = _attributes == null ? "" : _attributes.AttributesList[_index].Name;
        ButtonText.text = $"{name}\n{points}";
    }

    public void Click()
    {
        _attributes.LevelUp(_index);
        UpdateText();
        ui.ActivateButtons(false);
    }

  public  void Activate(bool show)
    {
        _image.sprite = Resources.Load<Sprite>("Sprites/UI/CharacterButton/" + show);
        button.enabled = show;
    }
    
}
