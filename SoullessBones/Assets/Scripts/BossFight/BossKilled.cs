using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BossKilled : MonoBehaviour
{
    [SerializeField] private BossDoor bossDoor;
    private bool openTrigger = false;
    public bool isBossAlive = true;

    private void Update()
    {
        if(openTrigger)
        {
            GetComponent<Animator>().enabled = true;
            openTrigger = false;
        }
        if (!isBossAlive)
        {
            bossDoor.OpenDoor();
            openTrigger = true;
            isBossAlive = true;
            //TODO SAVE

        }
    }

    public void SaveAndStop()
    {
        //TODO SAVE
        Destroy(GetComponentInParent<BossDoor>().gameObject);
    }
    
    public void ForPlatform()
    {
        openTrigger = true;
    }

    public void DeleteAnimator()
    {
        Destroy(GetComponent<Animator>());
        //TODO SAVE
    }
}
