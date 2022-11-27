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

    public void ShowPopUp(ResistanceType result, Vector3 pos, int damage)
    {
        gameObject.SetActive(true);
        transform.position =pos;
        text.text = result + "\n" + damage;
        StartCoroutine(DisableText());
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
    }
    
}
