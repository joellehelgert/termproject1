using UnityEngine;
using System.Collections;

public class HousingInformation
{
    public Districts district;
    public int[] FloorSpace;
    public int[] FlatsPerBuilding;
    public int[] RoomsPerFlat;
    public int FlatCount;
}

public enum RoomsPerFlat
{
    OneRoom,
    TwoRooms,
    ThreeRooms,
    FourRooms,
    FiveRooms,
    SixRooms,
    SevenRooms,
    MoreThanSeven,
}

public enum FloorSpace
{
    UpTo20,
    From21To40,
    From41To60,
    From61To80,
    From81To100,
    From101To130,
    MoreThan130,
    Unkown,
}

public enum FlatsPerBuilding
{
    From1To2,
    From3To4,
    From5To10,
    From11To15,
    From16To20,
    From21To50,
    MoreThan50,
    NoFlat // what does this mean? not for living? only one "House to live in"
}
