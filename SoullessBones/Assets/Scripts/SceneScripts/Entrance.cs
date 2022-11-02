using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword; //Пароль, по которому мы спавнимся у конкретного входа в локацию.

    void Start()
    {
        if(GameManager.instance)
            if(GameManager.instance.scenePassword == entrancePassword || GameManager.instance.scenePassword == "after_development")
            {
                GameManager.instance.Player.transform.position = transform.position;//наша точка спавна
            }
    }
}
