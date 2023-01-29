using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    /// <summary>
    /// Этажи(от меньшей высоты к большей)
    /// </summary>
    public List<Transform> points;
    /// <summary>
    /// триггер фиксации персонажа
    /// </summary>
    public bool upTrigger = false;
    public bool downTrigger = false;

    [SerializeField] private bool goUp = false;
    [SerializeField] private bool goDown = false;

    [SerializeField] private float speed;
    [SerializeField] private int curfloor;
    private float[] ypositions;
    void Up()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.fixedDeltaTime);
    }

    void Down()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + (-1) * speed * Time.fixedDeltaTime);
    }

    private void Awake()
    {
        Debug.Assert(points.Count > 1);
        int i = 0;
        ypositions = new float[points.Count];
        foreach (var c in points) //записывает высоты (координаты Y) контрольных точек (этажей)
        {
            ypositions[i++] = c.position.y;
        }
        curfloor = 0;
    }

    void FixedUpdate()
    {
        if (upTrigger)
        {
            MovementController.instance.transform.parent = transform;
            goUp = true;
            upTrigger = false;
        }

        if(downTrigger)
        {
            MovementController.instance.transform.parent = transform;
            goDown = true;
            downTrigger = false;
        }

        if (goUp)
        {
            if (transform.position.y < ypositions[curfloor + 1])
                Up();
            else 
            {
                curfloor++;
                MovementController.instance.transform.parent = null;
                DontDestroyOnLoad(MovementController.instance);
                goUp = false;
            }
        }

        if (goDown)
        {
            if (transform.position.y > ypositions[curfloor - 1])
                Down();
            else
            {
                curfloor--;
                MovementController.instance.transform.parent = null;
                DontDestroyOnLoad(MovementController.instance);
                goDown = false;
            }
        }
    }
}
