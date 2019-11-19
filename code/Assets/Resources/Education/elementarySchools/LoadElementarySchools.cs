using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadElementarySchools : MonoBehaviour
{
    List<ElementarySchool> elementarySchools = new List<ElementarySchool>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("in start");
        TextAsset data = Resources.Load<TextAsset>("Education/elementarySchools/data");
        string[] dataRows = data.text.Split(new char[] { '\n' });
        Debug.Log(dataRows.Length);

        for (int i = 1; i < dataRows.Length - 1; i++)
        {
            string[] elementarySchool = dataRows[i].Split(new char[] { ';' });
            ElementarySchool school = new ElementarySchool
            {
                name = elementarySchool[0]
            };
            int.TryParse(elementarySchool[1], out school.classCount);
            int.TryParse(elementarySchool[2], out school.maleStudents);
            int.TryParse(elementarySchool[3], out school.femaleStudents);


            // Not provided yet
            // float.TryParse(elementarySchool[7], out school.latitude);
            // float.TryParse(elementarySchool[8], out school.longitude);

            elementarySchools.Add(school);
        }

        foreach (ElementarySchool e in elementarySchools)
        {
            Debug.Log(e.name);
        }
    }

}
