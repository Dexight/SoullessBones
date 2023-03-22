using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : MonoBehaviour
{
    private void Awake()
    {
        if (SceneStats.stats.Contains("dj"))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            GameManager.instance.EnableDoubleJumping();
            SceneStats.stats.Add("dj");
            Destroy(gameObject);
        }
    }
}
