using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueScript : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJSON;

    private bool PlayerInRange;

    private GameObject player;

    private bool SelfDialogueEnd;

    private void Awake()
    {
        PlayerInRange = false;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (PlayerInRange & !DialogueManager.GetInstance().dialogueIsPlaying & !SelfDialogueEnd)
        {
            DialogueManager.GetInstance().EnterDialogueMode(InkJSON);
            SelfDialogueEnd = true;
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

