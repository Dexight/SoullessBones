using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDependence : MonoBehaviour
{
    [SerializeField] GameObject spawnedItem;
    public BossDoor bossDoor;
    public BossDoor bossDoor2;
    [SerializeField] private BossKilled bossKilled;

    public void DoAll()
    {
        spawnItems();
    }

    private void spawnItems()
    {
        if(spawnedItem)
            spawnedItem.SetActive(true);

        //end battle music
        SoundVolumeController.SwitchToNormal();
    }

    public void editItems()
    {
        if (bossDoor)
        {
            bossDoor.OpenDoor();
            if (bossDoor2)
                bossDoor2.OpenDoor();
            bossKilled.ForPlatform();
        }
        else bossKilled.ForPlatform();
    }
}
