using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image CardShell;
    public Image Image;
    public TextMeshProUGUI Name, Cost, Dam;


    public void CreateButton(ICharacter character,int i)
    {
        Name.text =character.GetWeapon(i).GetName();
        Cost.text = character.GetWeapon(i).GetCost().ToString();
        Cost.color = character.GetStats().AP >= character.GetWeapon(i).GetCost() ? Color.green : Color.red;
        Dam.color = GetDamageColor(character.GetWeapon(i).GetWeaponElement());
        Dam.text = character.GetWeapon(i).GetDamageAmount().ToString();
    }

    Color GetDamageColor(Weapon.Element e)
    {
        Color c = e switch
        {
            Weapon.Element.Electric => Color.yellow,
            Weapon.Element.Fire => Color.red,
            Weapon.Element.Physic => Color.gray,
            Weapon.Element.Water => Color.cyan,
            Weapon.Element.Wave => Color.white,
            _ => new Color()
        };
        return c;

    }
}
