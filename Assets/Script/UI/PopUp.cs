using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private List<PopUp> _list;
    private Image _icon;
    private float _textAlpha, _iconAlpha;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _icon = transform.GetChild(0).GetComponent<Image>();
    }

    public void ShowPopUp( Status status, Vector2 pos, List<PopUp> list)
    {
        SetColor(status);
        _icon.sprite = status.Icon;
        _list = list;
        _list.Remove(this);
        gameObject.SetActive(true);
        transform.position = pos;
        _text.text =$"{status.Points.ToString()}";
        StartCoroutine(PlayAnimation());
    }

    void SetColor(Status points)
    {
        _text.color = points.TraitCurrent > 0 ? new Color(0.07f,0.7f,0.1f,0) : new Color(0.7f,0.07f,0.1f,0);
    }
    
    IEnumerator PlayAnimation()
    {
        while (_icon.color.a <1)
        { 
            _text.color += new Color(0,0,0,0.01f) ;
           _icon.color += new Color(0,0,0,0.01f) ;
           transform.position += new Vector3(0, 0.003f, 0);
            yield return new WaitForEndOfFrame();
        }
        while (_icon.color.a >=0)
        {
            transform.position += new Vector3(0, 0.003f, 0);
            _text.color -= new Color(0,0,0,0.001f) ;
           _icon.color -= new Color(0,0,0,0.001f) ;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);
        _list.Add(this);
        gameObject.SetActive(false);
    }
}
