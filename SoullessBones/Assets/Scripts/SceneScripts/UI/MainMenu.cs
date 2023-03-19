using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsPanel;
    [SerializeField] private GameObject continueButton;
    private void Awake()
    {
        //SceneStatsJsonSerializer.DeleteSave();
        continueButton.SetActive(File.Exists(Application.persistentDataPath + "/save.json"));

        if (GameManager.instance)
        {
            if (GameManager.instance.inMenu) //��� �������� �� ����� � ���� ������� ������ � ���������
            {
                Destroy(GameManager.instance.Player);
                Destroy(GameManager.instance.Interface);
                Destroy(GameManager.instance.timeManager);
                Destroy(GameObject.FindGameObjectWithTag("GameManager"));
            }
        }
    }

    //TODO: ����������� �������� ���� � ���������� ���������
    public void Continuegame()
    {
        SceneStatsJsonSerializer.LoadSceneStatsFromJson();
        Debug.Log("CurScene: " + SceneStats.curScene);
        SceneManager.LoadScene(SceneStats.curScene);
    }

    public void PlayGame()
    {
        SceneStats.ResetData();
        SceneStatsJsonSerializer.SaveSceneStatsToJson();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        SceneStatsJsonSerializer.SaveSceneStatsToJson();
        Debug.Log("���� ����������� � ���������");
        Application.Quit();
    }

    public void ShowSettings()
    {
        SettingsPanel.SetActive(true);
    }
}
