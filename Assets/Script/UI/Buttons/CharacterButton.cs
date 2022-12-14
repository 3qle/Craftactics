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
  
   
    public void Itinialize( Controller controller)
    {
        _controller = controller;
    }

    public void AttachCharacter(Character character)
    {
        _character = character;
    }
 
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


    public void HighLightButton(bool selected) => ButtonImage.sprite = Resources.Load<Sprite>("Sprites/UI/CharacterButton/" + selected);
    
    public void Select(int i)
    {
        _controller.SelectFromUi(_character,this);
        //HighLightButton(true);
    }
    
}
