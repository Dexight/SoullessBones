using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 2f;
    public int damage = 10;
    private TimeManager timeManager;
    //Animator anim;

    private void Awake()
    {
        //anim = GetComponent<Animator>();    
        timeManager = GameManager.instance.timeManager.GetComponent<TimeManager>();
    }

    void FixedUpdate()
    {
        Collider2D touch = Physics2D.OverlapCircle(transform.position, 0.15f);
        if(!timeManager.TimeIsStopped)
            transform.position += transform.right * speed * Time.fixedDeltaTime;

        if (touch)
        {
            if (touch.CompareTag("Player"))
            {
                HealthSystem player = touch.GetComponent<HealthSystem>();

                player.TakeDamage(damage);

                //anim.SetTrigger("Boom");
                Destroy(gameObject);
            }
            else
            {
                if (touch.CompareTag("Ground"))
                {
                    //anim.SetTrigger("Boom");
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }
}
