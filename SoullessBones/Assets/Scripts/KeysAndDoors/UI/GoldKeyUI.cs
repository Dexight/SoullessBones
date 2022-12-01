using TMPro;
using UnityEngine;

public class GoldKeyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] GameObject sprite;
    [SerializeField] GameObject counter;
    bool hide = true;
    int count = 0;
    void Start()
    {
        textMeshPro = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = "" + 0;
        sprite.SetActive(false);
        counter.SetActive(false);
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
}
