using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterDeath : MonoBehaviour
{
    static private UnityEngine.Object explosion;

    void Update()
    {
        
    }

    static public void Death()
    {

        //DeathExplosion
        //explosion = Resources.Load("Explosion");
        //GameObject explosionRef = (GameObject)Instantiate(explosion);
        //explosionRef.transform.position = new Vector3(MovementController.instance.transform.position.x, MovementController.instance.transform.position.y, MovementController.instance.transform.position.z);


        SceneLoader sceneLoader = GameObject.FindGameObjectWithTag("Interface").GetComponent<SceneLoader>();
        //Воспроизвести звук смерти
        SoundVolumeController.PlaySoundEffect(3);
        GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>().StopTime(false);
        //TODO 0 in bottle
        sceneLoader.FadeTo("", true, true, true);
    }
}
