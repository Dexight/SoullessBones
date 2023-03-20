using Ink;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyUI : MonoBehaviour
{
    [SerializeField] private GameObject Sprite;
    public bool isTaken = false;

    private void Awake()
    {
        CheckFlyState();
    }

    private void CheckFlyState()
    {
        isTaken = SceneStats.stats.Contains("Fly") && !SceneStats.stats.Contains("Altar");
        Sprite.SetActive(isTaken);
    }

    public void Taked()
    {
        SceneStats.stats.Add("Fly");
        CheckFlyState();
    }

    public void Give()
    {
        CheckFlyState();
    }
}
