using UnityEngine;

public class showInfrastructure : MonoBehaviour
{
	public GameObject hospital;
    public GameObject busStop;
    public GameObject train;
    public GameObject nonScholaric;
    public GameObject basketball;
    public GameObject playground;
    public GameObject higherEducation;
    public GameObject lowerEducation;
    public GameObject skateTrack;
    public GameObject volleyball;

    [SerializeField]
	public DataLoader dataLoader;
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log(dataLoader);
		dataLoader.LoadAllData();

        // working 
        ShowHospitals();
        //ShowToddlerGroups();
        //ShowDayCareCenters();
        //ShowKindergardens();

        // wrong positions
        //ShowAcademicHighSchools();
        //ShowPolytechnicalSchools();
        //ShowSecondarySchools();
        // ShowElementarySchools();
        // ShowActivities();

        // Gauss
		ShowPublicTransport();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowHospitals()
	{
		Utils util = new Utils();
		foreach (var h in dataLoader.hospitals)
		{
            Vector2 vector = util.ConvertGeoCoordinates(h.latitude, h.longitude);
			GameObject hospitalVisual;
            Debug.Log("position of kh " + h.name + " is at x: " + vector.x + " and y: " + vector.y);
            hospitalVisual = (Instantiate(hospital, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
			hospitalVisual.transform.parent = transform;
		}
	}

    void ShowActivities() {
        Utils util = new Utils();
        foreach (var activity in dataLoader.activities)
        {
            Vector2 vector = util.ConvertGeoCoordinates(activity.latitude, activity.longitude);
            GameObject activitiVisual;
            GameObject visual = GetActivityVisual(activity.detailedActivityType);

            activitiVisual = (Instantiate(visual, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            activitiVisual.transform.parent = transform;
        }
    }

    private GameObject GetActivityVisual(DetailedActivityTypes type)
    {
        switch (type)
        {

            case DetailedActivityTypes.BeachVolleyball:
                return volleyball;

            case DetailedActivityTypes.SkatingTrack:
                return skateTrack;
            case DetailedActivityTypes.Sportsground:
                return basketball;
            default:
                return playground;

        }
    }

    void ShowToddlerGroups()
    {
        Utils util = new Utils();
        foreach (var h in dataLoader.toddlerGroups)
        {
            Vector2 vector = util.ConvertGeoCoordinates(h.latitude, h.longitude);
            GameObject nonScholaricVisual;
            nonScholaricVisual = (Instantiate(nonScholaric, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            nonScholaricVisual.transform.parent = transform;
        }
    }

    void ShowDayCareCenters()
    {
        Utils util = new Utils();
        foreach (var dcc in dataLoader.dayCareCenter)
        {
            Vector2 vector = util.ConvertGeoCoordinates(dcc.latitude, dcc.longitude);
            GameObject nonScholaricVisual;
            nonScholaricVisual = (Instantiate(nonScholaric, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            nonScholaricVisual.transform.parent = transform;
        }
    }

    void ShowKindergardens()
    {
        Utils util = new Utils();
        foreach (var k in dataLoader.kindergartens)
        {
            Vector2 vector = util.ConvertGeoCoordinates(k.latitude, k.longitude);
            GameObject nonScholaricVisual;
            nonScholaricVisual = (Instantiate(nonScholaric, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            nonScholaricVisual.transform.parent = transform;
        }
    }

    void ShowElementarySchools()
    {
        Utils util = new Utils();
        foreach (var elementarySchool in dataLoader.elementarySchools)
        {
            Vector2 vector = util.ConvertGeoCoordinates(elementarySchool.latitude, elementarySchool.longitude);
            GameObject lowerEducationVisual;
            lowerEducationVisual = (Instantiate(lowerEducation, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            lowerEducationVisual.transform.parent = transform;
        }
    }


    void ShowAcademicHighSchools()
    {
        Utils util = new Utils();
        foreach (var highschool in dataLoader.academicHighSchools)
        {
            Vector2 vector = util.ConvertGeoCoordinates(highschool.latitude, highschool.longitude);
            GameObject higherEducationVisual;
            higherEducationVisual = (Instantiate(higherEducation, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            higherEducationVisual.transform.parent = transform;
        }
    }

    void ShowSecondarySchools()
    {
        Utils util = new Utils();
        foreach (var secondarySchool in dataLoader.secondarySchools)
        {
            Vector2 vector = util.ConvertGeoCoordinates(secondarySchool.latitude, secondarySchool.longitude);
            GameObject higherEducationVisual;
            higherEducationVisual = (Instantiate(higherEducation, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            higherEducationVisual.transform.parent = transform;
        }
    }

    void ShowPolytechnicalSchools()
    {
        Utils util = new Utils();
        foreach (var polytechnicalSchool in dataLoader.polytechnicalSchools)
        {
            Vector2 vector = util.ConvertGeoCoordinates(polytechnicalSchool.latitude, polytechnicalSchool.longitude);
            GameObject higherEducationVisual;
            higherEducationVisual = (Instantiate(higherEducation, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            higherEducationVisual.transform.parent = transform;
        }
    }
    void ShowPublicTransport()
    {
        Utils util = new Utils();
        foreach (var t in dataLoader.transports)
        {
            Vector2 vector = util.ConvertGaussCoordinates(t.x, t.y);
            GameObject busStopVisual;
            busStopVisual = (Instantiate(busStop, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            busStopVisual.transform.parent = transform;
        }
    }
}