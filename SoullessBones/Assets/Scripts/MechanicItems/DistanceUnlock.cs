using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceUnlock : MonoBehaviour
{
    private void Awake()
    {
        if (SceneStats.stats.Contains("dist"))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            GameManager.instance.EnableDistanceAttack();
            SceneStats.stats.Add("dist");
            Destroy(gameObject);
        }
    }
}
