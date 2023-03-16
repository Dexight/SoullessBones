using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isBoss;
    private UnityEngine.Object explosion;
    [SerializeField] private BossKilled bossManager;
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
        
        if(isBoss)
        {
            bossManager.isBossAlive = false;

            //чтобы был массивнее взрыв
            explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
