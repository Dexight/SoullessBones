using System.Collections.Generic;

//contain information for each scene + player stats
public static class SceneStats
{
    public static bool doubleJump = false;
    public static bool wallJump = false;
    public static bool astral = false;
    public static bool distanceAttacks = false;

    public static float hp = 7;
    public static int tears = 0;

    public static bool isFull;
    public static bool isIncrementing;
    public static bool isEmpty;

    public static string curScene = "level_01";
    public static string EnterPassword = "level_01_00";
    public static string lastSave = "level_01_00";

    public static List<string> stats = new List<string> {};
}
