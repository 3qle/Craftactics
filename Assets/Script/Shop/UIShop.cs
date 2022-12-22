using System;
using System.Collections.Generic;
using Script.Enum;
using TMPro;
using UnityEngine;

[Serializable]
public class UIShop
{
    private Console Console;
    private Spawner _spawner; 
    private Shop _shop;
    private List<CategoryButton> CategoryButtons = new List<CategoryButton>();
    private List<ShopButton> ItemButtons = new List<ShopButton>();
    
    public GameObject ShopButtonsContainer, CategoryContainer;
    
   [HideInInspector]  public Character SelectedCharacter;
    private Entity SelectedEntity;
    private ShopButton SelectedButton;
    private CategoryButton SelectedCategory;
    
   public void Initialize(Spawner spawner,Shop shop )
   {
       Console = spawner.ui.Console;
       _spawner = spawner;
       _shop = shop;
       GetShopButtons(); 
       GetCategoryButtons();
       EnableValidCategories(true);
      CategoryButtons[0].OpenCategory();
   }
   
    public void GetShopButtons()
    {
        for (int i = 0; i < ShopButtonsContainer.transform.childCount; i++)
            ItemButtons.Add(ShopButtonsContainer.transform.GetChild(i).GetComponent<ShopButton>());
    }

    public void GetCategoryButtons()
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
        SelectedCategory?.CloseCategory();
        SelectedCategory = button;
    }

    public void HideShopButtons()
    {
        foreach (var button in ItemButtons) 
            button.HighLightButton(false);
    }

    public void SelectCharacter(Character character)
    {
        if (_shop.inShop && character != null)
        {
            SelectedCharacter = character;
            if(SelectedCharacter.Bought)SelectCategory(SelectedCategory);
            EnableValidCategories(true);
        }
    }

    public void SelectCharacterItem(Item item)
    {
        SelectedEntity = item;
    }
    void EnableValidCategories(bool enable)
    {
        foreach (var button in CategoryButtons) 
            button.Enable(false);
        for (int i = 0; i < SelectedCharacter?.MasteryTypes.Types.Count; i++) 
            CategoryButtons[i+1].EnableCategory(SelectedCharacter,i);
        OpenCharacterCategory();
    }

    void OpenCharacterCategory()
    {
        CategoryButtons[0].Enable(_spawner.pool.ActiveHeroes.Count < 5);
    }
    public void SelectItemInShop(ShopButton button, Entity item)
    {
        SelectedButton = button;
        SelectedEntity = item;
        if(SelectedEntity.entityType != EntityType.Hero) 
            Console.ShowInfo((Item)item);
        else
        {
            SelectCharacter((Character) item);
            _spawner.controller.Select((Character)SelectedEntity);
           
        }
           
    }
  
    public void Buy(bool buy)
    {
        if (_shop.Wallet.IsEnough(SelectedEntity.ShopCost) && SelectedCharacter.Bought || (!SelectedCharacter.Bought && SelectedEntity.entityType == EntityType.Hero ) || !buy)
        {
            SelectedEntity.Buy(SelectedCharacter,buy);
            _spawner.ui.ItemUI.UpdateButtons(SelectedCharacter);
            _shop.Wallet.Use(SelectedEntity.ShopCost, buy);
            SelectedButton.ClearButton(SelectedEntity.Bought);
            SelectedCategory.OpenCategory();
        }
    }
    
  
 
   
}
