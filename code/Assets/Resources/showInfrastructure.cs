using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showInfrastructure : MonoBehaviour
{
	public GameObject hospital;
	[SerializeField]
	public DataLoader dataLoader;
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log(dataLoader);
		dataLoader.LoadAllData();
		ShowHospitals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowHospitals()
	{
		Utils util = new Utils();
		Debug.Log(dataLoader.hospitals);
		foreach (var h in dataLoader.hospitals)
		{
			float x = util.ConvertRealXToGameX(h.latitude);
			float y = util.ConvertRealYToGameY(h.longitude);
			Debug.Log("position of " + h.name + " is " + x + ", " + y);
			GameObject hospitalVisual;
			hospitalVisual = (Instantiate(hospital, new Vector3(x, 0, y), Quaternion.identity) as GameObject);
			hospitalVisual.transform.parent = transform;
		}
		//GameObject hospitalVisual1;
		//hospitalVisual1 = (Instantiate(hospital, new Vector3(util.refPosition1_x, util.refPosition1_x, -3.3f), Quaternion.identity) as GameObject);
		//hospitalVisual1.transform.parent = transform;
		//GameObject hospitalVisual;
		//hospitalVisual = (Instantiate(hospital, transform.position, Quaternion.identity) as GameObject);
		//hospitalVisual.transform.parent = transform;
		Debug.Log("-------Position: " + transform.position);
	}
}
