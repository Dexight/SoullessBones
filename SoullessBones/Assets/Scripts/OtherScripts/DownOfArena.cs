using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownOfArena : MonoBehaviour
{
    public bool isPlayerDown = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        isPlayerDown = collision.GetComponent<MovementController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerDown = false;
    }
}
