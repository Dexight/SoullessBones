using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class TimeManager : MonoBehaviour
{
    public bool TimeIsStopped;

    public void ContinueTime()
    {
        TimeIsStopped = false;

        var objects = FindObjectsOfType<TimeBody>();  //Находит каждый оюъект с компонентом TimeBody
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
