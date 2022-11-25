using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            GameManager.instance.EnableWallJumping();
            Destroy(gameObject);
        }
    }
}
