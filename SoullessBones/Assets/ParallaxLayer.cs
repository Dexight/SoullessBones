using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    Transform m_target;
    [SerializeField] float m_YScrollSpeed;
    [SerializeField] float m_XScrollSpeed;
    private void Start()
    {
        m_target = Camera.main.transform;
    }
    private void Update()
    {
        transform.position = new Vector2(m_target.position.x * m_XScrollSpeed, m_target.position.y * m_YScrollSpeed);
    }
}
