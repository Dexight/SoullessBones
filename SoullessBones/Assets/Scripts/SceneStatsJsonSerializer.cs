using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//contain information for each scene + player stats
public static class SceneStats
{
    public static bool doubleJump = false;
    public static bool wallJump = false;
    public static bool astral = false;
    public static bool distanceAttacks = false;

    public static float hp = 7;
    public static int tears = 0;
    public static int keycounter = 0;
    public static int keycount = 0;
    public static Dictionary<Key.KeyType, int> keyList = new Dictionary<Key.KeyType, int> { { Key.KeyType.Gold, 0 }, { Key.KeyType.Fly, 0 }};

    public static bool isFull = false;
    public static bool isIncrementing = true;
    public static bool isEmpty = true;

    public static string curScene = "Hub Scene";
    public static string EnterPassword = "start";
    public static string lastSave = "start";

    public static List<string> stats = new List<string> { };

    public static void ResetData()
    {
      doubleJump = false;
      wallJump = false;
      astral = false;
      distanceAttacks = false;

      hp = 7;
      tears = 0;
      keycounter = 0;
      keycount = 0;
      keyList = new Dictionary<Key.KeyType, int> { { Key.KeyType.Gold, 0 }, { Key.KeyType.Fly, 0 }};

      isFull = false;
      isIncrementing = true;
      isEmpty = true;

      curScene = "Hub Scene";
      EnterPassword = "start";
      lastSave = "start";

      stats = new List<string> { };
    }
}

//класс-обёртка для статического класса SceneStats(для возможности сериализации/десериализации)
[System.Serializable]
public class SceneStatsWrapper
{
    public bool doubleJump;
    public bool wallJump;
    public bool astral;
    public bool distanceAttacks;

    public float hp;
    public int tears;
    public int keycounter;
    public Dictionary<Key.KeyType, int> keyList;

    public bool isFull;
    public bool isIncrementing;
    public bool isEmpty;

    public string curScene;
    public string EnterPassword;
    public string lastSave;

    public List<string> stats;

    public SceneStatsWrapper()
    {
        doubleJump = SceneStats.doubleJump;
        wallJump = SceneStats.wallJump;
        astral = SceneStats.astral;
        distanceAttacks = SceneStats.distanceAttacks;

        hp = SceneStats.hp;
        tears = SceneStats.tears;
        keycounter = SceneStats.keycounter;
        keyList = SceneStats.keyList;

        isFull = SceneStats.isFull;
        isIncrementing = SceneStats.isIncrementing;
        isEmpty = SceneStats.isEmpty;

        curScene = SceneStats.curScene;
        EnterPassword = SceneStats.EnterPassword;
        lastSave = SceneStats.lastSave;

        stats = SceneStats.stats;
    }

    public void UpdateSceneStats()
    {
        SceneStats.doubleJump = doubleJump;
        SceneStats.wallJump = wallJump;
        SceneStats.astral = astral;
        SceneStats.distanceAttacks = distanceAttacks;

        SceneStats.hp = hp;
        SceneStats.tears = tears;
        SceneStats.keycounter = keycounter;
        SceneStats.keyList = keyList;

        SceneStats.isFull = isFull;
        SceneStats.isIncrementing = isIncrementing;
        SceneStats.isEmpty = isEmpty;

        SceneStats.curScene = curScene;
        SceneStats.EnterPassword = EnterPassword;
        SceneStats.lastSave = lastSave;

        SceneStats.stats = stats;
    }
}

public static class SceneStatsJsonSerializer
{
    private static readonly string filePath = Application.persistentDataPath + "/save.json";

    public static void SaveSceneStatsToJson()
    {
        SceneStatsWrapper wrapper = new SceneStatsWrapper();

        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(filePath, json);
        Debug.Log("Save in : " + filePath);
        //Debug.Log("save keycount = " + SceneStats.keyList[Key.KeyType.Gold]);         //проверка на запись в сейв
    }

    public static void LoadSceneStatsFromJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SceneStatsWrapper wrapper = JsonUtility.FromJson<SceneStatsWrapper>(json);
            wrapper.UpdateSceneStats();                                              //загрузка в SceneStats из сейва
            //Debug.Log("load keycount = " + SceneStats.keyList[Key.KeyType.Gold]); //проверка на выгрузку из сейв
        }
    }


    public static void DeleteSave()
    {
        string savePath = Application.persistentDataPath + "/save.json";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
}