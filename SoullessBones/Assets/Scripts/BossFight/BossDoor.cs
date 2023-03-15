using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CultistMove bossScript;
    public void OpenDoor()
    {
        anim.SetTrigger("Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        bossScript.enabled = true;
        anim.enabled = true;
    }
}
