using UnityEngine;
using UnityEngine.UI;
public class ItemButton : MonoBehaviour
{
    private Image _itemImage;
    private UIItem _uiItem;
    public Item _itemOnButton;
    private int _buttonIndex;
    private float _imageValue;
    
    public void Initialize(int i, UIItem ui)
    {
        _itemImage = GetComponent<Image>();
        _uiItem = ui;
        _buttonIndex = i;
    }

    public void SelectWeapon()
    { 
        
        _uiItem.SelectWeapon(_buttonIndex);
        HighLightButton(true);
    } 

    public void UpdateButtonInfo( Character character, int i)
    {
        if (i < character.Bag.AllItems.Count)
        {
            _itemOnButton =  character.Bag.AllItems[i];
            _itemImage.enabled = true;
            _itemImage.color = Color.HSVToRGB(0,character.Attributes.stamina.current >= _itemOnButton.staminaCost ? 0f : 0.5f , _imageValue);
            _itemImage.sprite =_itemOnButton.Icon.sprite;
        }
        else ClearButton();
    }
    
    public void ClearButton()
    {
        HighLightButton(false);
        _itemImage.enabled = false;
        _itemOnButton = null;
    }
    
    public void HighLightButton(bool selected)
    {
      _imageValue = selected ? 1 : 0.5f; 
      _itemOnButton?.Deselect();
    }
}
