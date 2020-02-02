using System;
public class Activity
{
    public string name;
    public string area;
    public Districts district;
    public string urbanArea; // NEEDED ?
    public DetailedActivityTypes detailedActivityType; // maybe only this and not type?
    public string url;
    // nearly not filled
    // public string street;
    // public int zip;
    public float latitude; // Koordinate Nord
    public float longitude; // Koordinate Ost
}

public enum DetailedActivityTypes
{
    BeachVolleyball,
    SkatingTrack,
    Sportsground,
    Playground
}