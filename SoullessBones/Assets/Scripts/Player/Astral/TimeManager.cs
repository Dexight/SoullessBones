using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class TimeManager : MonoBehaviour
{
    public bool TimeIsStopped;
    private static TimeManager instance; //только один на сцене (синглтон)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }
    public void ContinueTime()
    {
        TimeIsStopped = false;

        var objects = FindObjectsOfType<TimeBody>();  //Находит каждый объект с компонентом TimeBody
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<TimeBody>().ContinueTime(); //Продолжает время каждого объекта
        }
    }
    public void StopTime()
    {
        TimeIsStopped = true;
    }
}
