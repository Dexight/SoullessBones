using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private Transform CameraCenter; 
    private float maxWorldPositionY;

    private void Start()
    {
        //delete dont destroy on load objects
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameManager.instance.Interface);
            Destroy(GameManager.instance.Player);
            Destroy(GameManager.instance.timeManager);
            Destroy(GameManager.instance.gameObject);
            SceneStatsJsonSerializer.DeleteSave();
        }
        //====================
        maxWorldPositionY = CameraCenter.position.y;
        speed = Mathf.Abs(transform.position.y - maxWorldPositionY) * 0.0008f;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(0, speed, 0);
        if (transform.position.y > maxWorldPositionY)
            Destroy(this);
    }
}
