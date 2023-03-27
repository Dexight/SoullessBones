using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralItem : MonoBehaviour
{
    [SerializeField] BossDependence bossDependence;
    private void Awake()
    {
        if (SceneStats.stats.Contains("Cultist"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            SceneStats.stats.Add("Cultist");//SAVE IT
            GameManager.instance.EnableAstral();
            bossDependence.editItems();
            SoundVolumeController.PlaySoundEffect2(0);
            Destroy(gameObject);
        }
    }
}
