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
    [SerializeField] private float _upSpeed;
    private bool Attacking = false;

    [Header("Attack Trigger Box")]
    [SerializeField] private BoxCollider2D aggroCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 _AttackBoxPosition;
    [SerializeField] private Vector2 aggroSize;
    #endregion
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        CrowAI = GetComponent<CrowAI>();

        _rigidbody.gravityScale = 1;
        CrowAI.enabled = false; //��������� ������ "�����"
    }

    void Update()
    {
        if (PlayerInSight()) //������� ��� ��������� ������ � ���� ������ (����� ����� ��������� ��� ������� ��� ��������� ����� ��� ����������� ������������)
        {
            Attacking = true;
        }
    }

    private void FixedUpdate()
    {
        if(Attacking)
            StartCoroutine(GetUpCorutine()); //����� ����� (� ����� ������� ���� ������)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(aggroCollider.bounds.center + transform.right * _AttackBoxPosition.x * transform.localScale.x + transform.up * _AttackBoxPosition.y, aggroCollider.bounds.size * aggroSize);
    }
}
