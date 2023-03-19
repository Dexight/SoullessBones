using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isBoss;
    [SerializeField] bool isSpawner = false; 
    private UnityEngine.Object explosion;
    public Spawner spawner;

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

        if (spawner)
        {
            if (isSpawner)
            {
                spawner.SpawnerRemove(transform);
            }
            else
            {
                spawner.EnemyDestroyed();
            }
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
