using UnityEngine;
public class GameManager : MonoBehaviour
{
    #region Singleton Variables
    [Header("Objects")]
    public static GameManager instance;
    public GameObject Player;
    public Rigidbody2D rb;
    public GameObject Interface;
    public GameObject timeManager;
    #endregion
    #region Other Variables
    public string scenePassword;//сохраняет строку, когда игрок переходит на другую сцену
    [Header("CheatStats")]
    public bool godMod;
    public int damage;
    public bool enableDoubleJumping;
    public bool enableWallJumping;
    public bool enableAstral;
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
        damage = 5;        
    }

    private void Start()
    {
        godMod = Player.GetComponent<HealthSystem>().godMod;
        enableDoubleJumping = Player.GetComponent<DoubleJumping>().enabled;
        enableWallJumping = Player.GetComponent<WallJumping>().enabled;
        enableAstral = Player.GetComponent<Astral>().enabled;
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
        damage = d;
    }
    //DOUBLE JUMPING
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
            //Debug.Log("DJ Added");
            EnableOrDisableDoubleJumping();
        }
        else
        {
            //Debug.Log("DJ already added");
        }
    }
    //WALL JUMPING
    public void EnableOrDisableWallJumping()
    {
        //Debug.Log("WS instance");
        enableWallJumping = !enableWallJumping;
        Player.GetComponent<WallJumping>().enabled = enableWallJumping;
    }
    //only enable WJumping
    public void EnableWallJumping()
    {
        if (!enableWallJumping)
        {
            //Debug.Log("WJ Added");
            EnableOrDisableWallJumping();
        }
        else
        {
            //Debug.Log("WJ already added");
        }
    }
    //ASTRAL
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
            //Debug.Log("Astral Added");
            EnableOrDisableAstral();
        }
        else
        {
            //Debug.Log("Astral already added");
        }
    }
}
