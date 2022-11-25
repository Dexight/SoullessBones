using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : MonoBehaviour
{
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            Debug.Log("DJ Added");
            GameManager.instance.EnableDoubleJumping();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("DJ already added");
        }
    }
}
