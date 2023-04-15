using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingGiveFly : MonoBehaviour
{
    [SerializeField] private GameObject TalkButton;
    [SerializeField] private GameObject TakeButton;
    [SerializeField] private DialogueScript dialogue;

    private bool flyani = true;

    void Awake()
    {
        TalkButton.SetActive(false);
        TakeButton.SetActive(false);
        if(SceneStats.stats.Contains("Fly"))
        {
            Destroy(TalkButton);
            Destroy(TakeButton);
            Destroy(dialogue);
            Destroy(this);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TalkButton)
        {
            TalkButton.SetActive(true);
            if (Input.GetKey(KeyCode.T))
            {
                //Dialogue start
                Destroy(TalkButton);
                dialogue.Update();
                Destroy(dialogue);
            }
        }
        else if (TakeButton && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (flyani)
            {
                GetComponent<Animator>().SetTrigger("fly");
                flyani = false;
            }

            TakeButton.SetActive(true);
            
            if (Input.GetKey(KeyCode.F))
            {
                GetComponent<Animator>().SetTrigger("take");
                GameManager.instance.Interface.GetComponentInChildren<FlyUI>().Taked();
                Destroy(TakeButton);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TalkButton)
        {
            TalkButton.SetActive(false);
        }
        else if (TakeButton)
            TakeButton.SetActive(false);
        else
            Destroy(this);  
    }
}
