using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public GameObject CheatBoxCanvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.instance.scenePassword = "menu";
        SceneManager.LoadScene(0);
    }
    //TODO: ����������� ������� ��  ���� ����� � ���� �����
    public void Settings()
    {
    }

    public void QuitGame()
    {
        Debug.Log("QUIT succesful");
        Application.Quit();
    }

    public void CheatBoxOn() => CheatBoxCanvas.SetActive(true);
    public void CheatBoxOff() => CheatBoxCanvas.SetActive(false);

}
