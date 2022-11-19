using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    Transform m_target;
    [SerializeField] float m_YScrollSpeed; //�������� ������ �� ��� �
    [SerializeField] float m_XScrollSpeed;//�������� ������ �� ��� �
    public Vector2 startPosition;
    private void Start()
    {
        m_target = Camera.main.transform;
    }
    private void Update()
    {
        transform.position = new Vector2((startPosition.x + m_target.position.x) * m_XScrollSpeed,(startPosition.y + m_target.position.y) * m_YScrollSpeed);
    }
}   
