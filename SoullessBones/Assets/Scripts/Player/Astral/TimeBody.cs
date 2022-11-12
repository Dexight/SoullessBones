using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public float TimeBeforeAffected; //����� ����� ��������� �������, ���� �� ���� �� �������� �������� (��� �������� � �. �.)
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
        TimeBeforeAffectedTimer -= Time.deltaTime; // ����� 1%
        if (TimeBeforeAffectedTimer <= 0f)
        {
            CanBeAffected = true;
        }

        if (CanBeAffected && timemanager.TimeIsStopped && !IsStopped)
        {
            if (rb.velocity.magnitude >= 0f) //���� ��������
            {
                recordedVelocity = rb.velocity.normalized; //���������� ����������� ��������
                recordedMagnitude = rb.velocity.magnitude; //���������� ��������� ��������

                rb.isKinematic = true; //�� ������������ �����
                rb.velocity = Vector3.zero; //������������� �������� rb
                IsStopped = true; // �� ������������
            }
        }

    }
    public void ContinueTime()
    {
        rb.isKinematic = false;
        IsStopped = false;
        rb.velocity = recordedVelocity * recordedMagnitude; //���������� �������� ����� ����������� �������
    }
}
