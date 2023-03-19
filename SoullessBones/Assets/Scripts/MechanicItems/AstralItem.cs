using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralItem : MonoBehaviour
{
    [SerializeField] BossDependence bossDependence;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementController>())
        {
            GameManager.instance.EnableAstral();
            bossDependence.editItems();
            Destroy(gameObject);
        }
    }
}
