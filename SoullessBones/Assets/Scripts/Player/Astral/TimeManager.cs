using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool GrayBack;
    public bool TimeIsStopped;
    private GameObject Player;
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
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void ContinueTime()
    {
        TimeIsStopped = false;
        Player.GetComponent<AttackSystem>().inAstral = false;
        GrayBack = false;
        var objects = FindObjectsOfType<TimeBody>();  //Находит каждый объект с компонентом TimeBody
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<TimeBody>().ContinueTime(); //Продолжает время каждого объекта
        }
    }
    public void StopTime(bool isAstral)
    {
        GrayBack = isAstral;
        TimeIsStopped = true;
        Player.GetComponent<MovementController>()._CanMove = false;
        Player.GetComponent<AttackSystem>().inAstral = true;
    }
}
