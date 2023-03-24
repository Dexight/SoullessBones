using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private int speed = 1;
    bool canMove = false;

    float eps = 0.01f;
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
    private bool GetCanMove()
    {
        return canMove;
    }

    private void Awake()
    {
        if(SceneStats.stats.Contains("spider"))
        {
            Destroy(gameObject);
            //etc.
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        //Слежка за игроком
        if(canMove && !pauseMenu.GameIsPaused)
        if ((transform.position.x - player.transform.position.x) * (transform.position.x - player.transform.position.x) > (speed * eps) * (speed * eps))//сравнение модусей относительно эпсилон
        {
            if (transform.position.x > player.transform.position.x)
                transform.position = new Vector3(transform.position.x - speed * eps, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + speed * eps, transform.position.y, transform.position.z);
        }
    }
}
