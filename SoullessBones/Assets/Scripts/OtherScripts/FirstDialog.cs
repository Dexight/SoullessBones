using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDialog : MonoBehaviour
{
    [SerializeField] private GameObject DialogManager;
    [SerializeField] private GameObject dialogItem;
    private void Awake()
    {
        if (GameManager.instance.scenePassword != "level_01_00")
        {
            Destroy(dialogItem.GetComponent<StartDialogueScript>());
            Destroy(DialogManager);
            Destroy(gameObject);
        }
    }
}
