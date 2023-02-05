using System.Collections.Generic;
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

    public bool goUp = false;
    public bool goDown = false;

    [SerializeField] private float speed;
    [SerializeField] private int curfloor;
    private float[] ypositions;
    float maxfloor;

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
        maxfloor = ypositions.Length-1;
    }

    void FixedUpdate()
    {
        if (upTrigger)
        {
            if (!goUp && !goDown && curfloor != maxfloor)
            {
                goUp = true;
            }
            upTrigger = false;
        }

        if(downTrigger)
        {
            if (!goUp && !goDown && curfloor != 0)
            {
                goDown = true;
            }
            downTrigger = false;
        }

        if (goUp)
        {
            if (transform.position.y < ypositions[curfloor + 1])
                Up();
            else 
            {
                curfloor++;
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
                goDown = false;
            }
        }
    }
}
