using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    public void FixedUpdate()
    {
        transform.position = transform.position - new Vector3(0, 1.5f * Time.deltaTime, 0);
        if(transform.position.y < -19.8f)
        {
            SpiderMovement movement = GetComponent<SpiderMovement>();
            movement.enabled = true;
            GetComponent<Animator>().enabled = true;
            movement.SetCanMove(true);
            Destroy(this);
        }
    }
}
