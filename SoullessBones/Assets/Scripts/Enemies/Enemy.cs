using System.Runtime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isHeavy;

    private void Update()
    {
        if(health <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
