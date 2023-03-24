using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarDoor : MonoBehaviour
{
    private void Awake()
    {
        if (SceneStats.stats.Contains("Altar"))
            Destroy(gameObject);
    }
}
