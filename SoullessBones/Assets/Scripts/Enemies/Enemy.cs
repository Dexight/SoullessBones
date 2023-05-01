using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isBoss;
    [SerializeField] bool isSpawner = false; 
    private UnityEngine.Object explosion;
    public Spawner spawner;
    [SerializeField] private int enemyId = -1;
    public Barrier barrier;

    [SerializeField] private BossKilled bossManager;
    BossHealth bossHealth;
    private void Start () 
    {
        explosion = Resources.Load("Explosion");
        if (enemyId != -1 && SceneStats.destroyedEnemies.Contains(enemyId))
        {
            Destroy(gameObject);
        }
        if (isBoss)
        {
            bossHealth = gameObject.GetComponent<BossHealth>();
            bossHealth.Initialize(health);
        }
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

        if (isBoss)
        {
            bossManager.isBossAlive = false;

            //чтобы был массивнее взрыв
            explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            bossHealth.EndFight();
        }

        Destroy(explosionRef, 1.0f);

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

        if(barrier)
        {
            barrier.RemoveChild(gameObject);
            Destroy(GetComponentInParent<Transform>().gameObject);
        }

        SceneStats.destroyedEnemies.Add(enemyId);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(isBoss)bossHealth.Damaged(health);
    }
}
