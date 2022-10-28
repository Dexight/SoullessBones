using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    #region Singleton Variables

    public static Singleton instance;
    public GameObject CMcamera;
    public GameObject playerCamera;
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
        DontDestroyOnLoad(CMcamera);
        DontDestroyOnLoad(playerCamera);
        DontDestroyOnLoad(Interface);
    }
}
