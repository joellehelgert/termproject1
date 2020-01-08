using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    // The ref point ONLY works it the image target is at (0/0/0)
    // -------- GEO - Coordinates References ----------
    static float refLatitude1 = 48.3073220f;
    static float refLatitude2 = 48.3092174f;
    static float refLongitude1 = 14.2855114f;
    static float refLongitude2 = 14.2841256f;

    static float refPosition1_y = 0.2793f;
    static float refPosition1_x = -0.1435f;
    static float refPosition2_x = -0.1536f;
    static float refPosition2_y = 0.3016f;

    static float lengthLat = refLatitude1 - refLatitude2;
    static float lengthLong = refLongitude1 - refLongitude2;
    static float lengthX = refPosition1_x - refPosition2_x;
    static float lengthY = refPosition1_y - refPosition2_y;

    // -------- Gauss-Küger - Coordinates References ----------
    static float refX1 = 72205;
    static float refX2 = 72142;
    static float refY1 = 352133;
    static float refY2 = 352067;

    static float refPosition3_x = -0.1222208f;
    static float refPosition3_y = -0.04761839f;
    static float refPosition4_x = -0.1190849f;
    static float refPosition4_y = -0.03377488f;

    static float lengthGaussX= refX1 - refX2;
    static float lengthGaussY = refY1 - refY2;
    static float lengthGameX = refPosition3_x - refPosition4_x;
    static float lengthGameY = refPosition3_y - refPosition4_y;


    public Vector2 ConvertGeoCoordinates(float latitude, float longitude)
    {
        float lat = ConvertRealXToGameX(latitude, refLatitude1, lengthLat, lengthX, refPosition1_x);
        float lon = ConvertRealYToGameY(longitude, refLongitude1, lengthLong, lengthY, refPosition1_y);

        return new Vector2(lat, lon);
       
    }

    public Vector2 ConvertGaussCoordinates(float x, float y)
    {
        float lat = ConvertRealXToGameX(y, refX1, lengthGaussX, lengthGameX, refPosition3_x);
        float lon = ConvertRealYToGameY(x, refY1, lengthGaussY, lengthGameY, refPosition3_y);

        return new Vector2(lat, lon);

    }

    public float ConvertRealXToGameX(float latitude, float refLatitude, float lengthLat, float lengthX, float refPosition)
    {
        float distance = latitude - refLatitude;
        float parts = distance / lengthLat;
        float positionX = parts * lengthX;

        return (refPosition + positionX);
    }

    public float ConvertRealYToGameY(float longitude, float refLongitude, float lengthLon, float lengthY, float refPosition)
    {
        float distance = longitude - refLongitude;
        float parts = distance / lengthLon;
        float positionY = parts * lengthY;

        return refPosition + positionY;
    }
}
