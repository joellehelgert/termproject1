using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    // lower 48.3073220 (x), 14.2855114 (y)
    // upper 48.3092174, 14.2841256
    // lower x = -0.5079 y = -6.69 z = 0.2024
    // upper x = -0.5236 y = -6.69 z = 0.2355

    // The ref point ONLY works it the image target is at (0/0/0)
    static float refLatitude1 = 48.3073220f;
    static float refLatitude2 = 48.3092174f;
    static float refLongitude1 = 14.2855114f;
    static float refLongitude2 = 14.2841256f;

    static float refPosition1_x = -0.5063f;
    static float refPosition1_y = 0.2022f;
    static float refPosition2_x = -0.5243f;
    static float refPosition2_y = 0.2348f;

    static float lengthLat = refLatitude1 - refLatitude2;
    static float lengthLong = refLongitude1 - refLongitude2;
    static float lengthX = refPosition1_x - refPosition2_x;
    static float lengthY = refPosition1_y - refPosition2_y;


    public float ConvertRealXToGameX(float latitude)
    {
        Debug.Log("Latitude" + latitude);
        // Get distance to reference Point
        // divide by coordinate based length -> you got how many "parts" you are away
        // the "parts"-count is the same in game space so you only need to multiply by positionLength
        float distance = latitude - refLatitude1;
        float parts = distance / lengthLat;
        float positionX = parts * lengthX;

        return refPosition1_x + positionX;
    }

    public float ConvertRealYToGameY(float longitude)
    {
        Debug.Log("Longitude" + longitude);
        // Get distance to reference Point
        // divide by coordinate based length -> you got how many "parts" you are away
        // the "parts"-count is the same in game space so you only need to multiply by positionLength
        float distance = longitude - refLongitude1;
        float parts = distance / lengthLong;
        float positionY = parts * lengthY;

        return refPosition1_y + positionY;
    }
}
