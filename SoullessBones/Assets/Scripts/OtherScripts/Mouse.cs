using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouse : MonoBehaviour
{
    PauseMenu pauseMenu;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(SceneManager.GetActiveScene().name == "Titrs")
            Cursor.lockState = CursorLockMode.None;
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
    }
    void Update()
    {
        if(pauseMenu)
        {
            if (pauseMenu.GameIsPaused)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}