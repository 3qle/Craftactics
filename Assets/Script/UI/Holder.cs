using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holder : MonoBehaviour
{
    public int Amount;
    public List<Image> Points;

    void Start()
    {

    }

    public void ShowResistance(int i)
    {
        for (int j = 0; j < Points.Count/10; j++)
        {
            Points[i].gameObject.SetActive(true);
            Points[i].color = SetColor(i);
        }
    }

    Color SetColor(int i) => i < 0 ? Color.red : Color.green;
}
    
