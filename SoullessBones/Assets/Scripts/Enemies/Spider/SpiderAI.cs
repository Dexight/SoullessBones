using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    TimeManager timeManager;
    #region Values
    private Rigidbody2D rb;
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
    #endregion
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }
    void Update()
    {
        Direction = facingRight ? 1 : -1;
        currentDirection = Direction;


        if (Direction != currentDirection)
            Flip();

        if(!timeManager.TimeIsStopped)
            Patrolling();
    }
    private void Patrolling()
    {
        float curSpeed = Speed;
        if (isAttack)
            curSpeed = 0;
        rb.velocity = new Vector2(curSpeed * Direction, 0);

        if (isWallTouch() || !isGrounded())
            Flip();
    }
    private void Flip()
    {
        //anim.SetTrigger("Flip"); potom
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
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
    }
    #endregion
}