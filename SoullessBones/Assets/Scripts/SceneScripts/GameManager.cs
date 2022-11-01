using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Variables

    public static GameManager instance;
    public GameObject Player;
    public GameObject Interface;
    public string scenePassword;//сохраняет строку, когда игрок переходит на другую сцену

    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(Interface);
    }
}
