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
        characterButton.onClick.AddListener(() => CharacterButtonSelected.Invoke(_character));
        CharacterImage.enabled = true;
        CharacterImage.sprite = _character.Face;
    }

    
    public void Show()
    {
        HPbar.color = SPbar.color =  CharacterImage.color =Color.HSVToRGB(0,0,_character.Selected?1:0.5f);
        HPbar.fillAmount = _character.Attributes.health.current / _character.Attributes.health.max;
        SPbar.fillAmount =_character.Attributes.stamina.current / _character.Attributes.stamina.max;
    }
}
