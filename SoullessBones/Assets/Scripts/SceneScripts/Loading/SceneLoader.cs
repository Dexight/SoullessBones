using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneLoader : MonoBehaviour
{
    public Image blackImage;
    [SerializeField] private float alpha;
    [SerializeField] private GameObject Player;
    private Astral astral;
    public static SceneLoader instance;

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

        Player = GameObject.FindGameObjectWithTag("Player");
        astral = Player.GetComponent<Astral>();
    }
    
    public void FadeTo(string sceneName, bool load, bool isSave, bool isDead)
    {
        astral.canUseAstral = false;
        MovementController.instance.canJumpDown = false;
        StartCoroutine(FadeOut(sceneName, load, isSave, isDead));
    }

    public IEnumerator FadeIn(bool isSave)
    {
        alpha = 1;
        if (isSave)
        {
            FountainSystem.Save();
            GameManager.instance.timeManager.GetComponent<TimeManager>().ContinueTime();    
            var hp = MovementController.instance.GetComponent<HealthSystem>();
            hp.health = hp.numOfHearts;
        }
        while (alpha > 0)
        {
            GameManager.instance.rb.velocity = new Vector2(0, GameManager.instance.rb.velocity.y);
            GameManager.instance.Player.GetComponent<Animator>().SetBool("isRunning", false);
            alpha -= Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }
        alpha = 0;
        GameManager.instance.Player.GetComponent<MovementController>()._CanMove = true;
        astral.canUseAstral = true;
        MovementController.instance.canJumpDown = true;
        MovementController.instance.GetComponent<HealthSystem>().SetDead(false);
    }

    private IEnumerator FadeOut(string sceneName, bool load, bool isSave, bool isDead)
    {
        GameManager.instance.Player.GetComponent<MovementController>()._CanMove = false;
        GameManager.instance.Player.GetComponent<HealthSystem>().loading = true;

        alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }
        alpha = 1;
        if (load)
        {
            if (isDead)
                StartCoroutine(DeathLoading());
            else
                StartCoroutine(Loading(sceneName));
            GameManager.instance.Save();
            StartCoroutine(FadeIn(isSave));
        }
        else
        {
            //наткнулись на шипы
            Spikes spikes = GameObject.FindGameObjectWithTag("Spikes").GetComponent<Spikes>();
            spikes.blackBackground = true;
            spikes.teleport = true;
            spikes.isTouched = false;
        }
    }
    private IEnumerator Loading(string sceneName)
    {
        SoundVolumeController.LoadToScene(sceneName);
        SceneManager.LoadScene(sceneName);
        yield return true;
        GameObject vcam = GameObject.FindGameObjectWithTag("PlayerCamera");
        GameManager.instance.Player.GetComponent<HealthSystem>().loading = false;

        if (vcam)
            vcam.GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
    }

    private IEnumerator DeathLoading()
    {
        if (SceneStats.lastSave != "start")
        {
            SceneManager.LoadScene(SceneStats.lastSave);
            GameManager.instance.enterPassword = "save";
            GameManager.instance.currentScene = GameManager.instance.lastSave;
        }
        else 
        {
            SceneManager.LoadScene("Hub Scene"); 
            GameManager.instance.enterPassword = "start";
            GameManager.instance.currentScene = "Hub Scene" ;
        }
        yield return true;
        GameManager.instance.Player.GetComponent<HealthSystem>().loading = false;
        GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
    }
}

