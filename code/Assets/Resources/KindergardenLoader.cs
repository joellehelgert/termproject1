using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class KindergardenLoader : MonoBehaviour
{

    public List<Hospital> hospitals;
    public GameObject MarkerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Debug message here"); 
        hospitals = new List<Hospital>();
        string[] dataRows = DataLoadingHelpers.GetDataRows("Health/hospitals");
        for (int i = 1; i < dataRows.Length - 1; i++)
        {
            string[] row = dataRows[i].Split(new char[] { ';' });
            Hospital hospital = new Hospital
            {
                name = row[0],
                address = row[1],
                url = row[2],
                category = row[3]

            };

            float.TryParse(DataLoadingHelpers.FormatCoordinates(row[7]), out hospital.latitude);
            float.TryParse(DataLoadingHelpers.FormatCoordinates(row[8]), out hospital.longitude);

            float x = hospital.latitude;
            float y = hospital.longitude;
            float z = 0;
            //instantiate the prefab with coordinates defined above
            Instantiate(MarkerPrefab, new Vector3(x, y, z), Quaternion.identity);

            hospitals.Add(hospital);
            Debug.Log("hospital: " + hospital.name);

            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
