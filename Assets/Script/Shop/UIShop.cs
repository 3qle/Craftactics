 

using System;
using System.Collections.Generic;
using Script.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UIShop
{
    private Console Console;
    private Pool _pool;
    private Spawner _spawner;
    [HideInInspector]public Shop _shop;
    private List<CategoryButton> CategoryButtons = new List<CategoryButton>();
    private List<ShopButton> ItemButtons = new List<ShopButton>();
    private List<Button> EnemyPreview = new List<Button>();

   
    public TextMeshProUGUI CoinsText;
    public Button ShopButton;
    public GameObject ItemContainer, CategoryContainer, PreviewContainer;
   
    private CategoryButton SelectedCategory;
    public Character SelectedCharacter;
    public CharacterButton SelectedCharacterButton;
    private Item SelectedItem;
    public ShopButton SelectedButton;
  
   

  
   public void Initialize(Spawner spawner,Shop shop )
   {
       Console = spawner.ui.Console;
       _spawner = spawner;
       _shop = shop;
       _pool = spawner.pool;
       CreateItemContainer(); 
       CreateItemCategories(); 
       LoadEnemyButtons();
       ShowNextEnemy();
       ShowNextEnemy(); 
       UpdateCoins();
       ShopButton.onClick.AddListener(() => shop.OpenShop(false));
   }
   
   
    public void CreateItemContainer()
    {
        for (int i = 0; i < ItemContainer.transform.childCount; i++)
            ItemButtons.Add(ItemContainer.transform.GetChild(i).GetComponent<ShopButton>());
    }

    public void CreateItemCategories()
    {
        for (int i = 0; i < CategoryContainer.transform.childCount; i++)
        {
            CategoryButtons.Add(CategoryContainer.transform.GetChild(i).GetComponent<CategoryButton>());
            CategoryButtons[i].Initialize(ItemButtons, this,i,_spawner);
        }
    }

    public void SelectCategory(CategoryButton button)
    {
        Console.Clear();
        SelectedCategory?.OpenCategory(false);
        SelectedCategory = button;
    }

    public void HideButtons()
    {
        foreach (var button in ItemButtons) 
            button.HighLightButton(false);
    }

    public void SelectCharacter(Character character,CharacterButton button)
    {
        if (_shop.inShop)
        {
            Console.Clear();
            SelectedCharacterButton = button;
            SelectedCharacter = character;
            SelectCategory(SelectedCategory);
            EnableValidCategories();
        }
    }

    void EnableValidCategories()
    {
        foreach (var button in CategoryButtons) 
            button.Enable(false);
        foreach(var butn in CategoryButtons)
            butn.EnableCategory(SelectedCharacter);
    }
    public void SelectItemInShop(ShopButton button)
    {
        SelectedButton = button;
        SelectedItem = button._item;
        Console.ShowInfo(button._item);
    }
    public void SelectCharacterInShop(ShopButton button)
    {
        SelectedButton = button;
        SelectedCharacter = button._character;
    }
    public void Buy()
    {
        if (SelectedCategory.Type != ItemType.Hero) BuyItem();
        else BuyCharacter(); 
        UpdateCoins();
    }
    public void BuyItem()
    {
        if (_shop.Wallet.IsEnough(SelectedItem.ShopCost))
        {
            _shop.Wallet.Spend(SelectedItem.ShopCost);
            SelectedCharacter.Bag.AddItem(SelectedItem);
            _spawner.ui.ItemUI.UpdateButtons(SelectedCharacter);
            SelectedButton.ClearButton();
           SelectedButton._item.Bought = true;
        }
    }
    public void BuyCharacter()
    {
        if (_shop.Wallet.IsEnough(SelectedCharacter.Cost))
        {
            _shop.Wallet.Spend(SelectedCharacter.Cost);
            SelectedCharacter.EnableHero(SelectedCharacterButton);
            SelectCharacter(SelectedCharacter,SelectedCharacterButton);
            SelectedButton.ClearButton();
            SelectedButton._character.Bought = true;
        }
    }

    public void UpdateCoins()
    {
        CoinsText.text = _shop.Wallet.GetCoins();
    }
    public void ShowShop()
    {
       
    }

    public void LoadEnemyButtons()
    {
        for (int i = 0; i < PreviewContainer.transform.childCount; i++)
         EnemyPreview.Add(PreviewContainer.transform.GetChild(i).GetComponent<Button>());
    }

    public void ShowNextEnemy()
    {
        foreach (var button in EnemyPreview)
            button.gameObject.SetActive(false);
        for (int i = 0; i < _pool.EnemiesList.Count; i++)
        {
            EnemyPreview[i].gameObject.SetActive(true);
            var enemy = _pool.EnemiesList[i];
            EnemyPreview[i].transform.GetChild(0).GetComponent<Image>().sprite = enemy._sprite.sprite;
            EnemyPreview[i].onClick.AddListener(() => SelectEnemyPreview(enemy));
        }
    }

    void SelectEnemyPreview(Character enemy)
    {
        _spawner.controller.SelectByMouse(enemy);
        SelectedCharacter = enemy;
        EnableValidCategories();
        SelectedCategory?.OpenCategory(false);
    }

}
