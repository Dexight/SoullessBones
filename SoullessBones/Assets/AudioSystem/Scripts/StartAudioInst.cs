using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioInst : MonoBehaviour
{
    [SerializeField] GameObject instance;
    [SerializeField] string Tag;
    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag(Tag);
        if (obj == null)
        {
            GameObject a = Instantiate(instance, Vector3.zero, Quaternion.Euler(0, 0, 0));
            DontDestroyOnLoad(a);
        }
    }
}
