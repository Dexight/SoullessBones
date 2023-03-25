using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CultistMove bossCultistScript;
    [SerializeField] Spawner Spawner;
    [SerializeField] OnStart spiderBoss;

    public void OpenDoor()
    {
        anim.SetTrigger("Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        if (bossCultistScript)
        {
            //start battle music
            if (!bossCultistScript.enabled) SoundVolumeController.SwitchToBattle();
            bossCultistScript.enabled = true;
            bossCultistScript.GetComponent<Animator>().enabled= true;
        }

        if (Spawner)
        {
            Spawner.enabled = true;
            foreach (var i in Spawner.spawnPoints)
            {
                i.GetChild(0).gameObject.SetActive(true);
            }
        }

        if (spiderBoss)
        {
            //start battle music
            if (!spiderBoss.enabled) SoundVolumeController.SwitchToBattle();
            spiderBoss.enabled = true;
        }

        anim.enabled = true;
    }
}
