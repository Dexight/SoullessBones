using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool GrayBack;
    public bool TimeIsStopped;
    private GameObject Player;
    private static TimeManager instance; //������ ���� �� ����� (��������)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void ContinueTime()
    {
        TimeIsStopped = false;
        Player.GetComponent<AttackSystem>().inAstral = false;
        GrayBack = false;
        var objects = FindObjectsOfType<TimeBody>();  //������� ������ ������ � ����������� TimeBody
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<TimeBody>().ContinueTime(); //���������� ����� ������� �������
        }
    }
    public void StopTime(bool isAstral)
    {
        GrayBack = isAstral;
        TimeIsStopped = true;
        Player.GetComponent<MovementController>()._CanMove = false;
        Player.GetComponent<AttackSystem>().inAstral = true;
    }
}
