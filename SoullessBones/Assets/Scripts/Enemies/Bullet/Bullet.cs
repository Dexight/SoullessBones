using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int damage = 10;

    private float timeSinceSpawned = 0f;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        if (timeSinceSpawned >= lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            timeSinceSpawned += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem player = collision.gameObject.GetComponent<HealthSystem>();

            player.TakeDamage(damage);

            Destroy(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
