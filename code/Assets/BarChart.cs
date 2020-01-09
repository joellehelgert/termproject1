using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarChart : MonoBehaviour
{
    public Bar barPrefab;

    List<Bar> bars = new List<Bar>();

    float chartHeight; 

    void Start()
    {
        chartHeight = Screen.height + GetComponent<RectTransform>().sizeDelta.y; 
        float[] values = { 0.1f, 0.2f, 0.7f };
        DisplayGraph(values); 
    }

    void DisplayGraph(float[] vals)
    {
        for(int i=0; i < vals.Length; i++)
        {
            Bar newBar = Instantiate(barPrefab) as Bar;
            newBar.transform.SetParent(transform);
            RectTransform rt = newBar.bar.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, chartHeight * vals[i]); 
        }
    }
}
