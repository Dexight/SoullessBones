using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public float shootDelay = 2f;
    public GameObject bulletPrefab;

    private float timeSinceLastShot = 0f;

    void Update()
    {
        if (timeSinceLastShot >= shootDelay)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            timeSinceLastShot = 0f;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }
}

