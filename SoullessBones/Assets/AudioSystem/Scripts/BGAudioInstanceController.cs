using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudioInstanceController : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string Tag;
    [SerializeField] bool battle;
    bool inBattle;

    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag(Tag);
        if(obj != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.tag = Tag;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Update()
    {
        if (battle && !inBattle)
        {
            inBattle = true;
            SoundVolumeController.SwitchToBattle();
        }
        else if(!battle && inBattle)
        {
            inBattle = false;
            SoundVolumeController.SwitchToNormal();
        }
    }
}
