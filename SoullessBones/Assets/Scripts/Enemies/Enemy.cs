using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isHeavy;
    private UnityEngine.Object explosion;

    private void Start () 
    {
        explosion = Resources.Load("Explosion");
    }

    private void Update()
    {
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy() 
    {
        GameObject explosionRef = (GameObject)Instantiate(explosion);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
