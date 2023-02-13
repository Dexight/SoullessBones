using UnityEngine;
public class Exit : MonoBehaviour
{
    bool gone = false;  //нужно для того, чтобы из-за корутин не ломалась логика перехода(не срабатывало дважды)
    public BoxCollider2D trigger;
    [SerializeField] private string nextSceneName;
    [SerializeField] private string newScenePassword;
    private SceneLoader loader;
    void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
        //loader = FindObjectOfType<SceneLoader>();
        loader = GameObject.FindGameObjectWithTag("Interface").GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !gone)
        {
            MovementController.instance.GetComponent<Astral>().timerNull();
                GameManager.instance.scenePassword = newScenePassword;
                loader.FadeTo(nextSceneName, true);
                gone = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(trigger.transform.position, trigger.size);
    }
}
