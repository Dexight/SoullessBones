using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public float shootDelay = 2f;
    public GameObject bulletPrefab;
    public Transform gunTransform;

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
            Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
            anim.SetTrigger("Shoot");
            timeSinceLastShot = 0f;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }
}

