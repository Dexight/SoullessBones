using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogItem;
    [SerializeField] private GameObject elf;
    private void Start()
    {
        if (GameManager.instance.enterPassword != "level_01_00")
        {
            Destroy(dialogItem.GetComponent<StartDialogueScript>());
            // Destroy(DialogManager);
            // Destroy(gameObject);
            // Debug.LogWarning("1");
        }
        else
        {
            Destroy(elf.GetComponent<DialogueScript>());
            // Debug.LogWarning("2");
        }
    }
}
