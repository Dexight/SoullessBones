using UnityEngine;

public class TimeDependent : MonoBehaviour
{
    private TimeManager timemanager;
    private Rigidbody2D rb;
    private Vector3 savedVelocity;
    private float savedForce;
    private Animator _animator;

    public float TimeBeforeTimestop; //����� ����� ��������� �������, ���� �� ���� �� �������� �������� (��� �������� � �. �.)
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

        TimerBeforeTimeStop -= Time.deltaTime; // ����� 1%
        if (TimerBeforeTimeStop <= 0f)
        {
            CanBeAffected = true;
        }

        if (CanBeAffected && timemanager.TimeIsStopped && !IsStopped)
        {
            if (rb.velocity.magnitude >= 0f) //���� ��������
            {
                savedVelocity = rb.velocity.normalized; //���������� ����������� ��������
                savedForce = rb.velocity.magnitude; //���������� ���� ��������

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
        rb.velocity = savedVelocity * savedForce; //���������� �������� ����� ����������� �������
    }
}
