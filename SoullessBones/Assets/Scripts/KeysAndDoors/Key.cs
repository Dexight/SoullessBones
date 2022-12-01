using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    [SerializeField] private Sprite replaceSprite;
    [SerializeField] private string keyId;
    public enum KeyType
    {
        Gold,
        Red
    }
    private void Start()
    {
        if (keyId == "level_01" && SceneStats.level01KeyTaken)
            DeleteKey();
        if (keyId == "level_07" && SceneStats.level07KeyTaken)
            DeleteKey();
    }
    public void DeleteKey()
    {
        Destroy(GetComponent<BoxCollider2D>());
        GetComponent<SpriteRenderer>().sprite = replaceSprite;
        if (keyId == "level_01")
            SceneStats.level01KeyTaken = true;
        if (keyId == "level_07")
            SceneStats.level07KeyTaken = true;
        Destroy(GetComponent<Key>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BoxCollider2D>() == MovementController.instance.GetComponent<BoxCollider2D>())
        if(collision.GetComponent<MovementController>())
        {
            collision.GetComponent<KeyHolder>().AddKey(keyType);
            DeleteKey();
        }
    }
}
