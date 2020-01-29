using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using System;

public class ChooseDistrict : MonoBehaviour
{

    // List<string> names = new List<string>() { "New Text","Fred", "Barney", "Wilma" };

    public Dropdown dropdown;
    [SerializeField]
    public DataLoader dataLoader;
    public void Dropdown_IndexChanged(int index)
    {
        Districts name = (Districts)index;
        Debug.Log(dataLoader);
        dataLoader.addDistrict(name);

    }

    private void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        string[] enumDistricts = Enum.GetNames(typeof(Districts));
        List<string> districts = new List<string>(enumDistricts);
        districts.Insert(0, "Choose District");
        dropdown.AddOptions(districts);
    }




}
