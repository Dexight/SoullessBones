using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDoubleJump : MonoBehaviour
{
    void Update()
    {
        DoubleJump();
    }
    private void DoubleJump()
    {
        if (!GetComponent<GhostMovement>().isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && (GetComponent<GhostMovement>().jumpCount == 1) && !GhostMovement.isWallSliding)
        {
            GetComponent<GhostMovement>().Jump();
            GetComponent<GhostMovement>().jumpCount = 0;
        }
    }
}
