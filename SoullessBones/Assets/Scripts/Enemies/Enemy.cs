using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isBoss;
    [SerializeField] bool isSpawner = false; 
    private UnityEngine.Object explosion;
    public Spawner spawner;
    [SerializeField] private int enemyId = -1;

    [SerializeField] private BossKilled bossManager;
    private void Start () 
    {
        explosion = Resources.Load("Explosion");
        if (enemyId != -1 && SceneStats.destroyedEnemies.Contains(enemyId))
        {
            Destroy(gameObject);
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

            //����� ��� ��������� �����
            explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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
        SceneStats.destroyedEnemies.Add(enemyId);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
