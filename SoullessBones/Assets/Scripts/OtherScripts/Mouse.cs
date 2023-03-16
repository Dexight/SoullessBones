using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    PauseMenu pauseMenu;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
    }
    void Update()
    {
        if (pauseMenu.GameIsPaused)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}