using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataLoader : MonoBehaviour
{
    public List<NonScholaric> kindergartens;
    public List<NonScholaric> dayCareCenter;
    public List<NonScholaric> toddlerGroups;
    public List<School> elementarySchools;
    public List<School> secondarySchools;
    public List<School> polytechnicalSchools;
    public List<School> academicHighSchools;
    public List<School> specialNeedsSchools;
 

    // Use this for initialization
    void Start()
    {
        LoadToddlerGroups();
        LoadKindergartens();
        LoadDayCareCenters();
        LoadSpecialNeedsSchools();
        LoadElementarySchools();
        LoadSecondarySchools();
        LoadPolytechnicalSchools();
        LoadAcademicHighSchools();
    }

    // Update is called once per frame
    void Update()
    {

    }

    string[] GetDataRows(string path)
    {
        TextAsset data = Resources.Load<TextAsset>(path);
        return data.text.Split(new char[] { '\n' });
    }

    public void LoadSpecialNeedsSchools()
    {
        string[] dataRows = GetDataRows("Education/specialNeedsSchool2018");
        specialNeedsSchools = ConvertSchool(dataRows, SchoolType.SpecialNeedsSchool);

    }
    
    public void LoadAcademicHighSchools()
    {
        string[] dataRows = GetDataRows("Education/academicHighSchools");
        academicHighSchools = ConvertSchool(dataRows, SchoolType.AcademicHighSchool);

    }

    public void LoadPolytechnicalSchools()
    {
        string[] dataRows = GetDataRows("Education/polytechnicalSchools2018");
        polytechnicalSchools = ConvertSchool(dataRows, SchoolType.PolytechnicalSchool);

    }

    public void LoadSecondarySchools()
    {
        string[] dataRows = GetDataRows("Education/secondarySchools2018");
        secondarySchools = ConvertSchool(dataRows, SchoolType.SecondarySchool);

    }

    public void LoadElementarySchools()
    {
        string[] dataRows = GetDataRows("Education/elementarySchools");
        elementarySchools = ConvertSchool(dataRows, SchoolType.ElementarySchool);

    }

    public void LoadKindergartens()
    {
        string[] dataRows = GetDataRows("Education/kindergartens");
        kindergartens = ConvertNonScholaric(dataRows, NonScholaricType.Kindergarten);
    }

    public void LoadDayCareCenters()
    {
        string[] dataRows = GetDataRows("Education/dayCareCenters");
        dayCareCenter = ConvertNonScholaric(dataRows, NonScholaricType.DayCareCenter);
    }

    public void LoadToddlerGroups()
    {
        string[] dataRows = GetDataRows("Education/toddlerGroups");
        dayCareCenter = ConvertNonScholaric(dataRows, NonScholaricType.ToddlerGroup);
    }

    private List<NonScholaric> ConvertNonScholaric(string[] dataRows, NonScholaricType type)
    {
        List<NonScholaric> nonScholaric = new List<NonScholaric>();

        for (int i = 1; i < dataRows.Length - 1; i++)
        {
            string[] row = dataRows[i].Split(new char[] { ';' });
            NonScholaric k = new NonScholaric
            {
                institiution = row[0],
                address = row[1],
                url = row[2],
                category = row[3],
                uniqueAdress = row[6],
                type = type
            };
            int.TryParse(row[4], out k.Y);
            int.TryParse(row[5], out k.X);
            float.TryParse(row[7], out k.latitude);
            float.TryParse(row[8], out k.longitude);

            nonScholaric.Add(k);
            Debug.Log(k.institiution);
        }

        return nonScholaric;
    }

    private List<School> ConvertSchool(string[] dataRows, SchoolType type)
    {
        Debug.Log(dataRows[0]);
        List<School> schools = new List<School>();
        for (int i = 1; i < dataRows.Length - 1; i++)
        {
            string[] elementarySchool = dataRows[i].Split(new char[] { ';' });
            School school = new School
            {
                type = type,
                name = elementarySchool[0]
            };
            int.TryParse(elementarySchool[1], out school.classCount);
            int.TryParse(elementarySchool[2], out school.maleStudents);
            int.TryParse(elementarySchool[3], out school.femaleStudents);

            // Not provided yet
            // float.TryParse(elementarySchool[4], out school.latitude);
            // float.TryParse(elementarySchool[5], out school.longitude);

            schools.Add(school);
            Debug.Log(school.name);
        }
       
        return schools;
    }
}
