using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterDeath : MonoBehaviour
{
    void Update()
    {
        
    }

    static public void Death()
    {
        Destroy(GameObject.FindGameObjectWithTag("Interface"));
        Destroy(GameObject.FindGameObjectWithTag("BGMusic"));
        Destroy(GameObject.FindGameObjectWithTag("TimeManager"));
        Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("Menu");
    }
}
