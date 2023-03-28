using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BossKilled : MonoBehaviour
{
    private bool openTrigger = false;
    public bool isBossAlive = true;
    [SerializeField] private BossDoor bossDoor;
    [SerializeField] private BossDoor bossDoor2;

    [SerializeField] private BossDependence bossDependence;
    [SerializeField] private GameObject CultistObject;
    [SerializeField] private GameObject SpiderObject;

    private void Awake()
    {
        bossDependence = GetComponent<BossDependence>();
        if (SceneStats.stats.Contains("Cultist") && CultistObject)
        {
            Destroy(bossDoor.gameObject);
            if(bossDoor2)
                Destroy(bossDoor2.gameObject);
            Destroy(CultistObject);
            transform.position = transform.position + new Vector3(0, -2, 0);
        }

        if (SceneStats.stats.Contains("Spider") && SpiderObject)
        {
            Destroy(bossDoor.gameObject);
            Destroy(bossDoor2.gameObject);
            Destroy(SpiderObject);
        }
    }

    private void Update()
    {
        if(openTrigger)
        {
            var anim = GetComponent<Animator>();
            if(anim)
                GetComponent<Animator>().enabled = true;
            openTrigger = false;
        }

        if (!isBossAlive)
        {
            isBossAlive = true;
            bossDependence.DoAll();
            if(SpiderObject)
            {
                bossDependence.editItems();
            }
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
