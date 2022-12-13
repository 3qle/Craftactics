using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public Image CharacterImage, ButtonImage;
    public Image HPbar, SPbar;
    public Bag bag;
    private Character _character;
    private Controller _controller;
    private UIShop _uiShop;
    private List<ItemButton> ItemButtons = new List<ItemButton>();
    public void Itinialize(Character character, Controller controller)
    {
        _controller = controller;
        _character = character;
        bag = character.Bag;
        CharacterImage.sprite = character._sprite.sprite;
        Show();
    }

    public void ShowInShop(Character character,UIShop uiShop)
    {
        _uiShop = uiShop;
        _character = character;
        bag = character.Bag;
        CharacterImage.sprite = character._sprite.sprite;
        LoadItems();
    }

    public void Show()
    {
        HPbar.fillAmount =(float)_character.Attributes.health.current / _character.Attributes.health.max;
        SPbar.fillAmount =(float)_character.Attributes.stamina.current / _character.Attributes.stamina.max;
    }

   public void LoadItems()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            ItemButtons.Add(transform.GetChild(0).GetChild(i).GetComponent<ItemButton>());
        }
        for (int i = 0; i < bag.Items.Count; i++)
        {
            ItemButtons[i].UpdateButtonInfo(_character.Bag.Items[i],_character);
        }
    }

    public void HighLightButton(bool selected) => ButtonImage.sprite = Resources.Load<Sprite>("Sprites/UI/CharacterButton/" + selected);
    
    public void Select(int i) => _controller.SelectFromUi(i);
    public void SelectFromShop()
    {
        HighLightButton(true);
        _uiShop.SetSelectedCharacter(_character,this);
    }
}
