using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] Animator anim;

    static void OpenDoor(Animator anim)
    {
        anim.SetTrigger("Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        anim.enabled = true;
    }
}
