using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpItem : MonoBehaviour
{
    [SerializeField] private BossDoor door1;
    [SerializeField] private BossDoor door2;
    private void Awake()
    {
        if(SceneStats.stats.Contains("Spawners"))
        {
            Destroy(door1);
            Destroy(door2);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            door1.OpenDoor();
            door2.OpenDoor();
            SceneStats.stats.Add("Spawners");
            GameManager.instance.EnableWallJumping();
            Destroy(gameObject);
        }
    }
}
