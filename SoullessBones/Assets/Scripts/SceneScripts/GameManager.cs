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
    public string scenePassword;//��������� ������, ����� ����� ��������� �� ������ �����

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
        DontDestroyOnLoad(gameObject);
        rb = Player.GetComponent<Rigidbody2D>();
    }
}
