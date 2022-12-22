using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{  
    private Character _character;
    private Button characterButton;
    
    public static Action<Character> CharacterButtonSelected;
    public Image CharacterImage, ButtonImage;
    public Image HPbar, SPbar;
    [SerializeField] private Sprite[] _images;
   
    
    public void Initialize( Character character)
    { 
        _character = character;
        characterButton = GetComponent<Button>(); 
        characterButton.onClick.AddListener(Select);
        CharacterImage.enabled = true;
        CharacterImage.sprite = _character.Icon.sprite;
    }


    public void Select()
        => CharacterButtonSelected.Invoke(_character);
    
    public void Show()
    {
        ButtonImage.sprite = _images[_character.Selected? 1:0];
        HPbar.fillAmount = _character.Attributes.health.current / _character.Attributes.health.max;
        SPbar.fillAmount =_character.Attributes.stamina.current / _character.Attributes.stamina.max;
    }
}
