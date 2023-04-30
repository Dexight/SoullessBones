using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    Animator anim;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject eatenAltar;

    [SerializeField] private GameObject tutor;
    void Awake()
    {
        anim = GetComponent<Animator>();
        if (SceneStats.stats.Contains("Altar"))
            eaten();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneStats.stats.Contains("Fly") && !SceneStats.stats.Contains("Altar"))
        {
            anim.enabled = true;
            SceneStats.stats.Add("Altar");
            GameManager.instance.Interface.GetComponentInChildren<FlyUI>().Give();
            SoundVolumeController.PlaySoundEffect2(5);
        }
        else
        {
            if(!SceneStats.stats.Contains("Altar"))
                tutor.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        tutor.SetActive(false);
    }

    public void eaten()
    {
        Destroy(head);
        Destroy(GetComponent<BoxCollider>());
        Destroy(body);
        Destroy(anim);
        eatenAltar.SetActive(true);
        Destroy(this);
    }
}