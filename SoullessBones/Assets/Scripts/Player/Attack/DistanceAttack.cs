using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Distance Attack UI
/// </summary>
public class DistanceAttack : MonoBehaviour
{
    [SerializeField] private Image fullBottle;
    [SerializeField] private Image emptyBottle;
    [SerializeField] private Sprite emptyBlack;
    [SerializeField] private Sprite emptyRed;
    [SerializeField] private TextMeshProUGUI counter;

    public bool isFull;
    private int count = 0; //0 to 100

    private void Start()
    {
        isFull = false; 
        emptyBottle.sprite = emptyBlack;
        emptyBottle.fillAmount = 1;
        updateBottle();
    }

    public void fillBottle(int n)
    {
        count += n;
        updateBottle();
    }

    public void minusTears(int n)
    {
        count-= n;
        updateBottle();
    }

    private void updateBottle()
    {
        if (count > 100)
            count = 100;
        if (count < 0)
            count = 0;

        fullBottle.fillAmount = (float)(count/100.0);

        counter.color = Color.cyan;
        isFull = false;
        emptyBottle.sprite = emptyBlack;

        if (count == 0)
        {
            counter.color = Color.white;
        }
        else if (count == 100)
        {
            isFull = true;
            counter.color = Color.red;
            emptyBottle.sprite = emptyRed;
        }

        counter.text = "" + count;
    }
}
