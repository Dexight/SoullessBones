using UnityEngine;

public class Spikes : MonoBehaviour
{
    SceneLoader sceneLoader;
    public bool blackBackground = false;
    public bool teleport = false;
    private void Awake()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("Interface").GetComponent<SceneLoader>();
    }
    private void Update()
    {
        if (sceneLoader.blackImage.color.a >= 0.9 && blackBackground)
        {
            StartCoroutine(sceneLoader.FadeIn());
            blackBackground = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetType() == typeof(BoxCollider2D))
            if(collision.gameObject.GetComponent<MovementController>() != null)
            {
                collision.gameObject.GetComponent<HealthSystem>().TakeDamage(2);
                sceneLoader.FadeTo("", false);
                //TODO teleport
                //blackBackground = true;
            }
    }
}
