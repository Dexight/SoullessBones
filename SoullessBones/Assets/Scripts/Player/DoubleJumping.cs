using System.Collections;
using UnityEngine;

public class DoubleJumping : MonoBehaviour
{
    private MovementController movementController;
    private void Awake()
    {
        movementController = GetComponent<MovementController>();
    }
    void Update()
    {
        DoubleJump();
    }
    private void DoubleJump()
    {
        if (!movementController.isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && (movementController.jumpCount == 1) && !movementController.isWallSliding && movementController._CanMove)
        {
            movementController.Jump();
            movementController.jumpCount = 0;
        }
    }
}
