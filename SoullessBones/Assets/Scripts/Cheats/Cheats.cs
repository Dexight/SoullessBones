using Newtonsoft.Json.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ������ ��� ������-����� 
/// </summary>
public class Cheats : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] WallJumping wallJumping;
    [SerializeField] TMP_InputField StrokeDamageField;
    [SerializeField] TMP_InputField DistDamageField;


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

    public void DistanceAttacksOnOff()
    {
        gameManager.EnableOrDisableDistanceAttack();
    }

    public void FullBottle()
    { 
        gameManager.changeFullBottle();
    }

    public void DistanceDamage()
    {
        int i = 1;
        if (int.TryParse(DistDamageField.text, out i))
        {
            var damage = int.Parse(DistDamageField.text);
            //gameManager.changeDamage(damage);
            gameManager.changeDamageDist(damage);
        }
        Debug.Log($"gameManager.damageDist = {gameManager.damageDist}");
    }

    public void StrokeDamage()
    {
        int i = 1;
        if(int.TryParse(StrokeDamageField.text, out i))
        {
            var damage = int.Parse(StrokeDamageField.text);
            gameManager.changeDamage(damage);
        }
        Debug.Log($"gameManager.damage = {gameManager.damage}");

    }

    public void BottleFilling(int power)
    {
        gameManager.changeBottleFill(power);
    }
}