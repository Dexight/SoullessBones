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
    internal bool alreadyWalking = false;
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
    public bool isJumped = false;
    public int jumpCount;
    private float MaxFallSpeed = 10f;
    private UnityEngine.Object dust;

    [Header("Jump Down")]
    public bool canJumpDown = true;
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
        dust = Resources.Load("Dust");
        wallJumping = GetComponent<WallJumping>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        JumpForce = 7f;
        jumpCount = 1;
        checkSize = new Vector2(0.52f, -0.01f);
        _CanMove = true;
    }

    private void Update()
    {
        isWallSliding = wallJumping.isWallSliding;
        isTouchingWall = wallJumping.isTouchingWall && wallJumping.enabled;

        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (_CanMove)
                HorizontalMove();
            NormalJump();
            JumpDown();
            Flip();
            Animations();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            _moveInput = 0;
            Animations();
            //Воспроизведение звука ходьбы
            if (alreadyWalking)
            {
                alreadyWalking = false;
                SoundVolumeController.PlayLongEffect(false, 0);
            }
        }
        fall_limit(MaxFallSpeed);
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
        {
            _moveInput = Input.GetAxis("Horizontal");
            //Воспроизведение звука ходьбы
            if(_moveInput != 0 && !alreadyWalking && isGrounded)
            {
                alreadyWalking = true;
                SoundVolumeController.PlayLongEffect(true, 0);
            }
            else if(alreadyWalking && (_moveInput == 0 || !isGrounded))
            {
                alreadyWalking = false;
                SoundVolumeController.PlayLongEffect(false, 0);
            }
        }
        else
        {
            _moveInput = Input.GetAxisRaw("Horizontal");
            //Воспроизведение звука ходьбы
            if (alreadyWalking)
            {
                alreadyWalking = false;
                SoundVolumeController.PlayLongEffect(false, 0);
            }
        }

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

    void fall_limit(float MaxSpeed)
    {
        if (rb.velocity.magnitude > MaxSpeed)   //Если скорость объекта превышает максимальную скорость
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;  //Задать скорость на уровне максимальной
        }
    }
    /// <summary>
    /// Просто прыжок вверх
    /// </summary>
    public void Jump()
    {
        rb.velocity = Vector2.up * JumpForce;
        StartCoroutine(isJumping());
        SoundVolumeController.PlaySoundEffect(4);
        GameObject dustRef = (GameObject)Instantiate(dust);
        dustRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Destroy(dustRef, 1.0f);
    }
    private IEnumerator isJumping()
    {
        yield return new WaitForSeconds(0.1f);
        isJumped = true;
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
        if(Input.GetKey(KeyCode.S) && !jumpDownEnable && canJumpDown)
        {
            StartCoroutine(JumpDownCoroutine());
        }
    }

    private IEnumerator JumpDownCoroutine()
    {
        jumpDownEnable = true;
        
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        yield return new WaitForSeconds(0.3f);
        while (isWallSliding)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        jumpDownEnable = false;
    }
    #endregion

    #region Other Functions
    private void CheckWorld()
    {
        var wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapBox(GroundCheck.position, checkSize, 0, Ground);
        if(isGrounded && !wasGrounded)
        {
            //SoundVolumeController.PlaySoundEffect(5);
        }
        if (isGrounded)
        {
            jumpCount = 1;
            isJumped = false;
        }
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
