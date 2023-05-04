using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    TimeManager timeManager;
    public GameObject PauseMenuUI;
    private AttackSystem attackSystem;
    public GameObject SettingsPanel;
    private void Awake()
    {
        attackSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSystem>();
    }

    private void Start()
    {
        timeManager = GameManager.instance.timeManager.GetComponent<TimeManager>();
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
        //SettingsPanel.SetActive(false);

        //audio
        if(!timeManager.TimeIsStopped)
            GameObject.FindWithTag("Player").GetComponent<MovementController>()._CanMove = true;
        SoundVolumeController.PauseMusic(false);
    }
    void Pause()
    {
        //audio
        var a = GameObject.FindWithTag("Player").GetComponent<MovementController>();
        a._CanMove = false;
        a.alreadyWalking = false;
        SoundVolumeController.PlayLongEffect(false, 0);
        SoundVolumeController.PauseMusic(true);

        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        attackSystem.gameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        SoundVolumeController.LoadToScene("Menu");
        Time.timeScale = 1f;
        GameManager.instance.inMenu = true;
        GameManager.instance.Save();
        SceneStatsJsonSerializer.SaveSceneStatsToJson();
        SceneManager.LoadScene(0);
    }

    //TODO: Реализовать переход из  меню паузы в меню опций
    public void ShowSettings() 
    {
        SettingsPanel.SetActive(true);
        SoundVolumeController.InitSliders();
    }

    public void QuitGame()
    {
        Debug.Log("Игра сохранилась и закрылась");
        GameManager.instance.Save();
        SceneStatsJsonSerializer.SaveSceneStatsToJson();
        Application.Quit();
    }

    public void CheatBoxOn() => CheatBoxCanvas.SetActive(true);
    public void CheatBoxOff() => CheatBoxCanvas.SetActive(false);

}
