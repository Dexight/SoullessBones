using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool TimeIsStopped;
    private MovementController movementController;
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
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
    }
    public void ContinueTime()
    {
        TimeIsStopped = false;

        var objects = FindObjectsOfType<TimeBody>();  //������� ������ ������ � ����������� TimeBody
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<TimeBody>().ContinueTime(); //���������� ����� ������� �������
        }
    }
    public void StopTime()
    {
        TimeIsStopped = true;
        movementController._CanMove = false;
    }
}
