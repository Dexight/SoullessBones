using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private TimeManager timeManager;
    private SpiderMovement move;
    private Barrier barrier;

    public bool CanAttack = false;
    [SerializeField] private Transform throwPos;
    [SerializeField] GameObject egg;
    [SerializeField] DownOfArena downOfArena;
    [SerializeField] WebLine webLine;

    //0 - 15%, 1 - 30%, 2 - 55%
    
    private int[] chances = new int[] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }; //20 argue (5% each)
    
    
    [SerializeField] private float cooldown;
    [SerializeField] private float curtime = 0;

    private void Start()
    {
        timeManager = GameManager.instance.timeManager.GetComponent<TimeManager>();
        move = GetComponent<SpiderMovement>();
        barrier = GameObject.FindGameObjectWithTag("Barrier").GetComponent<Barrier>();
    }

    private void OnRandomAttack()
    {
        CanAttack = false;
        System.Random random = new System.Random();
        int i = random.Next(0, 19);

        int a = chances[i];
        Debug.Log("Attack" + a);
        switch (a)
        {
            case 0:
                ShieldAttack();  break;
            case 1:
                ToxicAttack(); break;
            case 2: 
                ShootWeb(); break;
        }
    }
    
    void ShieldAttack()
    {
        disableMovement();
        Invoke("ThrowAndSpawn", 1.5f);
    }

    void ThrowAndSpawn()
    {
        Instantiate(egg, throwPos.transform.position, Quaternion.Euler(0, 0, -90));
        Instantiate(egg, throwPos.transform.position, Quaternion.Euler(0, 0, -45));
        Instantiate(egg, throwPos.transform.position, Quaternion.Euler(0, 0, -135));
    }

    void ToxicAttack()
    {
        CanAttack = true;
    }

    void ShootWeb()
    {
        webLine.Shoot();
    }

    private void Update()
    {
        if(!timeManager.TimeIsStopped && CanAttack)
            curtime += Time.deltaTime;

        if(CanAttack && curtime >= cooldown)
        {
            curtime = 0;

            System.Random random = new System.Random();
            int i = random.Next(1, 10);
            Debug.Log("AttackTry");
            if (i <= 5) // attack chance = 50%
            {
                OnRandomAttack();
            }
        }
    }

    public void disableMovement()
    {
        CanAttack = false;
        move.SetCanMove(false);
    }

    public void enableMovement()
    {
        move.SetCanMove(true);
        CanAttack = true;
    }
}
