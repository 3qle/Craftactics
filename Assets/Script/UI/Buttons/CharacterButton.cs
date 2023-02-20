using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{  
    private Character _character;
    private Button characterButton;
    
    public static Action<Character> CharacterButtonSelected;
    public Image CharacterImage;
    public Image HPbar, SPbar;
   
    public void Initialize( Character character)
    { 
        _character = character;
        characterButton = GetComponent<Button>(); 
        characterButton.onClick.AddListener(() => CharacterButtonSelected.Invoke(_character));
        CharacterImage.enabled = true;
        CharacterImage.sprite = _character.Face;
    }

    
    public void Show()
    {
        CharacterImage.sprite = _character.Faces[_character.Attributes.Get(Trait.Health).IsMore(0)?_character.Attributes.isWounded?1:2:0];
        HPbar.color = SPbar.color =  CharacterImage.color =Color.HSVToRGB(0,0,_character.Selected?1:0.5f);
        HPbar.fillAmount = _character.Attributes.Get(Trait.Health).current / _character.Attributes.Get(Trait.Health).startPoint;
        SPbar.fillAmount =_character.Attributes.Get(Trait.Stamina).current / _character.Attributes.Get(Trait.Stamina).startPoint;
    }
}
