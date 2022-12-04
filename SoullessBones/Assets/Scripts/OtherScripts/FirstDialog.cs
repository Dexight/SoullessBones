using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDialog : MonoBehaviour
{
    [SerializeField] private GameObject DialogManager;
    private void Awake()
    {
        if (GameManager.instance.scenePassword != "level_01_00")
        {
            Destroy(DialogManager);
            Destroy(gameObject);
        }
    }
}
