using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDependence : MonoBehaviour
{
    [SerializeField] GameObject spawnedItem;
    public BossDoor bossDoor;
    [SerializeField] private BossKilled bossKilled;

    public void DoAll()
    {
        spawnItems();
    }

    private void spawnItems()
    {
        spawnedItem.SetActive(true);
    }

    public void editItems()
    {
        if (bossDoor)
        {
            bossDoor.OpenDoor();
            bossKilled.ForPlatform();
        }
        else bossKilled.ForPlatform();
    }
}
