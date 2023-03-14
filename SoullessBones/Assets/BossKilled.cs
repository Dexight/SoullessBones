using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilled : MonoBehaviour
{
    public void SaveAndStop()
    {
        //TODO SAVE 
        //

        Destroy(GetComponentInParent<BossDoor>().gameObject);
    }
    public void help()
    {

    }
}
