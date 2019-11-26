using System;
public class Activity
{
    public string name;
    public ActivityType type;
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


public enum ActivityType
{
    Playground,
    Sportsground,
}


public enum DetailedActivityTypes
{
    Fitness,
    BeachVolleyball,
    SkatingTrack,
    Streetball,
    FunCourt,
    PlaygroundForTeens, // 12 - 18
    PlaygroundForKids, // 0 - 6
    PlaygroundForTeensAndKids, // 6 - 18
}