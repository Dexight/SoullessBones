using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            GameManager.instance.EnableAstral();
            Destroy(gameObject);
        }
    }
}
