using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform start;
    [SerializeField] private Transform end;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        end = GameObject.FindGameObjectWithTag("Barrier").GetComponent<Transform>();
    }

    void Update()
    {
        lineRenderer.positionCount = 2;
        if (start)
            lineRenderer.SetPosition(0, start.position);
        else
        {
            //Destroy(transform.parent.gameObject);
            Destroy(gameObject);
            return;
        }
        lineRenderer.SetPosition(1, end.position);
    }
}
