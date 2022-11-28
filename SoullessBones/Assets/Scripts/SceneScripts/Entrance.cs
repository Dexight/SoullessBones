using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword; //������, �� �������� �� ��������� � ����������� ����� � �������.
    private Spikes spikes;
    private void Awake()
    {
        spikes = GameObject.FindGameObjectWithTag("Spikes").GetComponent<Spikes>();
    }

    private void Update()
    {
        if (spikes.teleport)
        {
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().scenePassword == entrancePassword)
            {
                //Debug.Log("Spikes worked");
                GameManager.instance.Player.transform.position = transform.position;//���� ����� ������
                spikes.teleport = false;
            }
        }
    }

    void Start()
    {
        if(GameManager.instance)
            if(GameManager.instance.scenePassword == entrancePassword)
            {
                GameManager.instance.Player.transform.position = transform.position;//���� ����� ������
            }
    }
}
