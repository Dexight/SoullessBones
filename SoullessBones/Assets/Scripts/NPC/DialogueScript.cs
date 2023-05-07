using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueScript : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJSON;

    //Временно сделал на клавишу "t", потом это можно будет изменить в настройках, если это будет реализовано :)
    private string Key = "t";

    private bool PlayerInRange;

    private GameObject player;

    private void Awake()
    {
        PlayerInRange = false;
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if (PlayerInRange)
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                return;
            }

            if (Input.GetKeyDown(Key))
            {
                DialogueManager.GetInstance().EnterDialogueMode(InkJSON);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") { PlayerInRange = true; }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") { PlayerInRange = false; }
    }
}

