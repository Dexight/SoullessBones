using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//contain information for each scene + player stats
public static class SceneStats
{
    [Header("PLayer Stats")]
    //static private bool DoubleJumping = false;
    //static private bool WallJumping = false;
    //static private bool Astral = false;
    //static private int damage = 5;
    //static private hp
    [Header("level_01")]
    static public bool level01KeyTaken = false;
    static public bool level01LukeOpened = false;
    [Header("level_07")]
    static public bool level07KeyTaken = false;
    static public bool level07LukeOpened = false;
}
