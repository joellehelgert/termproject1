using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public static class DataLoadingHelpers
{
    public static DetailedActivityTypes GetDetailedActivityType(string type)
    {
        switch (type)
        {

            case "Beach Volleyball":
                return DetailedActivityTypes.BeachVolleyball;

            case "Skateanlage":
                return DetailedActivityTypes.SkatingTrack;
            case "Fitnessanlage":
            case "Streetball":
            case "FunCourt":
                return DetailedActivityTypes.Sportsground;
            default:
                return DetailedActivityTypes.Playground;

        }
    }


    public static string[] GetDataRows(string path)
    {
        TextAsset data = Resources.Load<TextAsset>(path);
        return data.text.Split(new char[] { '\n' });
    }


    public static List<NonScholaric> ConvertNonScholaric(string[] dataRows, NonScholaricType type)
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
                uniqueAddress
                = row[6],
                type = type
            };
            int.TryParse(row[4], out k.Y);
            int.TryParse(row[5], out k.X);
            float.TryParse(FormatCoordinates(row[7]), out k.latitude);
            float.TryParse(FormatCoordinates(row[8]), out k.longitude);

            nonScholaric.Add(k);
        }

        return nonScholaric;
    }

    public static List<School> ConvertSchool(string[] dataRows, SchoolType type)
    {
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

            float.TryParse(FormatCoordinates(elementarySchool[4]), out school.latitude);
            float.TryParse(FormatCoordinates(elementarySchool[5]), out school.longitude);

            schools.Add(school);
        }

        return schools;
    }

    /** Passed string (48 282 967) should look like 48,282967 */
    public static string FormatCoordinates(string coordinateString)
    {
        string formated = coordinateString.Trim();
        Regex regex = new Regex(Regex.Escape(" "));
        formated = regex.Replace(formated, ".", 1);
        formated = formated.Replace(" ", "");
        formated = formated.Replace(",", ".");

        return formated;
    }

    public static HousingInformation[] ParseFlatInformation<T>(HousingInformation[] housingInformation, string[] dataRows, string prop)
    {

        for (int i = 1; i < dataRows.Length - 1; i++)
        {
            string[] row = dataRows[i].Split(new char[] { ';' });
            int.TryParse(row[0], out int districtNumber);
            districtNumber--;

            if (housingInformation[districtNumber] == null)
            {
                housingInformation[districtNumber] = new HousingInformation
                {
                    district = (Districts)districtNumber
                };
            }



            var information = Enum.GetValues(typeof(T));
            int[] counts = new int[information.Length];
            int counter = 0;
            foreach (var inf in information)
            {
                int.TryParse(row[(int)inf + 2], out counts[(int)inf]);
                counter += counts[(int)inf];
            }


            switch (prop)
            {
                case "RoomsPerFlat":
                    housingInformation[districtNumber].FlatCount = counter;
                    housingInformation[districtNumber].RoomsPerFlat = counts;
                    break;
                case "FlatsPerBuilding":
                    housingInformation[districtNumber].FlatsPerBuilding = counts;
                    break;
                case "FloorSpace":
                    housingInformation[districtNumber].FloorSpace = counts;
                    break;

            }
        }
        return housingInformation;
    }

}
