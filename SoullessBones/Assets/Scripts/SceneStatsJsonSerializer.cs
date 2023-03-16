using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


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
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public static void LoadSceneStatsFromJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SceneStatsWrapper wrapper = JsonUtility.FromJson<SceneStatsWrapper>(json);
            wrapper.UpdateSceneStats();                             //загрузка в SceneStats из сейва
        }
    }
}