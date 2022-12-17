using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudioInstanceController : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string Tag;

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
}
