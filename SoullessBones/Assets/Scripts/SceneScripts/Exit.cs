using UnityEngine;
public class Exit : MonoBehaviour
{
    bool gone = false;  //нужно для того, чтобы из-за корутин не ломалась логика перехода(не срабатывало дважды)
    public BoxCollider2D trigger;
    [SerializeField] private string sceneName;
    [SerializeField] private string newScenePassword;
    private SceneLoader loader;
    void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
        loader = FindObjectOfType<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !gone)
        {
                GameManager.instance.scenePassword = newScenePassword;
                loader.FadeTo(sceneName, true);
                gone = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(trigger.transform.position, trigger.size);
    }
}
