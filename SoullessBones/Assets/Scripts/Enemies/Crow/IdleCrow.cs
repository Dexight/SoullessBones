using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCrow : MonoBehaviour
{
    #region Variables
    private CrowAI CrowAI;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Transform _player;
    private BoxCollider2D _collider;
    private Collider2D[] _playerCollider;
    [SerializeField] private float _upSpeed;
    private bool Attacking = false;

    [Header("Attack Trigger Box")]
    [SerializeField] private BoxCollider2D aggroCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 _AttackBoxPosition;
    [SerializeField] private Vector2 aggroSize;
    public int maxhp;
    public int curhp;
    #endregion
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _playerCollider = _player.GetComponents<Collider2D>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        CrowAI = GetComponent<CrowAI>();

        curhp = GetComponent<Enemy>().health;
        maxhp = GetComponent<Enemy>().health;

        _rigidbody.gravityScale = 1;
        CrowAI.enabled = false; //отключаем скрипт "полёта"
    }

    void Update()
    {
        if (PlayerInSight() || PlayerIsTouch() || PlayerIsDamage()) //триггер при попадании игрока в поле зрения (нужно будет дополнить ещё триггер при получении урона или физического столкновения)
        {
            Attacking = true;
            SoundVolumeController.PlayMobEffect(0);
        }
        curhp = GetComponent<Enemy>().health;
    }

    private void FixedUpdate()
    {
        if(Attacking)
            StartCoroutine(GetUpCorutine()); //метод полёта (в конце удаляет этот скрипт)
    }

    private IEnumerator GetUpCorutine() 
    {
        yield return new WaitForSeconds(0.2f);
        _animator.SetTrigger("toFly");
        Vector3 Up = new Vector3(0f, 3f);
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Up, _upSpeed * Time.fixedDeltaTime);
        yield return new WaitForSeconds(0.2f);
        CrowAI.enabled = true;
        Destroy(_transform.GetComponent<IdleCrow>());
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(aggroCollider.bounds.center + transform.right * _AttackBoxPosition.x * transform.localScale.x + transform.up * _AttackBoxPosition.y, aggroCollider.bounds.size * aggroSize,
            0, Vector2.right, 0, playerLayer);
        return hit.collider != null;
    }

    private bool PlayerIsDamage()
    {
        if(curhp < maxhp)
        {
            return true;
        }
        return false;
    }

    private bool PlayerIsTouch()
    {
        if (_collider.IsTouching(_playerCollider[0]) || _collider.IsTouching(_playerCollider[1]) || _collider.IsTouching(_playerCollider[2]))
        {
            FindObjectOfType<HealthSystem>().TakeDamage(1);
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(aggroCollider.bounds.center + transform.right * _AttackBoxPosition.x * transform.localScale.x + transform.up * _AttackBoxPosition.y, aggroCollider.bounds.size * aggroSize);
    }
}
