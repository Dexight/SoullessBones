using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsPanel;
    private void Awake()
    {
        if (GameManager.instance)
        {
            if (GameManager.instance.inMenu) //при переходе из паузы в меню удаляет игрока и интерфейс
            {
                Destroy(GameManager.instance.Player);
                Destroy(GameManager.instance.Interface);
                Destroy(GameManager.instance.timeManager);
                Destroy(GameObject.FindGameObjectWithTag("GameManager"));
            }
        }
    }

    //TODO: Реализовать загрузку игры с последнего чекпоинта
    public void Continuegame()
    {
        SceneStatsJsonSerializer.LoadSceneStatsFromJson();
        SceneManager.LoadScene(SceneStats.curScene);
    }

    public void PlayGame()
    {
        SceneStats.stats = new List<string>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }

    public void ShowSettings()
    {
        SettingsPanel.SetActive(true);
    }
}
