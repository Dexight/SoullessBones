using System.Collections;
using UnityEngine;

public class DoubleJumping : MonoBehaviour
{
    void Update()
    {
        DoubleJump();
    }
    private void DoubleJump()
    {
        if (!GetComponent<MovementController>().isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && (GetComponent<MovementController>().jumpCount == 1) && !MovementController.isWallSliding)
        {
            GetComponent<MovementController>().Jump();
            GetComponent<MovementController>().jumpCount = 0;
        }
    }
}
