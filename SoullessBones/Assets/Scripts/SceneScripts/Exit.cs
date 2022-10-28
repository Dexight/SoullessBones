using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    bool gone = false;  //нужно для того, чтобы из-за корутин не ломалась логика перехода(не срабатывало дважды)
    public BoxCollider2D trigger;
    [SerializeField] private string sceneName;
    [SerializeField] private string newScenePassword;
    void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !gone)
        {
            Singleton.instance.scenePassword = newScenePassword;
            FindObjectOfType<SceneFader>().FadeTo(sceneName, true);
            gone = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(trigger.transform.position, trigger.size);
    }
}
