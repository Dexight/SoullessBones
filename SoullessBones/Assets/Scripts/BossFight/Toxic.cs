using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Toxic : MonoBehaviour
{
    [SerializeField] private Transform centerOfArena;
    [SerializeField] private DownOfArena downOfArena;
    [SerializeField] private BossAttacks attacks;
    [SerializeField] private SpiderMovement moving;
    [SerializeField] private GameObject spray;
    [SerializeField] private PauseMenu pauseMenu;

    private TimeManager timeManager;

    private SpriteRenderer sprite;
    private Animator anim;

    public float sprayTime;
    float timer = 0;

    public bool canSmoke = true;
    bool goCenter = false;
    bool goLeft = false;
    bool isSpray = false;
    bool doSmoke = false;
    public float speedCoef;
    void Start()
    {
        sprite = spray.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        anim = spray.GetComponent<Animator>();
        anim.enabled = false;
        timeManager = GameManager.instance.timeManager.GetComponent<TimeManager>();
    }

    void Update()
    {
        if (!timeManager.TimeIsStopped && !pauseMenu.GameIsPaused)
        {
            if (goCenter)
            {
                if (goLeft)
                {
                    transform.position = new Vector3(transform.position.x - moving.speed * Time.deltaTime, transform.position.y, transform.position.z);//go left
                    if (transform.position.x < centerOfArena.position.x)
                    {
                        goCenter = false;
                        sprayToxic();
                    }
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + moving.speed * Time.deltaTime, transform.position.y, transform.position.z);//go right
                    if (transform.position.x > centerOfArena.position.x)
                    {
                        goCenter = false;
                        sprayToxic();
                    }
                }
            }
        }

        if(isSpray)
        {
            sprite.enabled = true;
            if (!timeManager.TimeIsStopped && !pauseMenu.GameIsPaused)
            {
                anim.enabled = true;
                timer += Time.deltaTime;
            }
            else
            {
                anim.enabled = false;
            }

            if(doSmoke && timer > sprayTime/2)
            {
                downOfArena.GenerateSmoke();
                doSmoke = false;
            }

            if (sprayTime < timer)
            {
                Stop();
            }
        }
    }

    public void goToCenter()
    {
        goCenter = true;
        goLeft = transform.position.x > centerOfArena.position.x;
        moving.speed = moving.fixedSpeed * speedCoef;
        moving.enabled = false;
    }

    void sprayToxic()
    {
        timer = 0;
        isSpray = true;
        doSmoke = true;
        canSmoke = false;
    }
    
    private void Stop()
    {
        isSpray = false;
        anim.enabled = false;
        sprite.enabled = false;
        moving.enabled = true;
        attacks.CanAttack = true;
        moving.speed = moving.fixedSpeed;
        doSmoke = false;
    }
}
