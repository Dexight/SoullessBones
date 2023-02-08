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

    //water in bottle
    private int count = 0; //0 to 100
    public bool isFull = true;
    public bool isIncrementing = true;//наполняется|убавляется
    public bool isEmpty = true;

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
        //Updating counter.
        if (count > 100)
            count = 100;
        if (count < 0)
            count = 0;
        fullBottle.fillAmount = (float)(count/100.0);
        counter.text = "" + count;
        //Updating sprite.
        //as default:
        counter.color = Color.cyan;
        emptyBottle.sprite = emptyBlack;
        isFull = count == 100;
        isEmpty = count == 0;

        //red sprite + red text     (=100 -> decrementing)
        if (isFull)
        {
            isIncrementing = false;            
            counter.color = Color.red;
            emptyBottle.sprite = emptyRed;
        }

        //black sprite + white text (=0 -> incrementing)
        if (isEmpty && !isIncrementing)
        {
            isIncrementing = true;
            counter.color = Color.white;
            emptyBottle.sprite = emptyBlack;
        }

        //black sprite + cyan text  (>0 and <100 and incrementing)
        if (!isFull && !isEmpty)
        {
            if (isIncrementing)
            {
                counter.color = Color.cyan;
                emptyBottle.sprite = emptyBlack;
            }
            else
            {
                counter.color = Color.red;
                emptyBottle.sprite = emptyRed;
            }
        }
    }
}
