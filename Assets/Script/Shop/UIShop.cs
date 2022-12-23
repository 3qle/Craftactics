using System;
using System.Collections.Generic;
using Script.Enum;
using UnityEngine;

[Serializable]
public class UIShop
{
    private Console Console;
    private Spawner _spawner; 
    public Shop _shop;
   
    private List<CategoryButton> CategoryButtons = new List<CategoryButton>();
    private List<ShopButton> ItemButtons = new List<ShopButton>();
    
    public GameObject ShopButtonsContainer, CategoryContainer;
    public Character SelectedCharacter;
    private Entity SelectedEntity;
    public CategoryButton SelectedCategory;
    
   public void Initialize(Spawner spawner,Shop shop )
   {
       SelectedCharacter = spawner.controller._selectable;
       Console = spawner.ui.Console;
       _spawner = spawner;
       _shop = shop;
       GetItemButtons(); 
       GetCategoryButtons();
       
       CategoryButtons[0].Change(EntityType.Hero);
       CategoryButtons[0].Open();
       SelectedCategory = CategoryButtons[0];
   }
   public void SelectCharacterItem(Item item)=>SelectedEntity = item;
   public void SelectCategory(CategoryButton button)=>SelectedCategory = button;
   public void GetItemButtons()
   {
       for (int i = 0; i < ShopButtonsContainer.transform.childCount; i++) 
           ItemButtons.Add(ShopButtonsContainer.transform.GetChild(i).GetComponent<ShopButton>());
   }
   
   public void GetCategoryButtons()
   {
       for (int i = 0; i < CategoryContainer.transform.childCount; i++)
       {
           CategoryButtons.Add(CategoryContainer.transform.GetChild(i).GetComponent<CategoryButton>());
           CategoryButtons[i].Initialize(ItemButtons, _spawner.pool.EntityPool, this,CategoryButtons);
       }
   }
   public  void ChangeCategories(Character character)
 { 
     SelectedCharacter = character;
     
     foreach (var category in CategoryButtons)
         category.Clear();
     for (int i = 0; i < SelectedCharacter.MasteryTypes.Types.Count; i++)
         CategoryButtons[i+1].Change(SelectedCharacter.MasteryTypes.Types[i]);
      
     CategoryButtons[0].Change(EntityType.Hero);
     SelectedCategory.Open();
 }
  
 
    public void Buy(bool buy)
    {
        if (_shop.Wallet.IsEnough(SelectedEntity.ShopCost) && SelectedCharacter.Bought || (!SelectedCharacter.Bought && SelectedEntity.entityType == EntityType.Hero ) || !buy)
        {
            SelectedEntity.Buy(SelectedCharacter,buy);
            SelectedCategory.Open();
            _spawner.ui.ItemUI.UpdateButtons(SelectedCharacter);
            _shop.Wallet.Use(SelectedEntity.ShopCost, buy);
        }
    }

    public void SelectEntity(Entity entity)
    {
        SelectedEntity = entity;
        if(entity.entityType != EntityType.Hero) 
            Console.ShowInfo((Item)entity);
    }
}
