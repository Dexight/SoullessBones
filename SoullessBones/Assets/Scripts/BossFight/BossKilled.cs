using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilled : MonoBehaviour
{
    public bool openTrigger = false;

    private void Update()
    {
        if(openTrigger)
        {
            GetComponent<Animator>().enabled = true;
            openTrigger = false;
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
