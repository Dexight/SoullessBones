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
    
    public void FadeTo(string sceneName, bool load)
    {
        astral.canUseAstral = false;
        MovementController.instance.canJumpDown = false;
        StartCoroutine(FadeOut(sceneName, load));
    }

    public IEnumerator FadeIn()
    {
        alpha = 1;
        while(alpha > 0)
        {
            GameManager.instance.rb.velocity = new Vector2(0, GameManager.instance.rb.velocity.y);
            GameManager.instance.Player.GetComponent<Animator>().SetBool("isRunning", false);
            alpha -= Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }
        GameManager.instance.Player.GetComponent<MovementController>()._CanMove = true;
        astral.canUseAstral = true;
        MovementController.instance.canJumpDown = true;
    }

    private IEnumerator FadeOut(string sceneName, bool load)
    {
        GameManager.instance.Player.GetComponent<MovementController>()._CanMove = false;
        alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }

        if (load)
        {
            StartCoroutine(Loading(sceneName));
            StartCoroutine(FadeIn());
        }
        else
        {
            Spikes spikes = GameObject.FindGameObjectWithTag("Spikes").GetComponent<Spikes>();
            spikes.blackBackground = true;
            spikes.teleport = true;
            spikes.isTouched = false;
        }
    }
    private IEnumerator Loading(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        yield return true;
        GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
    }
}

