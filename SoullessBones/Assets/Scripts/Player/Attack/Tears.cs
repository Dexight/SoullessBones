using UnityEngine;

public class Tears : MonoBehaviour
{
    [SerializeField] private bool isRight;
    [SerializeField] private bool isUp;
    [SerializeField] private int speed;
    void Awake()
    {
    }

    void FixedUpdate()
    {
        if (!isUp)
        {
            if (isRight)
            {
                transform.position = transform.position = new Vector2(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y); ;
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y);
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.fixedDeltaTime);
        }
    }
}
