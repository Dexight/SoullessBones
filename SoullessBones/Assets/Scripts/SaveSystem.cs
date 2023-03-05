using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject saveText;
    private bool isPlayer = false;

    void Save()
    {

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
            Debug.Log("Saved");
            SceneLoader.instance.FadeTo(GameManager.instance.currentScene, true);
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
