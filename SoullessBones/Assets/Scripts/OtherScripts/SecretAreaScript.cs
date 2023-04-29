using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretAreaScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer darknessSprite;
    float alphaValue = 1f;

    [SerializeField] float disappearRate = 1f;

    bool playerEntered = false;
    [SerializeField] bool toggleDarkness = false;


    void Update()
    {
        if (playerEntered)
        {
            alphaValue -= Time.deltaTime * disappearRate;
            if(alphaValue <= 0)
                alphaValue = 0;

            darknessSprite.color = new Color(
                darknessSprite.color.r,
                darknessSprite.color.g,
                darknessSprite.color.b,
                alphaValue);
        }

        else
        {
            alphaValue += Time.deltaTime * disappearRate;
            if (alphaValue >= 1)
                alphaValue = 1;

            darknessSprite.color = new Color(
                darknessSprite.color.r,
                darknessSprite.color.g,
                darknessSprite.color.b,
                alphaValue);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("GhostPlayer"))
            playerEntered = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if((other.CompareTag("Player") || other.CompareTag("GhostPlayer")) && toggleDarkness)
        {
            playerEntered = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("GhostPlayer"))
            playerEntered = true;
    }
}
