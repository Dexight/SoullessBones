using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Singleton Variables
    [Header("Objects")]
    public static GameManager instance;
    public GameObject Player;
    public Rigidbody2D rb;
    public GameObject Interface;
    public GameObject timeManager;
    public string scenePassword;//сохраняет строку, когда игрок переходит на другую сцену
    [Header("CheatStats")]
    public bool godMod = false;
    public int damage = 5;
    public bool enableDoubleJumping = false;
    public bool enableWallJumping = false;
    public bool enableAstral = false;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(Interface);
        DontDestroyOnLoad(timeManager);
        DontDestroyOnLoad(gameObject);
        rb = Player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    //функция для изменения godmod
    public void changeGodMod()
    {
        godMod = !godMod;
        Player.GetComponent<HealthSystem>().godMod = godMod;
    }

    //функция для изменения урона
    public void changeDamage(int d)
    {
        Player.GetComponent<AttackSystem>().SlashLeft.GetComponent<Slash>().damage = d;
        Player.GetComponent<AttackSystem>().SlashRight.GetComponent<Slash>().damage = d;
        Player.GetComponent<AttackSystem>().SlashUpLeft.GetComponent<Slash>().damage = d;
        Player.GetComponent<AttackSystem>().SlashUpRight.GetComponent<Slash>().damage = d;
        damage = d;
    }

    public void EnableOrDisableDoubleJumping()
    {
        enableDoubleJumping = !enableDoubleJumping;
        Player.GetComponent<DoubleJumping>().enabled = enableDoubleJumping;
    }
    //only enable DJumping
    public void EnableDoubleJumping()
    {
        if (!enableDoubleJumping)
        {
            EnableOrDisableDoubleJumping();
        }
    }

    public void EnableOrDisableWallJumping()
    {
        enableWallJumping = !enableWallJumping;
        Player.GetComponent<WallJumping>().enabled = enableWallJumping;
    }
    //only enable WJumping
    public void EnableWallJumping()
    {
        if(!enableWallJumping)
        {
            EnableOrDisableWallJumping();
        }
    }

    public void EnableOrDisableAstral()
    {
        enableAstral = !enableAstral;
        Player.GetComponent<Astral>().enabled = enableAstral;
    }
    //only enable Astral
    public void EnableAstral()
    {
        if (!enableAstral)
        {
            EnableOrDisableAstral();
        }
    }
}
