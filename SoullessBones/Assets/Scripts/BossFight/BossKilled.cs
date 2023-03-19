using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BossKilled : MonoBehaviour
{
    private bool openTrigger = false;
    public bool isBossAlive = true;
    public string bossName;
    [SerializeField] private BossDoor bossDoor;
    [SerializeField] private BossDependence bossDependence;
    [SerializeField] private GameObject BossObject;
    private void Awake()
    {
        bossDependence = GetComponent<BossDependence>();
        if (SceneStats.stats.Contains("Cultist"))
        {
            Destroy(bossDoor.gameObject);
            Destroy(BossObject);
            transform.position = transform.position + new Vector3(0, -2, 0);    
        }
    }

    private void Update()
    {
        if(openTrigger)
        {
            GetComponent<Animator>().enabled = true;
            openTrigger = false;
        }

        if (!isBossAlive)
        {
            isBossAlive = true;
            SceneStats.stats.Add(bossName);//SAVE IT
            bossDependence.DoAll();
        }
    }
    
    public void ForPlatform()
    {
        openTrigger = true;
    }

    public void DeleteAnimator()
    {
        Destroy(GetComponent<Animator>());
    }
}
