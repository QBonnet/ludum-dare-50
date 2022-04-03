using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_maxPosX;
    
    private Vector3 m_startPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * m_speed * Time.deltaTime);
        if (transform.position.x >= m_maxPosX)
        {
            transform.position = m_startPosition;
        }
    }
}
