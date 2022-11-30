using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    [SerializeField] private Sprite replaceSprite;
    public enum KeyType
    {
        Gold,
        Red
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<MovementController>())
        {
            collision.GetComponent<KeyHolder>().AddKey(keyType);
            Destroy(GetComponent<BoxCollider2D>());
            GetComponent<SpriteRenderer>().sprite = replaceSprite;
            Destroy(GetComponent<Key>());
        }
    }
}
