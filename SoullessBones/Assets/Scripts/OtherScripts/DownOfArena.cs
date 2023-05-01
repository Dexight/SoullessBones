using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownOfArena : MonoBehaviour
{
    public bool isPlayerDown = false;
    public bool isSmoke = false;
    [SerializeField] Toxic toxic;
    [SerializeField] PauseMenu pauseMenu;
    private TimeManager timeManager;
    public float smokeTime;
    float timer = 0;

    //заглушка
    private SpriteRenderer sprite;
    [SerializeField] ParticleSystem particles;

    private void Start()
    {
        timeManager = GameManager.instance.timeManager.GetComponent<TimeManager>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    private void Update()
    {
        if(isSmoke)
        {
            if (isPlayerDown && !pauseMenu.GameIsPaused && !timeManager.TimeIsStopped)
            {
                GameManager.instance.Player.GetComponent<HealthSystem>().TakeDamage(1);
            }

            timer += Time.deltaTime;
            if (timer > smokeTime) 
            {
                endOfSmoke();
            }
        }
    }

    public void GenerateSmoke()
    {
        isSmoke = true;
        //======
        //TODO Smoke ON
        //sprite.enabled = true;
        //======
        timer = 0;
        particles.Play();
    }

    private void endOfSmoke()
    {
        isSmoke = false;
        //======
        //TODO Smoke OFF
        sprite.enabled = false; 
        //======
        toxic.canSmoke = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isPlayerDown = collision.GetComponent<MovementController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerDown = false;
    }
}
