using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLine : MonoBehaviour
{
    private bool inProcess = false;
    public bool isShooted;
    SpriteRenderer sprite;
    Animator anim;
    TimeManager timeManager;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] BossAttacks attacks;
    [SerializeField] SpiderMovement moving;
    public float shootTime;
    float timer = 0;

    [SerializeField] Transform leftBorder;
    [SerializeField] Transform rightBorder;
    [SerializeField] Transform topBorder;
    [SerializeField] Transform bottomBorder;
    bool goRight;
    bool goLeft;
    bool goUp;
    bool goDown;
    public float speedCoef;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        anim = GetComponent<Animator>();
        anim.enabled = false;
        timeManager = GameManager.instance.timeManager.GetComponent<TimeManager>();
    }

    void Update()
    {
        if (!timeManager.TimeIsStopped && !pauseMenu.GameIsPaused)
        {
            anim.enabled = true;
            if (goUp)
            {
                moving.transform.position = new Vector3(moving.transform.position.x, moving.transform.position.y + moving.speed * Time.deltaTime, moving.transform.position.z);//go down
                if (moving.transform.position.y > topBorder.position.y)
                {
                    goUp = false;
                }
            }
            else
            {
                if (isShooted)
                {
                    sprite.enabled = true;
                    timer += Time.deltaTime;

                    if (shootTime < timer)
                    {
                         Stop();
                    }
                }
                else
                {
                    if (inProcess)
                    {
                        if (goRight)
                        {
                            moving.transform.position = new Vector3(moving.transform.position.x + moving.speed * Time.deltaTime, moving.transform.position.y, moving.transform.position.z);//go right
                            if (moving.transform.position.x >= leftBorder.position.x)
                            {
                                goRight = false;
                            }
                        }
                        else if (goLeft)
                        {
                            moving.transform.position = new Vector3(moving.transform.position.x - moving.speed * Time.deltaTime, moving.transform.position.y, moving.transform.position.z);//go left
                            if (moving.transform.position.x <= rightBorder.position.x)
                            {
                                goLeft = false;
                            }
                        }

                        if (goDown)
                        {
                            moving.transform.position = new Vector3(moving.transform.position.x, moving.transform.position.y - moving.speed * Time.deltaTime, moving.transform.position.z);//go down
                            if (moving.transform.position.y <= bottomBorder.position.y)
                            {
                                goDown = false;
                            }
                        }

                        if (!goDown && !goLeft && !goRight)
                        {
                            attacks.CanAttack = true;
                            moving.enabled = true;
                            inProcess = false;
                        }
                    }
                }
            }
        }
        else
        {
            anim.enabled = false;
        }
    }
    public void Shoot()
    {
        timer = 0;
        inProcess = true;
        goUp = true;
        isShooted = true;
        attacks.CanAttack = false;
        moving.speed = moving.fixedSpeed * speedCoef;
    }

    void Stop()
    {
        isShooted = false;
        anim.enabled = false;
        sprite.enabled = false;

        moving.enabled = false;
        moving.speed = moving.fixedSpeed;
        BackToPosition();   
    }

    void BackToPosition()
    {
        if (moving.transform.position.x < leftBorder.transform.position.x)
        {
            goRight = true;
        }
        else if(rightBorder.position.x < moving.transform.position.x)
        {
            goLeft = true;
        }
        goDown = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(sprite.enabled && !timeManager.TimeIsStopped && !pauseMenu.GameIsPaused)
        {
            if(collision.GetComponent<MovementController>())
            {
                GameManager.instance.Player.GetComponent<HealthSystem>().TakeDamage(1);
            }
        }
    }
}
