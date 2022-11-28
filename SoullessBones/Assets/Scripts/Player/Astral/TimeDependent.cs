using UnityEngine;

public class TimeDependent : MonoBehaviour
{
    private TimeManager timemanager;
    private Rigidbody2D rb;
    private Vector3 savedVelocity;
    private float savedForce;
    private Animator _animator;

    public float TimeBeforeTimestop; //Время после появления объекта, пока на него не повлияет таймстоп (для снарядов и т. д.)
    private float TimerBeforeTimeStop;
    private bool CanBeAffected;
    private bool IsStopped;
    private bool _isAnimating = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        TimerBeforeTimeStop = TimeBeforeTimestop;
    }

    void Update()
    {
        if (!timemanager.TimeIsStopped && !_isAnimating)
        {
            _animator.enabled = true;
            _isAnimating = true;
        }

        if (timemanager.TimeIsStopped && _isAnimating)
        {
            _animator.enabled = false;
            _isAnimating = false;
        }

        TimerBeforeTimeStop -= Time.deltaTime; // минус 1%
        if (TimerBeforeTimeStop <= 0f)
        {
            CanBeAffected = true;
        }

        if (CanBeAffected && timemanager.TimeIsStopped && !IsStopped)
        {
            if (rb.velocity.magnitude >= 0f) //Если движется
            {
                savedVelocity = rb.velocity.normalized; //записывает направление движения
                savedForce = rb.velocity.magnitude; //записывает силы движения

                rb.isKinematic = true; //не подвергается силам
                rb.velocity = Vector3.zero; //останавливает движение rb
                IsStopped = true; // от зацикливания
            }
        }

    }
    public void ContinueTime()
    {
        rb.isKinematic = false;
        IsStopped = false;
        rb.velocity = savedVelocity * savedForce; //возвращает скорость после продолжения времени
    }
}
