using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ExperienceButton : MonoBehaviour
{
    private TextMeshProUGUI ButtonText;
    private void Awake() 
        => ButtonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    
    public void UpdateText(Attribute attribute)
    {
        ButtonText.text = attribute == null ? "" : attribute.current.ToString();;
        ButtonText.color = attribute == null
                ? Color.white:attribute.current > attribute.max
                ? Color.green
                : attribute.current == attribute.max
                    ? Color.white
                    : Color.red;
    }
}
