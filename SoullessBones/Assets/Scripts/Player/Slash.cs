using UnityEngine;

public class Slash : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public int damage;
    public float Upper; //поднимает на данную высоту удар
    public float forceOfOutput;
    public float forceOfPlayerOutput;
    [Range(0.5f, 1f)] public float coefficientOfOutput;

    private Transform attackPos;
    public LayerMask enemy;
    [Range(0, 10f)] public float attackRange;
    private bool isDamageDone;

    private void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        attackPos = transform;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position + new Vector3(0, Upper, 0), attackRange, enemy);
        isDamageDone = enemies.Length > 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
            if (!enemies[i].GetComponent<Enemy>().isHeavy)  //отдача от удара
            {
                if (GetComponent<Slash>().gameObject.tag == "RightSlash")
                {
                    enemies[i].GetComponent<Rigidbody2D>().AddForce(transform.right * forceOfOutput);
                    if(playerRB.velocity.x == 0)
                        playerRB.GetComponent<Rigidbody2D>().AddForce(-1 * transform.right * forceOfPlayerOutput);
                    else
                        playerRB.GetComponent<Rigidbody2D>().AddForce(-1 * coefficientOfOutput * playerRB.velocity.x * transform.right * forceOfPlayerOutput);
                }
                else if (GetComponent<Slash>().gameObject.tag == "LeftSlash")
                {
                    enemies[i].GetComponent<Rigidbody2D>().AddForce(transform.right * -1 * forceOfOutput);
                    if (playerRB.velocity.x == 0)
                        playerRB.GetComponent<Rigidbody2D>().AddForce(transform.right * forceOfPlayerOutput);
                    else
                        playerRB.GetComponent<Rigidbody2D>().AddForce(-1 * coefficientOfOutput * playerRB.velocity.x * transform.right * forceOfPlayerOutput);
                }
                else
                    enemies[i].GetComponent<Rigidbody2D>().AddForce(transform.up * forceOfOutput);
            }
        }
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = isDamageDone ? Color.green : Color.red;
        Gizmos.DrawWireSphere(attackPos.position + new Vector3(0, Upper, 0), attackRange);
    }
}   
