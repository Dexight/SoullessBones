using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    private AttackSystem attackSystem;
    public GameObject SettingsPanel;
    private void Awake()
    {
       attackSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSystem>();
    }

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
        attackSystem.gameIsPaused = false;
        CheatBoxOff();
        SettingsPanel.SetActive(false);
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        attackSystem.gameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.instance.scenePassword = "menu";
        SceneManager.LoadScene(0);
    }

    //TODO: Реализовать переход из  меню паузы в меню опций
    public void ShowSettings() => SettingsPanel.SetActive(true);

    public void QuitGame()
    {
        Debug.Log("QUIT succesful");
        Application.Quit();
    }

    public void CheatBoxOn() => CheatBoxCanvas.SetActive(true);
    public void CheatBoxOff() => CheatBoxCanvas.SetActive(false);

}
