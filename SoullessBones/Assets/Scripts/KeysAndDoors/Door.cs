using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Key.KeyType lockType;
    private Animator _anim;
    [SerializeField] private string doorID;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        if (SceneStats.stats.Contains(doorID))
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BoxCollider2D>() == MovementController.instance.GetComponent<BoxCollider2D>())
        {
            KeyHolder holder = collision.GetComponent<KeyHolder>();
            if (holder != null)
            {
                if (holder.ContainsKey(lockType))
                {
                    holder.RemoveKey(lockType);
                    OpenDoor();
                }
            }
        }
    }

    private void OpenDoor()
    {
        _anim.SetBool("Open", true);
        SceneStats.stats.Add(doorID);
        StartCoroutine(Delete());
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);    
    }
}
