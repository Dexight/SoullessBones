using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : MonoBehaviour
{
    public GameObject tutorial;
    private void Awake()
    {
        if (SceneStats.stats.Contains("dj"))
        {
            Destroy(gameObject);
            Destroy(tutorial);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            GameManager.instance.EnableDoubleJumping();
            SceneStats.stats.Add("dj");
            SoundVolumeController.PlaySoundEffect2(0);
            tutorial.SetActive(true);
            Destroy(gameObject);
        }
    }
}
