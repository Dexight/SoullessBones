using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private List<GameObject> childs = new List<GameObject>();
    [SerializeField] private BossAttacks attacks;
    public bool barrierUpped = false;
    [SerializeField] private SpriteRenderer sprite;

    private BoxCollider2D hitbox;

    public void AddChild(GameObject g)
    {
        childs.Add(g);
    }

    public void RemoveChild(GameObject g)
    {
        childs.Remove(g);
    }

    void Start()
    {
        sprite.enabled = false;
        hitbox = attacks.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (barrierUpped)
        {
            if (childs.Count > 0)
            {
                BarrierON();
            }
            else
            {
                BarrierOFF();
            }
        }
    }

    public void BarrierON()
    {
        hitbox.enabled = false;
        sprite.enabled = true;
    }

    public void BarrierOFF()
    {
        barrierUpped = false;
        hitbox.enabled = true;
        attacks.enableMovement();
        sprite.enabled = false;
    }
}
