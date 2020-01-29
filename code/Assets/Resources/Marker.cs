using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
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

    private void loadPrefabs()
    {
        hospital = Resources.Load("MarkerIcons/hospital") as GameObject;
        busStop = Resources.Load("MarkerIcons/busStop") as GameObject;
        train = Resources.Load("MarkerIcons/train") as GameObject;
        nonScholaric = Resources.Load("MarkerIcons/nonScholaric") as GameObject;
        basketball = Resources.Load("MarkerIcons/basketball") as GameObject;
        playground = Resources.Load("MarkerIcons/playground") as GameObject;
        higherEducation = Resources.Load("MarkerIcons/higherEducation") as GameObject;
        lowerEducation = Resources.Load("MarkerIcons/lowerEducation") as GameObject;
        skateTrack = Resources.Load("MarkerIcons/skateTrack") as GameObject;
        volleyball = Resources.Load("MarkerIcons/volleyball") as GameObject;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateMarker(string type, DataLoader dataLoader)
    {
        if (!hospital)
        {
            loadPrefabs();
        }

        switch (type)
        {
            case "hospitals":
                dataLoader.LoadHospitals();
                CreateHospitals(dataLoader, type);
                break;
            case "transports":
                dataLoader.LoadPublicTransport();
                CreatePublicTransport(dataLoader, type);
                break;
            case "kindergartens":
                dataLoader.LoadKindergartens();
                CreateKindergardens(dataLoader, type);
                break;
            case "elementarySchools":
                dataLoader.LoadElementarySchools();
                CreateElementarySchools(dataLoader, type);
                break;
            case "dayCareCenters":
                dataLoader.LoadDayCareCenters();
                CreateDayCareCenters(dataLoader, type);
                break;
            case "activities":
                dataLoader.LoadActivities();
                CreateActivities(dataLoader, type);
                break;
            case "toddlerGroups":
                dataLoader.LoadToddlerGroups();
                CreateToddlerGroups(dataLoader, type);
                break;
            case "highschools":
                dataLoader.LoadAcademicHighSchools();
                CreateAcademicHighSchools(dataLoader, type);
                break;
            case "secondarySchools":
                dataLoader.LoadSecondarySchools();
                CreateSecondarySchools(dataLoader, type);
                break;
            case "polytechnicalSchools":
                dataLoader.LoadPolytechnicalSchools();
                CreatePolytechnicalSchools(dataLoader, type);
                break;
            case "education":
                dataLoader.LoadAcademicHighSchools();
                CreateAcademicHighSchools(dataLoader, type);
                dataLoader.LoadPolytechnicalSchools();
                CreatePolytechnicalSchools(dataLoader, type);
                dataLoader.LoadSecondarySchools();
                CreateSecondarySchools(dataLoader, type);
                dataLoader.LoadElementarySchools();
                CreateElementarySchools(dataLoader, type);
                break;

        }
    }

    private void CreateHospitals(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");


        foreach (var h in dataLoader.hospitals)
        {
            Vector2 vector = util.ConvertGeoCoordinates(h.latitude, h.longitude);
            GameObject hospitalVisual;

            hospitalVisual = (Instantiate(hospital, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            hospitalVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }

    private void CreateActivities(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");

        foreach (var activity in dataLoader.activities)
        {
            Vector2 vector = util.ConvertGeoCoordinates(activity.latitude, activity.longitude);
            GameObject activitiVisual;
            GameObject visual = GetActivityVisual(activity.detailedActivityType);

            activitiVisual = (Instantiate(visual, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            activitiVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
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

    private void CreateToddlerGroups(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");

        foreach (var h in dataLoader.toddlerGroups)
        {
            Vector2 vector = util.ConvertGeoCoordinates(h.latitude, h.longitude);
            GameObject nonScholaricVisual;
            nonScholaricVisual = (Instantiate(nonScholaric, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            nonScholaricVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }

    private void CreateDayCareCenters(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");

        foreach (var dcc in dataLoader.dayCareCenter)
        {
            Vector2 vector = util.ConvertGeoCoordinates(dcc.latitude, dcc.longitude);
            GameObject nonScholaricVisual;
            nonScholaricVisual = (Instantiate(nonScholaric, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            nonScholaricVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }

    private void CreateKindergardens(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");
        foreach (var k in dataLoader.kindergartens)
        {
            Vector2 vector = util.ConvertGeoCoordinates(k.latitude, k.longitude);
            GameObject nonScholaricVisual;
            nonScholaricVisual = (Instantiate(nonScholaric, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            nonScholaricVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }

    private void CreateElementarySchools(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");

        foreach (var elementarySchool in dataLoader.elementarySchools)
        {
            Vector2 vector = util.ConvertGeoCoordinates(elementarySchool.latitude, elementarySchool.longitude);
            GameObject lowerEducationVisual;
            lowerEducationVisual = (Instantiate(lowerEducation, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            lowerEducationVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }


    private void CreateAcademicHighSchools(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");

        foreach (var highschool in dataLoader.academicHighSchools)
        {
            Vector2 vector = util.ConvertGeoCoordinates(highschool.latitude, highschool.longitude);
            GameObject higherEducationVisual;
            higherEducationVisual = (Instantiate(higherEducation, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            higherEducationVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }

    private void CreateSecondarySchools(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");

        foreach (var secondarySchool in dataLoader.secondarySchools)
        {
            Vector2 vector = util.ConvertGeoCoordinates(secondarySchool.latitude, secondarySchool.longitude);
            GameObject higherEducationVisual;
            higherEducationVisual = (Instantiate(higherEducation, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            higherEducationVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }

    private void CreatePolytechnicalSchools(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");

        foreach (var polytechnicalSchool in dataLoader.polytechnicalSchools)
        {
            Vector2 vector = util.ConvertGeoCoordinates(polytechnicalSchool.latitude, polytechnicalSchool.longitude);
            GameObject higherEducationVisual;
            higherEducationVisual = (Instantiate(higherEducation, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            higherEducationVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }

    private void CreatePublicTransport(DataLoader dataLoader, string type)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");

        foreach (var t in dataLoader.transports)
        {
            Vector2 vector = util.ConvertGaussCoordinates(t.x, t.y);
            GameObject busStopVisual;
            busStopVisual = (Instantiate(busStop, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            busStopVisual.transform.parent = wrapper.transform;
        }
        wrapper.transform.SetParent(parent.transform);
    }

    private void Create(string type, string prefabName, List<PointBasedData> data)
    {
        Utils util = new Utils();
        GameObject wrapper = new GameObject();
        wrapper.name = type;
        GameObject parent = GameObject.Find("ImageTarget");


        foreach (var h in data)
        {
            Vector2 vector = util.ConvertGeoCoordinates(h.latitude, h.longitude);
            GameObject hospitalVisual;
            GameObject prefab = Resources.Load(prefabName) as GameObject;
            hospitalVisual = (Instantiate(prefab, new Vector3(vector.x, 0, vector.y), Quaternion.identity) as GameObject);
            hospitalVisual.transform.parent = wrapper.transform;
        }

        wrapper.transform.SetParent(parent.transform);
    }

}
