using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumping : MonoBehaviour
{
    #region Other   
    private Transform player;
    private Rigidbody2D rb;
    private MovementController movementController;
    private TimeManager timeManager;
    #endregion

    #region Wall Sliding
    [Header("WallSliding")]
    private float wallSlideSpeed;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    [Range(0, 3)] public float SlideSpeedUp;

    [HideInInspector]public bool isTouchingWall;
    [HideInInspector]public bool isWallSliding;
    #endregion

    #region Wall Jumping Variables
    [Header("WallJumping")]
    public int DirectionX;
    public Vector2 climbJumpForce;
    private Vector2 realClimbJumpForce;
    #endregion

    void Awake()
    {
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        wallSlideSpeed = 0.7f;
        movementController = GetComponent<MovementController>();
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();  
    }

    void Update()
    {
        DirectionX = movementController.DirectionX;
        CheckWorld();
    }

    void FixedUpdate()
    {
        WallSlide();

        if (isWallSliding && Input.GetKey(KeyCode.Space))
        {
            movementController._CanMove = false;
            WallJump();

            movementController._moveInput *= -1;
            StartCoroutine(WallJumpCoroutine());
        }
    }

    #region WallSliding
    void WallSlide()
    {
        isWallSliding = isTouchingWall && rb.velocity.y < 0;

        if (isWallSliding)
        {
            GetComponent<AttackSystem>().onWall = true;
            movementController._CanMove = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
        else
        {
            GetComponent<AttackSystem>().onWall = false;
        }

        if (isWallSliding && Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed - SlideSpeedUp);
        }
    }
    #endregion

    #region WallJumping
    private void WallJump()
    {
        realClimbJumpForce.x = climbJumpForce.x * DirectionX;
        realClimbJumpForce.y = climbJumpForce.y;
        rb.velocity = new Vector2(0, 0);
        if (!timeManager.TimeIsStopped)
            rb.AddForce(realClimbJumpForce, ForceMode2D.Impulse);
        movementController.jumpCount = 1;
    }
    private IEnumerator WallJumpCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        if (!timeManager.TimeIsStopped)
        {
            rb.velocity = Vector2.up * (movementController.JumpForce / 2);
            movementController._CanMove = true;
        }
    }
    #endregion

    #region Other Functions
    private void CheckWorld()
    {
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isTouchingWall ? Color.green : Color.red;
        Gizmos.DrawWireCube(wallCheckPoint.position, wallCheckSize);
    }
    #endregion
}
