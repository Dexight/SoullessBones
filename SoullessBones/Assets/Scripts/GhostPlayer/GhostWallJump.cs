using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWallJump : MonoBehaviour
{
    #region Other   
    private Transform player;
    private Rigidbody2D rb;
    private GhostMovement movementController;
    #endregion

    #region Wall Sliding
    [Header("WallSliding")]
    [SerializeField] float wallSlideSpeed;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    [Range(0, 3)] public float SlideSpeedUp;

    [HideInInspector] public bool isTouchingWall;
    [HideInInspector] public bool isWallSliding;
    #endregion

    #region Wall Jumping Variables
    [Header("WallJumping")]
    public int DirectionX;
    public Vector2 climbJumpForce;
    private Vector2 realClimbJumpForce;
    #endregion

    void Start()
    {
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        movementController = GetComponent<GhostMovement>();
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
            StartCoroutine(WallSlideCoroutine());
        }
    }

    #region WallSliding
    void WallSlide()
    {
        isWallSliding = isTouchingWall && rb.velocity.y < 0;

        if (isWallSliding)
        {
            movementController._CanMove = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
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
        rb.AddForce(realClimbJumpForce, ForceMode2D.Impulse);
        movementController.jumpCount = 1;
    }
    private IEnumerator WallSlideCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        movementController._CanMove = true;
        rb.velocity = Vector2.up * (movementController.JumpForce / 2);
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
