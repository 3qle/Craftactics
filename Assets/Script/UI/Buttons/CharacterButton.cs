using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterButton : MonoBehaviour
{
    public Image CharacterImage, ButtonImage;
    public Image HPbar, SPbar;
   
    private Character _character;
    private Controller _controller;
    private Button characterButton;

    public void Itinialize(Controller controller)
    {
      _controller = controller;
      characterButton = GetComponent<Button>();
      characterButton.onClick.AddListener(Select);
    }
       
    
    public void AttachCharacter(Character character) 
        => _character = character;

    public void HighLightButton(bool selected, Character character)
    {
        if( character == null ||character.side == Character.Fraction.Hero ) 
            ButtonImage.sprite = Resources.Load<Sprite>("Sprites/UI/CharacterButton/" + selected);
    }
    
       
    
    public void Select()
        => _controller.SelectCharacterButton(_character,this);
    
    public void Show()
    {
        if (_character != null)
        {
            CharacterImage.enabled = true;
            CharacterImage.sprite = _character._sprite.sprite;
            HPbar.fillAmount =(float)_character.Attributes.health.current / _character.Attributes.health.max;
            SPbar.fillAmount =(float)_character.Attributes.stamina.current / _character.Attributes.stamina.max;
        } 
    }
}
