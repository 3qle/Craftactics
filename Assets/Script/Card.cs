using System.Collections;
using System.Collections.Generic;
using Script.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image CardShell;
    public Image Image;
    public TextMeshProUGUI Name, Cost, Dam;


    public void UpdateButtonInfo(IWeapon weapon, ICharacter character)
    {
        Name.text = weapon.Name;
        Cost.text = weapon.ApCost.ToString();
        Cost.color = character.Stamina >= weapon.ApCost ? Color.green : Color.red;
        Dam.color = GetDamageColor(weapon.WeaponElement);
        Dam.text = weapon.Damage.ToString();
    }

    Color GetDamageColor(Element e)
    {
        Color c = e switch
        {
            Element.Electric => Color.yellow,
            Element.Fire => Color.red,
            Element.Physic => Color.gray,
            Element.Water => Color.cyan,
            Element.Wave => Color.white,
            _ => new Color()
        };
        return c;

    }
}
