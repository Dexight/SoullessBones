using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 8f;
    public int damage = 1;
    public float jumpInterval = 2f;
    public float sightRange = 5f;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Transform player;
    private bool facingRight = true;
    private bool jumping = false;
    private float jumpTimer = 0f;

    private TimeManager timeManager;

    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timeManager = GameManager.instance.timeManager.GetComponent<TimeManager>();
    }

    void Update()
    {
        //animations
        anim.SetFloat("velocity", rb.velocity.y - 0.1f);
        Vector3 rotate = transform.eulerAngles;
        //===========
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, sightRange, playerLayer);
        if (players.Length > 0)
        {
            player = FindClosestPlayer(players);
            if (!timeManager.TimeIsStopped)
                jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpInterval)
            {
                jumpTimer = 0f;
                jumping = true;
            }
        }
        else
        {
            player = null;
            rb.velocity = new Vector3(0, rb.velocity.y);
            jumping = false;
        }
    }

    private void FixedUpdate()
    {
        if (jumping && !timeManager.TimeIsStopped)
        {
            float playerDirection = player.position.x - transform.position.x;
            int direction = playerDirection > 0 ? 1 : -1;
            if (direction == 1 && !facingRight || direction == -1 && facingRight)
            {
                Flip();
            }
            rb.velocity = new Vector2(direction * moveSpeed, jumpForce);
            jumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(player)
                player.GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }

    private Transform FindClosestPlayer(Collider2D[] players)
    {
        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider2D player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestPlayer = player.transform;
                closestDistance = distance;
            }
        }
        return closestPlayer;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }
}

