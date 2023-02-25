using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{

    public GameObject SettingsPanel;
    public Button CloseButton;

    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void CloseSettings()
    {
        SettingsPanel.SetActive(false); 
    }
}
