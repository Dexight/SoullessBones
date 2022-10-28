using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneFader : MonoBehaviour
{
    public Image blackImage;
    [SerializeField]private float alpha;

    private void Start()
    {
    }
    
    public void FadeTo(string sceneName, bool load)
    {
        StartCoroutine(FadeOut(sceneName, load));
    }

    private IEnumerator FadeIn()
    {
        alpha = 1;
        while(alpha > 0)
        {
            alpha -= Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator FadeOut(string sceneName, bool load)
    {
        alpha = 0;
        while(alpha < 1)
        {
            alpha += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }
        if (load)
            SceneManager.LoadScene(sceneName);
        StartCoroutine(FadeIn());
    }
}

