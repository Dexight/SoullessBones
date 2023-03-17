using UnityEngine;
public class Exit : MonoBehaviour
{
    public bool gone = false;  //нужно для того, чтобы из-за корутин не ломалась логика перехода(не срабатывало дважды)
    public BoxCollider2D trigger;
    [SerializeField] private string nextSceneName;
    [SerializeField] private string newScenePassword;
    private SceneLoader loader;

    void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
        loader = GameObject.FindGameObjectWithTag("Interface").GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !gone)
        {
            MovementController.instance.GetComponent<Astral>().timerNull();
            GameManager.instance.enterPassword = newScenePassword;
            GameManager.instance.currentScene = nextSceneName;
            GameManager.instance.Save();
            Debug.Log("FastSave");
            loader.FadeTo(nextSceneName, true, false, false);
            gone = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(trigger.transform.position, trigger.size);
    }
}
