using System.Collections;
using UnityEngine;

public class DeletePortal : MonoBehaviour
{
    public float timeOfEnterAndExit;
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(deletePortal());
    }

    private IEnumerator deletePortal()
    {
        yield return new WaitForSeconds(timeOfEnterAndExit*5);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.1f);
        yield return new WaitForSeconds(timeOfEnterAndExit);
        Destroy(gameObject);
    }
}
