using UnityEngine;
using UnityEngine.AI;

public class CrowAI : MonoBehaviour
{
    #region Variables
    private Transform _transform;
    private Animator _animator;
    private Transform _player;
    private Collider2D[] _playerCollider;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    [Header("Movement")]
    public float _normalSpeed = 4f;
    public float _slowlySpeed = 2f;
    public float _AttackDistance;
    public float _SlowlyDistance;
    [SerializeField] private CompositeCollider2D groundCollider;

    [Header("Attack")]
    public int damage;
    public float timeBtwDamage;

    [Header("Conditions")]
    [SerializeField] private bool facingRight;
    private bool Slowly = false;
    [Header("TimeStop")]
    TimeManager timeManager;
    #endregion

    void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _playerCollider = _player.GetComponents<Collider2D>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        groundCollider = GameObject.FindGameObjectWithTag("Ground").GetComponent<CompositeCollider2D>();

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), groundCollider); //игнорирование столкновения с землёй
        _rigidbody.gravityScale = 0; // обнуляем гравитацию после взлёта
    }

    private void Update()
    {
        Slowly = Vector2.Distance(_player.position, _transform.position) < _SlowlyDistance;
        Flip();
        if (_collider.IsTouching(_playerCollider[0]) || _collider.IsTouching(_playerCollider[1]) || _collider.IsTouching(_playerCollider[2]))
        {
            FindObjectOfType<HealthSystem>().TakeDamage(damage);
        }
    }

    void FixedUpdate()
    {
        if (!timeManager.TimeIsStopped)
        {
            _animator.SetTrigger("toFly");
            //замедляется при приближении к игроку
            if (!Slowly)
                NormalAttack();
            else
                SlowlyAttack();
        }
    }
    private void NormalAttack()
    {
        _transform.position = Vector2.MoveTowards(_transform.position, _player.position, _normalSpeed * Time.fixedDeltaTime);
        
    }

    private void SlowlyAttack()
    {
        _transform.position = Vector2.MoveTowards(_transform.position, _player.position, _slowlySpeed * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        if (_player.position.x < _transform.position.x && facingRight)
        {
            facingRight = !facingRight;
            FlipCur();
        }
        else if (_player.position.x > _transform.position.x && !facingRight)
        {
            facingRight = !facingRight;
            FlipCur();
        }
    }

    void FlipCur()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
