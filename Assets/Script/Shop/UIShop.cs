 

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIShop
{
    public GameObject ShopContainer;
    public TextMeshProUGUI CoinsText;
    public Button StartNewRound, BuyButton,BuyCharacterButton, ShopButton;
    public List<CategoryButton> CategoryButtons = new List<CategoryButton>();

    public GameObject ItemContainer, CategoryContainer;
    public List<ShopButton> ItemButtons;
    public CategoryButton SelectedCategory;
    public Character SelectedCharacter;
    public CharacterButton SelectedCharacterButton;
    private Item SelectedItem;
   public Console[] Consoles;
   public Button[] EnemyButtons;

  
   private Pool _pool;
   private Spawner _spawner;
   [HideInInspector]public Shop _shop;
   public void Initialize(Spawner spawner,Shop shop )
   {
       _spawner = spawner;
       _shop = shop;
       _pool = spawner.pool;
       CreateItemContainer();
        CreateItemCategories();
        BuyButton.onClick.AddListener(BuyItem);
        BuyCharacterButton.onClick.AddListener(BuyCharacter);
        ShopButton.onClick.AddListener(() => ShowShop());
        ShowNextEnemy();
   }

   public void Show()
   {
       
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
        Consoles[0].Clear();
        SelectedCategory?.OpenCategory(false);
        SelectedCategory = button;
    }

    public void SelectCharacter(Character character,CharacterButton button)
    {
        if (_shop.inShop)
        {
            Consoles[0].Clear();
            SelectedCharacterButton = button;
            SelectedCharacter = character;
            SelectCategory(SelectedCategory);
            EnableValidCategories();
        }
    }

    void EnableValidCategories()
    {
        foreach (var button in CategoryButtons) 
            button.Disable();
        foreach(var butn in CategoryButtons)
            butn.EnableCategory(SelectedCharacter);
    }
    public void ShowConsole(Item  item)
    {
        SelectedItem = item;
        Consoles[0].ShowInfo(item);
    }
    
    public void BuyItem()
    {
        if (_shop.Wallet.IsEnough(SelectedItem.ShopCost))
        {
            _shop.Wallet.Spend(SelectedItem.ShopCost);
           UpdateCoins();
            SelectedCharacter.Bag.AddItem(SelectedItem);
            _spawner.ui.UIItem.UpdateButtons(SelectedCharacter);
        }
    }
    public void BuyCharacter()
    {
        if (_shop.Wallet.IsEnough(SelectedCharacter.Cost))
        {
            _shop.Wallet.Spend(SelectedCharacter.Cost);
            UpdateCoins();
            SelectedCharacter.Enable(SelectedCharacterButton);
            SelectCharacter(SelectedCharacter,SelectedCharacterButton);
        }
    }

    public void UpdateCoins()
    {
        CoinsText.text = _shop.Wallet.Coins.ToString();
    }
    public void ShowShop()
    {
        ShowNextEnemy();
        CoinsText.text = _shop.Wallet.Coins.ToString();
        ShopContainer.gameObject.transform.localScale =
            ShopContainer.gameObject.transform.localScale == new Vector3(0, 0, 0) ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);
        _shop.inShop = ShopContainer.gameObject.transform.localScale != new Vector3(0, 0, 0);
    }

    public void ShowNextEnemy()
    {
        foreach (var button in EnemyButtons)
            button.gameObject.SetActive(false);
        for (int i = 0; i < _pool.EnemiesList.Count; i++)
        {
            EnemyButtons[i].gameObject.SetActive(true);
            EnemyButtons[i].image.sprite = _pool.EnemiesList[i]._sprite.sprite;
        }
    }

}
