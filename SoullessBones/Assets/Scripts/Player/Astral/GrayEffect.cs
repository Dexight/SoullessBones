using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayEffect : MonoBehaviour
{
    SpriteRenderer sprite;
    TimeManager timeManager;
    private void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    void Update()
    {
        sprite.enabled = timeManager.GrayBack;
    }
}
