﻿using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "DataStorage", menuName = "DataStorage")]
public class DataLoader : ScriptableObject
{
    public List<NonScholaric> kindergartens;
    public List<NonScholaric> dayCareCenter;
    public List<NonScholaric> toddlerGroups;
    public List<NonScholaric> youthCenters;
    public List<School> elementarySchools;
    public List<School> secondarySchools;
    public List<School> polytechnicalSchools;
    public List<School> academicHighSchools;
    public List<School> specialNeedsSchools;
    public List<AgeDistribution> ageDistributions;
    public List<Activity> activities;
    public List<Hospital> hospitals;
    public List<Transport> transports;

    // sorted by district numbers
    public HousingInformation[] housingInformation;

    // Use this for initialization
    public void LoadAllData()
    {
        LoadToddlerGroups();
        LoadKindergartens();
        LoadDayCareCenters();
        LoadSpecialNeedsSchools();
        LoadElementarySchools();
        LoadSecondarySchools();
        LoadPolytechnicalSchools();
        LoadAcademicHighSchools();
        LoadAgeStructure();
        LoadYouthCenters();
        LoadActivities();
        LoadHousingInformation();
        LoadHospitals();
        // LoadPublicTransport();
    }

    // TODO coordinates are in Grauß-Krüger system ONLY
    public void LoadPublicTransport() {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Transportation/Stops");
        string stop = "";
        float longitude = 0f;
        float latitude = 0f;
        List<string> lines = new List<string>();
        List<string> directions = new List<string>();

        for (int i = 1; i < dataRows.Length - 1; i++)
        {
            string[] row = dataRows[i].Split(new char[] { ';' });
            if(stop != row[0])
            {
                bool isBim = lines[0].Contains("00");
                Transport transport = new Transport
                {
                    name = stop,
                    longitude = longitude,
                    latitude = latitude,
                    lines = lines,
                    isBim = isBim,
                    directions = directions,
                };

                transports.Add(transport);
                lines.Clear();
                directions.Clear();
                Debug.Log("name: " + stop);
            }

            stop = row[0];
            float.TryParse(DataLoadingHelpers.FormatCoordinates(row[4]), out latitude);
            float.TryParse(DataLoadingHelpers.FormatCoordinates(row[5]), out longitude);

            string[] lineArray = row[2].Split(new char[] { ',' });
            foreach(var line in lineArray) { lines.Add(line); Debug.Log("Lines: " + line); }

            string[] dirArray = row[2].Split(new char[] { ',' });
            foreach (var dir in dirArray) { directions.Add(dir); }

        }
    }

    public void LoadHospitals()
    {
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

            hospitals.Add(hospital);
        }
    }

    // ---- Flats ----
    public void LoadHousingInformation()
    {
        string[] dataFloorSpace = DataLoadingHelpers.GetDataRows("HousingInformation/floorSpace");
        string[] dataFlatRooms = DataLoadingHelpers.GetDataRows("HousingInformation/flatRooms");
        string[] dataFlatsPerBuilding = DataLoadingHelpers.GetDataRows("HousingInformation/flatsPerBuilding");

        var districts = Enum.GetValues(typeof(Districts));
        housingInformation = new HousingInformation[districts.Length];
        housingInformation = DataLoadingHelpers.ParseFlatInformation<RoomsPerFlat>(housingInformation, dataFlatRooms, "RoomsPerFlat");
        housingInformation = DataLoadingHelpers.ParseFlatInformation<RoomsPerFlat>(housingInformation, dataFloorSpace, "FloorSpace");
        housingInformation = DataLoadingHelpers.ParseFlatInformation<RoomsPerFlat>(housingInformation, dataFlatsPerBuilding, "FlatsPerBuilding");
    }

    // ---- Activities ----
    public void LoadActivities()
    {
        activities = new List<Activity>();
        string[] dataRows = DataLoadingHelpers.GetDataRows("Activities/activities");

        for (int i = 1; i < dataRows.Length - 1; i++)
        {
            string[] row = dataRows[i].Split(new char[] { ';' });
            Activity activity = new Activity {
                name = row[1],
                area = row[2],
                urbanArea = row[5],
                url = row[7],
                //street = row[9],
            };

            int.TryParse(row[3], out int number);
            activity.district = (Districts)number;
            //int.TryParse(row[10], out activity.zip);

            float.TryParse(DataLoadingHelpers.FormatCoordinates(row[12]), out activity.latitude);
            float.TryParse(DataLoadingHelpers.FormatCoordinates(row[13]), out activity.longitude);

            activity.type = row[0] == "Sportanlage" ? ActivityType.Sportsground : ActivityType.Playground;
            activity.detailedActivityType = DataLoadingHelpers.GetDetailedActivityType(row[6]);

            activities.Add(activity);
        }
    }

    // ---- Age ----
    public void LoadAgeStructure()
    {
        ageDistributions = new List<AgeDistribution>();
        string[] dataRows = DataLoadingHelpers.GetDataRows("Age/data");

        for (int i = 1; i < dataRows.Length - 1; i++)
        {
            string[] row = dataRows[i].Split(new char[] { ';' });
            AgeDistribution distribution = new AgeDistribution
            {
                ages = new int[row.Length - 2],
                district = (Districts)(i - 1)
            };

            for(int age = 2; age < dataRows.Length -1; age++)
            {
                int.TryParse(dataRows[age], out distribution.ages[age]);
            }

            ageDistributions.Add(distribution);
        }
    }


    // ---- Schools ----
    public void LoadSpecialNeedsSchools()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Education/specialNeedsSchool2018");
        specialNeedsSchools = DataLoadingHelpers.ConvertSchool(dataRows, SchoolType.SpecialNeedsSchool);

    }
    
    public void LoadAcademicHighSchools()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Education/academicHighSchools");
        academicHighSchools = DataLoadingHelpers.ConvertSchool(dataRows, SchoolType.AcademicHighSchool);

    }

    public void LoadPolytechnicalSchools()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Education/polytechnicalSchools2018");
        polytechnicalSchools = DataLoadingHelpers.ConvertSchool(dataRows, SchoolType.PolytechnicalSchool);

    }

    public void LoadSecondarySchools()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Education/secondarySchools2018");
        secondarySchools = DataLoadingHelpers.ConvertSchool(dataRows, SchoolType.SecondarySchool);

    }

    public void LoadElementarySchools()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Education/elementarySchools");
        elementarySchools = DataLoadingHelpers.ConvertSchool(dataRows, SchoolType.ElementarySchool);

    }

    // ---- non scholaric ----
    public void LoadYouthCenters()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Activities/youthCenters");
        youthCenters = DataLoadingHelpers.ConvertNonScholaric(dataRows, NonScholaricType.YouthCenter);
    }

    public void LoadKindergartens()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Education/kindergartens");
        kindergartens = DataLoadingHelpers.ConvertNonScholaric(dataRows, NonScholaricType.Kindergarten);
    }

    public void LoadDayCareCenters()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Education/dayCareCenters");
        dayCareCenter = DataLoadingHelpers.ConvertNonScholaric(dataRows, NonScholaricType.DayCareCenter);
    }

    public void LoadToddlerGroups()
    {
        string[] dataRows = DataLoadingHelpers.GetDataRows("Education/toddlerGroups");
        dayCareCenter = DataLoadingHelpers.ConvertNonScholaric(dataRows, NonScholaricType.ToddlerGroup);
    }

}
