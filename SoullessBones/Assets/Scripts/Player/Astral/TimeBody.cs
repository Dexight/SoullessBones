using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public float TimeBeforeAffected; //Время после появления объекта, пока на него не повлияет таймстоп (для снарядов и т. д.)
    private TimeManager timemanager;
    private Rigidbody2D rb;
    private Vector3 recordedVelocity;
    private Vector3 recordedTransform;
    private float recordedMagnitude;

    private float TimeBeforeAffectedTimer;
    private bool CanBeAffected;
    private bool IsStopped;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        TimeBeforeAffectedTimer = TimeBeforeAffected;
    }

    void Update()
    {
        TimeBeforeAffectedTimer -= Time.deltaTime; // минус 1%
        if (TimeBeforeAffectedTimer <= 0f)
        {
            CanBeAffected = true;
        }

        if (CanBeAffected && timemanager.TimeIsStopped && !IsStopped)
        {
            if (rb.velocity.magnitude >= 0f) //Если движется
            {
                recordedVelocity = rb.velocity.normalized; //записывает направление движения
                recordedMagnitude = rb.velocity.magnitude; //записывает магнетуду движения

                rb.velocity = Vector3.zero; //останавливает движение rb
                rb.isKinematic = true; //не подвергается силам
                IsStopped = true; // от зацикливания
            }
        }

    }
    public void ContinueTime()
    {
        rb.isKinematic = false;
        IsStopped = false;
        rb.velocity = recordedVelocity * recordedMagnitude; //возвращает скорость после продолжения времени
    }
}
