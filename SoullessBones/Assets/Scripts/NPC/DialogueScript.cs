using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueScript : MonoBehaviour
    {
        [Header("Cue")]
        [SerializeField] private GameObject Cue;

        [Header("Ink JSON")]
        [SerializeField] private TextAsset InkJSON;

        //Временно сделал на клавишу "t", потом это можно будет изменить в настройках, если это будет реализовано :)
        private string Key = "t";

        private bool PlayerInRange;

        private GameObject player;

        private void Awake()
        {
            PlayerInRange = false;
            Cue.SetActive(false);
            player = GameObject.FindWithTag("Player");
        }

        private void Update() 
        {
            if (PlayerInRange)
            { 
                if (DialogueManager.GetInstance().dialogueIsPlaying) 
                {
                Cue.SetActive(false);
                return; 
                }

            if (Input.GetKeyDown(Key) && Cue.activeSelf)
            {
                DialogueManager.GetInstance().EnterDialogueMode(InkJSON);
            }

            Cue.SetActive(true);

            Cue.transform.position = player.transform.position + new Vector3(0, 0.6f, 0);

        }
            else 
            { 
                Cue.SetActive(false);
                DialogueManager.GetInstance().ExitDialogueMode();
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

