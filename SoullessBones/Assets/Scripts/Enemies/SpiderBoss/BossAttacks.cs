using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public void enableMovement()
    {
        GetComponent<SpiderMovement>().enabled = true;
    }
}
