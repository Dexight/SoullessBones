using UnityEngine;

/// <summary>
/// скрипт для кнопок-читов 
/// </summary>
public class Cheats : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] WallJumping wallJumping;
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        wallJumping = GameObject.FindGameObjectWithTag("Player").GetComponent<WallJumping>();
    }
    
    public void GodModOnOff() 
    {
        gameManager.changeGodMod();
    }
    public void DoubleJumpOnOff()
    {
        gameManager.EnableOrDisableDoubleJumping();
    }

    public void WallJumpOnOff()
    {
        wallJumping.isWallSliding = false;
        gameManager.EnableOrDisableWallJumping();
    }
    public void AstralOnOff()
    {
        gameManager.EnableOrDisableAstral();
    }
    public void SuperDamageOnOff()
    {
        if(gameManager.damage == 5)
            gameManager.changeDamage(1000);
        else 
            gameManager.changeDamage(5);
    }

}
