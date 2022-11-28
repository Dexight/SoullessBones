using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// скрипт для кнопок-читов 
/// </summary>
public class Cheats : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    [SerializeField] GameManager gameManager;
    private void Awake()
    {
         gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    
    public void GodModOnOff() 
    {
        Debug.Log("GodModOnOff called");
        gameManager.changeGodMod();
    }
    public void DoubleJumpOnOff()
    {
        Debug.Log("DoubleJubmpOnOff called");
        gameManager.EnableOrDisableDoubleJumping();
    }

    public void WallJumpOnOff()
    {
        Debug.Log("WallJumpOnOff called");
        gameManager.EnableOrDisableWallJumping();
    }
    public void AstralOnOff()
    {
        Debug.Log("AstralOnOff called");
        gameManager.EnableOrDisableAstral();
    }
    public void SuperDamageOnOff()
    {
        Debug.Log("SuperDamageOnOff called");
        if(gameManager.damage == 5)
            gameManager.changeDamage(1000);
        else 
            gameManager.changeDamage(5);
    }

}
