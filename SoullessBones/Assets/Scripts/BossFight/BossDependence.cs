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

        //end battle music
        SoundVolumeController.SwitchToNormal();
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
