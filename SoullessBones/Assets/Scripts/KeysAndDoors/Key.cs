using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    [SerializeField] private Sprite replaceSprite;
    [SerializeField] private string keyId;

    public enum KeyType
    {
        Gold,
        Red // to use in future (may be)
    }

    private void Start()
    {
        if (SceneStats.stats.Contains(keyId))
            DeleteKey();
    }

    public void DeleteKey()
    {
        Destroy(GetComponent<BoxCollider2D>());
        GetComponent<SpriteRenderer>().sprite = replaceSprite;
        SceneStats.stats.Add(keyId);
        Destroy(GetComponent<Key>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovementController>() && !SceneStats.stats.Contains(keyId))
        {
            collision.GetComponent<KeyHolder>().AddKey(keyType);
            DeleteKey();
            SoundVolumeController.PlaySoundEffect2(4);
        }
    }
}
