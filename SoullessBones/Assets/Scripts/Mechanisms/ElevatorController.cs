using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private GameObject lift;
    [SerializeField] private Animator fireAnimator;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject dialog;
    private Elevator elevator;
    private bool nearLeverArm = false;

    private void Awake()
    {
        elevator = lift.GetComponent<Elevator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        tutorial.SetActive(true);
        dialog.SetActive(true);
        nearLeverArm = collision.gameObject.tag == "Player";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tutorial.SetActive(false);
        dialog.SetActive(false);
        nearLeverArm = false;
    }

    void Update()
    {
        if (nearLeverArm && Input.GetKeyDown(KeyCode.W))
            elevator.upTrigger = true;
        if (nearLeverArm && Input.GetKeyDown(KeyCode.S))
            elevator.downTrigger = true;

        if (nearLeverArm && Input.GetKeyDown(KeyCode.T))
        {
            //start dialogue
        }

        fireAnimator.SetBool("large", elevator.goUp);
        fireAnimator.SetBool("small", elevator.goDown);
    }
}