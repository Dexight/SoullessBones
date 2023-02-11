using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private GameObject lift;
    [SerializeField] private Animator fireAnimator;
    private Elevator elevator;
    private bool nearLeverArm = false;
    private void Awake()
    {
        elevator = lift.GetComponent<Elevator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        nearLeverArm = collision.gameObject.tag == "Player";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nearLeverArm = false;
    }

    void Update()
    {
        if (nearLeverArm && Input.GetKeyDown(KeyCode.W))
            elevator.upTrigger = true;
        if (nearLeverArm && Input.GetKeyDown(KeyCode.S))
            elevator.downTrigger = true;

        fireAnimator.SetBool("large", elevator.goUp);
        fireAnimator.SetBool("small", elevator.goDown);
    }
}