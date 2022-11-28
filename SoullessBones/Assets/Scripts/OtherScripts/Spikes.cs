using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    TimeManager timeManager;
    SceneLoader sceneLoader;
    public bool blackBackground = false;
    public bool teleport = false;
    public bool isTutorial;
    private void Awake()
    {
        isTutorial = SceneManager.GetActiveScene().name == "Hub Scene";
        sceneLoader = GameObject.FindGameObjectWithTag("Interface").GetComponent<SceneLoader>();
        timeManager = GameObject.FindWithTag("TimeManager").GetComponent<TimeManager>();
    }
    private void Update()
    {
        if (sceneLoader.blackImage.color.a >= 0.9 && blackBackground)
        {
            timeManager.ContinueTime();
            StartCoroutine(sceneLoader.FadeIn());
            blackBackground = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType() == typeof(BoxCollider2D))
            if (collision.gameObject.GetComponent<MovementController>() != null)
            {
                if(!isTutorial)
                    collision.gameObject.GetComponent<HealthSystem>().TakeDamage(2);
                timeManager.StopTime(false);
                sceneLoader.FadeTo("", false);
            }
    }
}
