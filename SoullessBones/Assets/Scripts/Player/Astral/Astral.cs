using Cinemachine;
using UnityEngine;

public class Astral : MonoBehaviour
{
    private TimeManager timeManager;
    private MovementController movementController;
    private Animator _animator;
    [SerializeField] private GameObject ghostPrefab;
    void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        movementController = GetComponent<MovementController>();
        _animator = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.Q)) //Stop Time when Q is pressed
        {
            if(!GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().GameIsPaused)
            {
                if (!timeManager.TimeIsStopped)
                {
                    timeManager.StopTime();
                    spawnGhost();
                }
                else
                {
                    timeManager.ContinueTime(); //Cancel when Q is pressed again
                    cancelAstral();
                } 
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && timeManager.TimeIsStopped)  //Continue Time and teleport when E is pressed
        {
            timeManager.ContinueTime();
            teleport_to_ghost();
        }
    }

    GameObject prefab; 
    void spawnGhost()
    {
       prefab = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
       GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>().Follow = prefab.transform;
    }

    void cancelAstral()
    {
        GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform;
        movementController._CanMove = true;
        Destroy(prefab);
    }

    void teleport_to_ghost()
    {
        transform.position = prefab.transform.position;
        GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform;
        if (prefab.GetComponent<GhostMovement>().facingRight != movementController.facingRight)
            movementController.FlipCur();
        movementController._CanMove = true;
        Destroy(prefab);
    }
}
