using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private WebLine web;
    public float fixedSpeed = 0;
    [HideInInspector] public float speed;
    [SerializeField] private BossAttacks attacks;
    bool canMove = false;

    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;
    public void OnStartMove()
    {
        attacks.CanAttack = true;
        SetCanMove(true);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    private void Awake()
    {
        if(SceneStats.stats.Contains("Spider"))
        {
            Destroy(gameObject);
            //etc.
        }
        player = GameObject.FindGameObjectWithTag("Player");
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        speed = fixedSpeed;
    }
    
    void Update()
    {
        //������ �� �������
        if (canMove && !pauseMenu.GameIsPaused && !timeManager.TimeIsStopped)
            if ((transform.position.x - player.transform.position.x) * (transform.position.x - player.transform.position.x) > (speed * Time.deltaTime) * (speed * Time.deltaTime))//��������� ������� ������������ �������
            {
                if (transform.position.x > player.transform.position.x)
                {
                    if (!(transform.position.x < leftBorder.position.x) || web.isShooted)
                        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);//go left
                }
                else
                {
                    if (!(transform.position.x > rightBorder.position.x) || web.isShooted)
                        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);//go right
                }
            }
    }
}
