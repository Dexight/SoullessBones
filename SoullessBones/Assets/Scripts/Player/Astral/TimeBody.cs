using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public float TimeBeforeAffected; //Время после появления объекта, пока на него не повлияет таймстоп (для снарядов и т. д.)
    private TimeManager timemanager;
    //private MovementController movementController;
    private Rigidbody2D rb;
    private Vector3 recordedVelocity;
    private float recordedMagnitude;
    private Animator _animator;

    private float TimeBeforeAffectedTimer;
    private bool CanBeAffected;
    private bool IsStopped;
    private bool _isAnimating = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        //if(GetComponent<MovementController>())
            //movementController = GetComponent<MovementController>();
        TimeBeforeAffectedTimer = TimeBeforeAffected;
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
        rb.velocity = recordedVelocity * recordedMagnitude; //возвращает скорость после продолжения времени
    }
}
