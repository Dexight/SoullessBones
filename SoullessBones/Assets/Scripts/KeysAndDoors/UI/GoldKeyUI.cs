using TMPro;
using UnityEngine;

public class GoldKeyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] GameObject sprite;
    [SerializeField] GameObject counter;
    bool hide = true;
    public int count = 0;       //просто UI счётчик

    void Start()
    {
        count = SceneStats.keycounter;
        textMeshPro = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = "" + count;
        UpdateCounter();
    }

    public void AddKey()
    {
        count++;
        textMeshPro.text = "" + count;
        if(hide)
        {
            sprite.SetActive(true);
            counter.SetActive(true);
            hide = false;
        }
    }

    public void RemoveKey()
    {
        count--;
        if (count < 0)
            count = 0;

        if (count == 0)
        {
            sprite.SetActive(false);
            counter.SetActive(false);
            hide = true;
        }
        textMeshPro.text = "" + count;
    }

    public void UpdateCounter()
    {
        if (count > 0 && hide)
        {
            sprite.SetActive(true);
            counter.SetActive(true);
            hide = false;
            textMeshPro.text = "" + count;
        }
        else
        {
            sprite.SetActive(false);
            counter.SetActive(false);
        }
    }
}
