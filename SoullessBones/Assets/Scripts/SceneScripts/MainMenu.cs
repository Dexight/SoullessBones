using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.instance)
        {
            if (GameManager.instance.scenePassword == "menu") //при переходе из паузы в меню удаляет игрока и интерфейс
            {
                Destroy(GameManager.instance.Player);
                Destroy(GameManager.instance.Interface);
            }
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}
