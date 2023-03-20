using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CultistMove bossScript;
    [SerializeField] Spawner Spawner;
    public void OpenDoor()
    {
        anim.SetTrigger("Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        if (bossScript)
        {
            bossScript.enabled = true;
            bossScript.GetComponent<Animator>().enabled= true;
        }
        if(Spawner)
            Spawner.enabled = true;
        anim.enabled = true;
    }
}
