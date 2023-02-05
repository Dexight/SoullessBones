using UnityEngine;

public class InElevator : MonoBehaviour
{
    [SerializeField] private Sprite OutOfBox;
    [SerializeField] private Sprite inBox;
    [SerializeField] private GameObject box;
    [SerializeField] private SpriteRenderer snail;
    private SpriteRenderer boxSprite;
    
    void Start()
    {
        boxSprite = box.GetComponent<SpriteRenderer>();
        boxSprite.sprite = OutOfBox;
        snail.enabled= false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        boxSprite.sprite = inBox;
        snail.enabled = true;
        MovementController.instance.transform.parent = transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        boxSprite.sprite = OutOfBox;
        snail.enabled = false;
        MovementController.instance.transform.parent = null;
        DontDestroyOnLoad(MovementController.instance);
    }
}
