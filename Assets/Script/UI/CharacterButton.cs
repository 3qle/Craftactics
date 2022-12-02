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

    public void Itinialize(Character character, Controller controller)
    {
        _controller = controller;
        _character = character;
        CharacterImage.sprite = character._sprite.sprite;
        Show();
    }

    public void Show()
    {
        HPbar.fillAmount =(float)_character.Attributes.health.HP / _character.Attributes.Health;
        SPbar.fillAmount =(float)_character.Attributes.stamina.SP / _character.Attributes.Stamina;
    }

    public void HighLightButton(bool selected) => ButtonImage.sprite = Resources.Load<Sprite>("Sprites/UI/CharacterButton/" + selected);
    
    public void Select(int i) => _controller.SelectFromUi(i);
}
