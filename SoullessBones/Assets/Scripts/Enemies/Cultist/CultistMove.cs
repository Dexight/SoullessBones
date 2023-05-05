using UnityEngine;

public class CultistMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private LayerMask playerLayer;
    private bool facingRight = true;
    public bool isAttack = false;
    private int Direction;
    private int currentDirection;

    [SerializeField] Transform player;
    [SerializeField] float Speed;
    [SerializeField] float checkDistance;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask layer;
    [SerializeField] RectTransform healthBar;

    #region Aggro Zone Variables
    [Header("Aggro Zone")]
    [SerializeField] private CapsuleCollider2D aggroCollider;
    [SerializeField] private Vector2 aggroBoxPosition;
    [SerializeField] private Vector2 aggroSize;
    static Vector2 staticAggroBoxPosition;
    static Vector2 staticAggroSize;
    #endregion

    #region Attack Zone Variables
    [SerializeField] private Vector2 attackBoxPosition;
    [SerializeField] private Vector2 attackSize;
    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        staticAggroBoxPosition = aggroBoxPosition;
        staticAggroSize = aggroSize;
    }

    void Update()
    {
        Direction = facingRight ? 1 : -1;
        currentDirection = Direction;

        if (isAttack)
        {
            aggroBoxPosition = attackBoxPosition;
            aggroSize = attackSize;
            currentDirection = transform.position.x < player.position.x ? 1 : -1;
        }
        else
        {
            aggroBoxPosition = staticAggroBoxPosition;
            aggroSize = staticAggroSize;
        }

        if (Direction != currentDirection)
            Flip();

        if (PlayerInZone(aggroCollider, aggroBoxPosition, aggroSize))
        {
            if (PlayerInSight())
            {
                isAttack = true;
                anim.SetBool("Attack", true);
            }
            else
            {
                isAttack = false;
                anim.SetBool("Attack", false);
            }
        }
        else
        {
            isAttack = false;
            anim.SetBool("Attack", false);
        }
        Patrolling();
    }

    #region Idle Move
    private void Patrolling()
    {
        float curSpeed = Speed;
        if (isAttack)
            curSpeed = 0;
        rb.velocity = new Vector2(curSpeed * Direction, 0);

        if (isWallTouch() || !isGrounded()) // поворот
            Flip();
    }

    private void Flip()
    {
        anim.SetTrigger("Flip");
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        healthBar.rotation = !facingRight ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }
    #endregion

    #region In Zone and In Sight
    private bool PlayerInZone(CapsuleCollider2D collider, Vector2 position, Vector2 size) //проверяет в зоне атаки ли игрок
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center + transform.right * position.x * transform.localScale.x + transform.up * position.y, aggroCollider.bounds.size * size,
            0, Vector2.right, 0, playerLayer);
        return hit.collider != null;
    }

    private bool PlayerInSight() => !Physics2D.Linecast(player.position, transform.position, layer);  //проверяет, видит ли напрямую игрока враг
    #endregion

    #region Other Functions
    private bool isGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, layer);

    private bool isWallTouch() => Physics2D.Raycast(wallCheck.position, Vector2.right, checkDistance, layer);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (isWallTouch())
            Gizmos.color = Color.blue;
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + checkDistance, wallCheck.position.y));

        Gizmos.color = Color.yellow;
        if (isGrounded())
            Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - checkDistance));

        //Зона патрулирования
        Gizmos.color = Color.red;
        if(!isAttack)
            Gizmos.DrawWireCube(aggroCollider.bounds.center + transform.right * aggroBoxPosition.x * transform.localScale.x + transform.up * aggroBoxPosition.y, aggroCollider.bounds.size * aggroSize);
        else
            Gizmos.DrawWireCube(aggroCollider.bounds.center + transform.right * attackBoxPosition.x * transform.localScale.x + transform.up * attackBoxPosition.y, aggroCollider.bounds.size * attackSize);
        //Соединяющий луч
        Gizmos.color = Color.red;
        if (PlayerInSight())
            Gizmos.color = Color.green;
        if (PlayerInZone(aggroCollider, aggroBoxPosition, aggroSize))
            Gizmos.DrawLine(transform.position, player.position);
    }
    #endregion
}
