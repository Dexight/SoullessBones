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
            if (GameManager.instance.scenePassword == "menu") //��� �������� �� ����� � ���� ������� ������ � ���������
            {
                Destroy(GameManager.instance.Player);
                Destroy(GameManager.instance.Interface);
                Destroy(GameManager.instance.timeManager);
                Destroy(GameManager.instance);
                //��������� ���������� �� ��������� (TODO � ������� ������� ���������, �.�. � ������� ����-����� "������" ������.)
                SceneStats.level01LukeOpened = false;
                SceneStats.level01KeyTaken = false;
                SceneStats.level07LukeOpened = false;
                SceneStats.level07KeyTaken = false;
            }
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("���� ���������");
        Application.Quit();
    }
}
