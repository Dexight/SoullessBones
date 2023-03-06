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
    public GameObject DistAttack;
    #endregion
    #region Cheats Variables
    [Header("CheatStats")]
    private AttackSystem attackSystem;
    private DistanceAttack BottleUI;
    public int damage;
    public int damageDist;
    public int bottleFill;
    public bool godMod;
    public bool enableDoubleJumping;
    public bool enableWallJumping;
    public bool enableAstral;
    public bool enableDistanceAttacks;
    public bool fullBottle;
    #endregion
    public string scenePassword;//сохраняет, когда игрок переходит на другую сцену
    public string currentScene;
    public string lastSave;
    private void Awake()
    {
        attackSystem = Player.GetComponent<AttackSystem>();
        BottleUI = DistAttack.GetComponent<DistanceAttack>();
        Interface = GameObject.FindGameObjectWithTag("Interface");
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
        damageDist = 15;
        bottleFill = 4;
    }

    private void Start()
    {
        godMod = Player.GetComponent<HealthSystem>().godMod;
        enableDoubleJumping = Player.GetComponent<DoubleJumping>().enabled;
        enableWallJumping = Player.GetComponent<WallJumping>().enabled;
        enableAstral = Player.GetComponent<Astral>().enabled;
        enableDistanceAttacks = attackSystem.distanceUnlock;
        if(BottleUI)
            fullBottle = BottleUI.alwaysFull;
        else
            fullBottle= false;
    }

    /// <summary>
    /// Fast Save
    /// </summary>
    public void Save()
    {
        //scene stats
        SceneStats.EnterPassword = scenePassword;
        SceneStats.curScene = currentScene;
        SceneStats.lastSave = lastSave;

        //player stats
        SceneStats.doubleJump = enableDoubleJumping;
        SceneStats.wallJump = enableWallJumping;
        SceneStats.astral = enableAstral;
        SceneStats.distanceAttacks = enableDistanceAttacks;

        //hp & tears
        SceneStats.hp = Player.GetComponent<HealthSystem>().health;
        var distAttack = SceneLoader.instance.GetComponentInChildren<DistanceAttack>();
        SceneStats.tears = distAttack.getCount();
        SceneStats.isFull = distAttack.getIsFull();
        SceneStats.isEmpty = distAttack.getIsEmpty();
        SceneStats.isIncrementing = distAttack.getIsIncrementing();
    }

    public void LoadSave()
    {

    }

    //-----------------------------------------------------------------------------х
    //Ниже функции вкл\выкл состояние + функции для вкл. состояний по лору игры
    //--------------------------------------------------------------------------х

    //функция для изменения godmod
    public void changeGodMod()
    {
        godMod = !godMod;
        Player.GetComponent<HealthSystem>().godMod = godMod;
    }

    //функция для изменения урона
    public void changeDamage(int d)
    {
        if (!(damage > 0))
            damage = 1;
        else
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
            EnableOrDisableDoubleJumping();
        }
    }

    //WALL JUMPING
    public void EnableOrDisableWallJumping()
    {
        enableWallJumping = !enableWallJumping;
        Player.GetComponent<WallJumping>().enabled = enableWallJumping;
    }

    //only enable WJumping
    public void EnableWallJumping()
    {
        if (!enableWallJumping)
        {
            EnableOrDisableWallJumping();
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
            EnableOrDisableAstral();
        }
    }

    // Unlock/Lock DistAttacks
    public void EnableOrDisableDistanceAttack()
    {
        if(enableDistanceAttacks)
        {
            attackSystem.OnDistanceLock();
        }
        else
        { attackSystem.OnDistanceUnlock();}
        enableDistanceAttacks = !enableDistanceAttacks;
    }

    public void EnableDistanceAttack()
    {
        if(!enableDistanceAttacks)
        {
            EnableOrDisableDistanceAttack();
        }
    }

    //always full bottle
    public void changeFullBottle()
    {
        if(enableDistanceAttacks)//if unlocked distance attacks
        {
            fullBottle = !fullBottle;
            if (!fullBottle) { BottleUI.minusTears(100); }
            BottleUI.alwaysFull = fullBottle;
            BottleUI.updateBottle();
        }
        else { fullBottle = false; }
    }

    //distance damage changing
    public void changeDamageDist(int d)
    {
        if (!(d > 0))
            damageDist = 1;
        else
            damageDist = d;
    }

    //change power of filling bottle
    public void changeBottleFill(int fill)
    {
        if(!(fill > 0))
            bottleFill = 1;
        else
           bottleFill = fill;
    }
}