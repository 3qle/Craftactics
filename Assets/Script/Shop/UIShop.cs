using System;
using System.Collections.Generic;
using System.Linq;
using Script.Character;
using Script.Enum;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIShop
{
    private Console Console;
    private Spawner _spawner; 
    [HideInInspector] public Shop _shop;
   
    private List<CategoryButton> CategoryButtons = new List<CategoryButton>();
    private List<ShopButton> ItemButtons = new List<ShopButton>();
    
    public GameObject ShopButtonsContainer, CategoryContainer;
    public Character SelectedCharacter;
    public Entity SelectedEntity;
    public CategoryButton SelectedCategory;
    private Item SelectedItem;
    public BuyButton BuyButton;
    public Button ShopButton;
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
       BuyButton._button.onClick.RemoveAllListeners();
       BuyButton._button.onClick.AddListener(() =>Buy(BuyButton.CanBuy));
       ShopButton.onClick.AddListener(() => _shop.OpenShop(false));
   }
   public void SelectCharacterItem(Item item)
   {
       SelectedEntity = item;
       BuyButton.SetCost(item.ActiveItem.ShopCost, _shop.Wallet.Coins,SelectedCharacter,SelectedEntity);
      
       
   }

   public void SelectCategory(CategoryButton button)
   {
       SelectedCategory = button;
       BuyButton.Disable("Choose");
   }
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
     foreach (var category in CategoryButtons)
         category.Clear();
     if (character.entityType == EntityType.Hero)
     {
         SelectedCharacter = character;
         for (int i = 0; i < SelectedCharacter.MasteryTypes.Types.Count; i++)
             CategoryButtons[i+1].Change(SelectedCharacter.MasteryTypes.Types[i]);
         SelectedCategory.Open();
     }
     else
     {
         BuyButton.Disable("Choose");
         SelectedCategory.RemoveItems();
     }
    
     CategoryButtons[0].Change(EntityType.Hero);
 }

   private bool CanBuyCharacter => !SelectedCharacter.Bought && SelectedEntity.entityType == EntityType.Hero;
   private bool CanBuyWeapon => SelectedCharacter.Bought && (SelectedCharacter.Bag.HasWeaponSlot && SelectedItem.itemType == ItemType.Weapon);
   private bool CanBuyItem => SelectedItem.itemType == ItemType.Item && SelectedCharacter.Bag.HasItemSlot;
   private bool CanBuyProjectile => SelectedItem.itemType == ItemType.Projectile && !SelectedCharacter.Bag.Weapons[SelectedCharacter.Bag.Weapons.IndexOf(SelectedCharacter.Arms.selectedItem)].ProjectilesFull(SelectedItem);
   private bool CanBuy => SelectedCharacter.entityType == EntityType.Hero && _shop.Wallet.IsEnough(SelectedEntity.ShopCost) 
                                                                           && (CanBuyCharacter || CanBuyWeapon || CanBuyProjectile || CanBuyItem);
    public void Buy(bool buy)
    {
        Debug.Log("buy");
        if (SelectedEntity.entityType != EntityType.Hero) 
            SelectedItem = (Item)SelectedEntity;
      
        if (!BuyButton.CanBuy && !buy || (buy && CanBuy))
        {
            
          Debug.Log($"{!BuyButton.CanBuy && !buy} sell {buy && CanBuy} buy");
          SelectedEntity.Buy(SelectedCharacter,buy);
          _spawner.pool.BuyEntity(SelectedEntity,buy);
          SelectedCategory.Open();
          _spawner.ui.ItemUI.UpdateButtons(SelectedCharacter);
          _shop.Wallet.Use(SelectedEntity.ShopCost, buy);
          EnableCloseShopButton();
          foreach (var button in _spawner.ui.ItemUI.Items.Where(button => button._itemOnButton == SelectedEntity))
              button.SelectWeapon();
              
      }
  }

    public void SelectEntity(Entity entity)
    {
        SelectedEntity =_spawner.pool.CategoryPool[_spawner.pool.EntityPool.IndexOf(entity)][_spawner.pool.CategoryPool[_spawner.pool.EntityPool.IndexOf(entity)].Count -1];
        if(entity.entityType != EntityType.Hero) 
            Console.ShowInfo((Item)SelectedEntity);
        BuyButton.SetCost(SelectedEntity.ShopCost, _shop.Wallet.Coins,SelectedCharacter,SelectedEntity);
    }

    void EnableCloseShopButton()
    {
        _shop.CanClose = _spawner.pool.ActiveHeroes.Count > 0 && _spawner.pool.ActiveHeroes[0].Bag.AllItems.Count > 0;
        ShopButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Shop/BuyButton/" + _shop.CanClose);
    }
}
