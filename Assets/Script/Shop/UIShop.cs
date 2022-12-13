 

using System;
using System.Collections.Generic;
using Script.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIShop
{
    public GameObject ShopContainer;
    public TextMeshProUGUI CoinsText;
    public Button StartNewRound, BuyButton, ShopButton;
    public List<CategoryButton> CategoryButtons = new List<CategoryButton>();

    public GameObject ItemContainer, CategoryContainer;
    public List<ItemButton> ItemButtons;
    public CategoryButton SelectedCategory;
    public Character SelectedCharacter;
    public CharacterButton SelectedCharacterButton;
    private Item SelectedItem;
   public Console[] Consoles;

   public List<CharacterButton> CharacterButtons;
   private Pool _pool;
   public void Initialize(Pool pool)
   {
       _pool = pool;
        CreateItemContainer();
        CreateItemCategories();
        ShowHeroesButton();
        BuyButton.onClick.AddListener(Buy);
        ShopButton.onClick.AddListener(() => ShowShop());
   }

    public void CreateItemContainer()
    {
        for (int i = 0; i < ItemContainer.transform.childCount; i++)
            ItemButtons.Add(ItemContainer.transform.GetChild(i).GetComponent<ItemButton>());
    }

    public void CreateItemCategories()
    {
        for (int i = 0; i < CategoryContainer.transform.childCount; i++)
        {
            CategoryButtons.Add(CategoryContainer.transform.GetChild(i).GetComponent<CategoryButton>());
            CategoryButtons[i].Initialize(ItemButtons, this,i);
        }
    }

    public void SetSelectedCategory(CategoryButton button)
    {
        SelectedCategory?.ShowCategory(false);
        SelectedCategory = button;
    }

    public void SetSelectedCharacter(Character character,CharacterButton button)
    {
        SelectedCharacterButton?.HighLightButton(false);
        SelectedCharacterButton = button;
        SelectedCharacter = character;
        foreach(var butn in CategoryButtons)
            butn.EnableButton(SelectedCharacter);
    }
    public void ShowConsole(Item  item)
    {
        SelectedItem = item;
        Consoles[0].ShowInfo(item);
    }

    public void ShowHeroesButton()
    {
        for (int i = 0; i < _pool.HeroesList.Count; i++)
        {
            CharacterButtons[i].ShowInShop(_pool.HeroesList[i], this);
        }
    }

    public void Buy()
    {
        SelectedCharacter.Bag.AddItem(SelectedItem);
        SelectedCharacterButton.LoadItems();
    }

    public void ShowShop()
    {
        ShopContainer.gameObject.transform.localScale =
            ShopContainer.gameObject.transform.localScale == new Vector3(0, 0, 0) ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);
    }

}
