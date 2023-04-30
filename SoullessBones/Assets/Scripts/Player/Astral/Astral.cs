using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Astral : MonoBehaviour
{
    private TimeManager timeManager;
    private MovementController movementController;
    private Animator _animator;
    private Interface Interface;
    private Image timebar1;
    private Image timebar2;
    private Image timeCd1;
    private Image timeCd2;
    [SerializeField] private GameObject ghostPrefab;
    public bool canUseAstral = true;
    private float timer = 0; //0 to 1
    float timeLessSpeed = 0.3f;
    private bool lossTime = false;
    private bool isCooldown = false;
    private float cooldownTimer = 0;
    [SerializeField] private float cooldownSpeed;
    void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        movementController = GetComponent<MovementController>();
        _animator = GetComponent<Animator>();
        Interface = SceneLoader.instance.GetComponent<Interface>();
        timebar1 = Interface.timebar1.GetComponent<Image>();
        timebar2 = Interface.timebar2.GetComponent<Image>();
        timeCd1 = Interface.timeCooldown1.GetComponent<Image>();
        timeCd2 = Interface.timeCooldown2.GetComponent<Image>();
    }

    void Update()
    {

        if (!timeManager.TimeIsStopped)
        {
            _animator.enabled = true;
        }
        else
        {
            _animator.enabled = false;
        }

        if ((Input.GetKeyDown(KeyCode.Q)) && canUseAstral) //Stop Time when Q is pressed
        {
            if(!GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().GameIsPaused)
            {
                if (!timeManager.TimeIsStopped)
                {
                    SoundVolumeController.PauseMusic(true);
                    SoundVolumeController.PlaySoundEffect2(1);
                    timeManager.StopTime(true);
                    spawnGhost();
                    timer = 1;
                    lossTime = true;
                }
                else
                {
                    SoundVolumeController.PauseMusic(false);
                    SoundVolumeController.PlaySoundEffect2(2);
                    timeManager.ContinueTime(); //Cancel when Q is pressed again
                    cancelAstral();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && timeManager.TimeIsStopped)  //Continue Time and teleport when E is pressed
        {
            SoundVolumeController.PauseMusic(false);
            SoundVolumeController.PlaySoundEffect2(2);
            timeManager.ContinueTime();
            teleport_to_ghost();
        }

        Timer();
        cooldown();
    }

    GameObject prefab;
    void spawnGhost()
    {
       prefab = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
       GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>().Follow = prefab.transform;
    }

    void Timer()
    {
        if(lossTime)
        {
            timer -= timeLessSpeed * Time.deltaTime;
            if (timer <= 0)
            {
                SoundVolumeController.PauseMusic(false);
                SoundVolumeController.PlaySoundEffect2(2);
                timeManager.ContinueTime();
                if(prefab)
                    teleport_to_ghost();
                lossTime = false;
            }
        }
        timebar1.fillAmount = timer;
        timebar2.fillAmount = timer;
    }

    void cooldown()
    {
        if(isCooldown)
        {
            cooldownTimer += cooldownSpeed * Time.deltaTime;
            canUseAstral = false;
        }

        if(cooldownTimer >= 1)
        {
            cooldownTimer = 0;
            isCooldown= false;
            canUseAstral = true;
        }
        timeCd1.fillAmount = cooldownTimer;
        timeCd2.fillAmount = cooldownTimer;
    }

    public void timerNull()
    {
        timer = 0;
        cooldownTimer = 0;
    }

    void cancelAstral()
    {
        GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform;
        movementController._CanMove = true;
        Destroy(prefab);
        lossTime = false;
        timer = 0;
        isCooldown = true;
        cooldownTimer = 0;
    }

    void teleport_to_ghost()
    {
        transform.position = prefab.transform.position;
        GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform;
        if (prefab.GetComponent<GhostMovement>().facingRight != movementController.facingRight)
            movementController.FlipCur();
        movementController._CanMove = true;
        timer = 0;
        Destroy(prefab);
        isCooldown = true;
        cooldownTimer = 0;
    }
}
