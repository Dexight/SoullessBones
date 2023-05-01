using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 2f;
    public int damage = 10;
    private TimeManager timeManager;
    [SerializeField] private Vector3 deviation = new Vector3(0, 0, 0);


    public bool isEgg = false;
    [SerializeField] private GameObject child;
    [SerializeField] private GameObject Line;
    [SerializeField] private Barrier barrier;

    private void Awake()
    {
        timeManager = GameManager.instance.timeManager.GetComponent<TimeManager>();
        if(isEgg)
        {
            barrier = GameObject.FindGameObjectWithTag("Barrier").GetComponent<Barrier>();
        }
    }

    void FixedUpdate()
    {
        Collider2D touch = Physics2D.OverlapCircle(transform.position, 0.15f);
        if(!timeManager.TimeIsStopped)
            transform.position += transform.right * speed * Time.deltaTime + deviation;

        if (touch)
        {
            if (touch.CompareTag("Player"))
            {
                HealthSystem player = touch.GetComponent<HealthSystem>();

                player.TakeDamage(damage);

                Destroy(gameObject);
            }
            else
            {
                if (touch.CompareTag("Ground") || touch.CompareTag("Door"))
                {
                    if (isEgg)
                    {
                        GameObject childObject = Instantiate(child, transform.position + new Vector3(0, 0.15f), Quaternion.Euler(0, 0, 0));
                        if (barrier)
                        {
                            childObject.GetComponentInChildren<Enemy>().barrier = GameObject.FindGameObjectWithTag("Barrier").GetComponent<Barrier>();
                            barrier.AddChild(childObject);
                            GameObject childLine = Instantiate(Line, new Vector3(0, 0), Quaternion.Euler(0, 0, 0));
                            childLine.GetComponent<LineController>().start = childObject.transform;
                            barrier.barrierUpped = true;
                        }
                    }
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
