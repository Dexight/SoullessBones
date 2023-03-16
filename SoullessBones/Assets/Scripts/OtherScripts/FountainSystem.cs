using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FountainSystem : MonoBehaviour
{
    [SerializeField] private GameObject saveText;
    private bool isPlayer = false;

    public static void Save()
    {
        GameManager.instance.lastSave = GameManager.instance.currentScene;
        GameManager.instance.Save();
        SceneStats.EnterPassword = "save";
        SceneStatsJsonSerializer.SaveSceneStatsToJson();
        Debug.Log("Fountain Save!!");
    }

    private void Awake()
    {
        saveText.SetActive(false);
    }

    void Update()
    {
        if (MovementController.instance._CanMove && Input.GetKeyDown(KeyCode.F) && isPlayer)
        {
            Save();
            GameManager.instance.scenePassword = "save";
            SceneLoader.instance.FadeTo(SceneStats.curScene, true, true, false);
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        isPlayer = collision.gameObject.tag == "Player";
        if(isPlayer)
        {
            saveText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        saveText.SetActive(false);
    }
}
