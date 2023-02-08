using UnityEngine;

public class Slash : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public int damage;
    public int bottleFill;
    public float Upper; //поднимает на данную высоту удар
    public float forceOfOutput;
    public float forceOfPlayerOutput;
    [Range(0.5f, 1f)] public float coefficientOfOutput;

    private Transform attackPos;
    public LayerMask enemy;
    [Range(0, 10f)] public float attackRange;
    private bool isDamageDone;
    private DistanceAttack distanceAttack;
    private void Awake()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        distanceAttack = SceneLoader.instance.GetComponentInChildren<DistanceAttack>();
        attackPos = transform;
        damage = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().damage;
    }
    private void Start()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position + new Vector3(0, Upper, 0), attackRange, enemy);
        isDamageDone = enemies.Length > 0;
        for (int i = 0; i < enemies.Length; i++)    //перебор всех врагов
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage); //нанесение урона
            if(playerRB.GetComponent<AttackSystem>().distanceUnlock)
                distanceAttack.fillBottle(bottleFill);    //набор бутылки
            if (!enemies[i].GetComponent<Enemy>().isHeavy)      //отдача от удара
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
