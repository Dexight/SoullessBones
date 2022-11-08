using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Singleton Variables

    public static GameManager instance;
    public GameObject Player;
    public Rigidbody2D rb;
    public GameObject Interface;
    public GameObject timeManager;
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
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(Interface);
        DontDestroyOnLoad(timeManager);
        DontDestroyOnLoad(gameObject);
        rb = Player.GetComponent<Rigidbody2D>();
    }
}
