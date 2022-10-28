using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword;

    void Start()
    {
        if(Singleton.instance.scenePassword == entrancePassword || Singleton.instance.scenePassword == "after_development")
        {
            Singleton.instance.transform.position = transform.position;//transform.position наша точка спавна
            //Debug.Log("ENTER");
        }
        else
        {
            Debug.Log("WrongPw(Something went wrong)");
        }
    }

    void Update()
    {
        
    }
}
