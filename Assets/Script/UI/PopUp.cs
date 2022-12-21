using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ShowPopUp(AttackResult result, Vector3 pos, float damage)
    {
        gameObject.SetActive(true);
        transform.position =pos;
        string damagetext = damage <= 0 ? "" : damage.ToString();
        text.text =$"{result}\n{damagetext}";
        StartCoroutine(DisableText());
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
    }
    
}
