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
        explosion = Resources.Load("PlayerExplosion");
        GameObject explosionRef = (GameObject)Instantiate(explosion);
        explosionRef.transform.position = new Vector3(MovementController.instance.transform.position.x, MovementController.instance.transform.position.y, MovementController.instance.transform.position.z);
        Destroy(explosionRef, 1.0f);

        SceneLoader sceneLoader = GameObject.FindGameObjectWithTag("Interface").GetComponent<SceneLoader>();
        //Воспроизвести звук смерти
        SoundVolumeController.PlaySoundEffect(3);
        SceneStats.destroyedEnemies.Clear();
        GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>().StopTime(false);
        sceneLoader.FadeTo("", true, true, true);
    }
}
