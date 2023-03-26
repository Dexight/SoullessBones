using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public float shootDelay = 2f;
    public GameObject bulletPrefab;

    private float timeSinceLastShot = 0f;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (timeSinceLastShot >= shootDelay)
        {
            //Bullet spawn
            //Instantiate(bulletPrefab, transform.position, transform.rotation);
            anim.SetTrigger("Shoot");
            timeSinceLastShot = 0f;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }
}

