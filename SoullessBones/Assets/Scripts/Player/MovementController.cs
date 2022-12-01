using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    #region Other Variables
    public static MovementController instance;
    [Header("ObjectReferences")]
    private Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    public int DirectionX;
    private WallJumping wallJumping;
    #endregion

    #region Horizontal move Variables
    [Header("HorizontalMovement")]
    public bool _CanMove;
    [Range(0, 10f)] public float _Speed;
    [HideInInspector] public float _moveInput;
    [HideInInspector] public bool facingRight = true;
    #endregion

    #region World Check Variables
    [Header("Grounded")]
    public bool isGrounded;
    public Transform GroundCheck; // положение ног(чекера)
    public Vector2 checkSize; //размер чекера
    public LayerMask Ground;
    [Header("Touching wall")]
    public bool isTouchingWall;
    public bool isWallSliding;
    #endregion

    #region Jump Variables
    [Header("Jump Variables")]
    [Range(0, 10f)] public float JumpForce;
    public int jumpCount;

    [Header("Jump Down")]
    public bool jumpDownEnable = false;
    private int playerLayer = 3;
    private int platformLayer = 9;
    #endregion

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
    }

    private void Start()
    {   
        wallJumping = GetComponent<WallJumping>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        JumpForce = 6.6f;
        jumpCount = 1;
        checkSize = new Vector2(0.52f, -0.01f);
        _CanMove = true;
    }

    private void Update()
    {
        isWallSliding = wallJumping.isWallSliding;
        isTouchingWall = wallJumping.isTouchingWall;

        if(_CanMove)
            HorizontalMove();
        NormalJump();
        JumpDown();
        Flip();
        Animations();
        CheckWorld();
        if (isGrounded && isTouchingWall && !Input.GetKey(KeyCode.Space))   //Фикс бага с прыгающей rb.velocity.y при спаме ходьбы в стенку;
            rb.velocity = new Vector2(0, 0);
    }

    #region Horizontal Move Functions

    /// <summary>
    /// Горизонтальное перемещение персонажа
    /// </summary>
    private void HorizontalMove()
    {
        if (!isTouchingWall)
            _moveInput = Input.GetAxis("Horizontal");
        else
            _moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(_moveInput * _Speed, rb.velocity.y);
    }
    /// <summary>
    /// Процедура поворота спрайта на 180 градусов
    /// </summary>
    private void Flip()
    {
        if (_moveInput != 0 && (_moveInput > 0) != facingRight && !isWallSliding)
        {
            FlipCur();
            GetComponent<AttackSystem>().PreDelete();
        }
    }

    public void FlipCur()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    #endregion

    #region Vertical Move Functions
    /// <summary>
    /// Просто прыжок вверх
    /// </summary>
    public void Jump()
    {
        rb.velocity = Vector2.up * JumpForce;
    }

    private void NormalJump()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && jumpCount == 1 && _CanMove)
        {
            Jump();
        }
    }

    private void JumpDown()
    {
        if(Input.GetKey(KeyCode.S) && !jumpDownEnable)
        {
            StartCoroutine(JumpDownCoroutine());
        }
    }

    private IEnumerator JumpDownCoroutine()
    {
        jumpDownEnable = true;
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        jumpDownEnable = false;
    }
    #endregion

    #region Other Functions
    private void CheckWorld()
    {
        isGrounded = Physics2D.OverlapBox(GroundCheck.position, checkSize, 0, Ground);
        if (isGrounded)
            jumpCount = 1;
        DirectionX = !facingRight ? 1 : -1;
    }

    private void Animations()
    {
        anim.SetBool("isRunning", _moveInput != 0 && !isTouchingWall);
        anim.SetBool("Grounded", isGrounded);
        if (!isWallSliding)
            anim.SetFloat("VelocityY", rb.velocity.y);
        anim.SetBool("WallSlide", isWallSliding);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(GroundCheck.position, checkSize);
    }
    #endregion
}
