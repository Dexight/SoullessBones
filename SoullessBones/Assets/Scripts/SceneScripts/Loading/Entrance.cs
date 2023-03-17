using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword; //Пароль, по которому мы спавнимся у конкретного входа в локацию.
    private Spikes spikes;
    private void Awake()
    {
        spikes = GameObject.FindGameObjectWithTag("Spikes").GetComponent<Spikes>();
    }

    private void Update()
    {
        if (spikes.teleport)
        {
            if (GameManager.instance.enterPassword == entrancePassword)
            {
                //Debug.Log("Spikes worked");
                GameManager.instance.Player.transform.position = transform.position;//наша точка спавнасле шипов
                spikes.teleport = false;
            }
        }
    }

    void Start()
    {
        if(GameManager.instance)
            if(GameManager.instance.enterPassword == entrancePassword)
            {
                GameManager.instance.Player.transform.position = transform.position;//наша точка спавна
            }
    }
}
