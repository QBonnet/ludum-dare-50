using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject m_objectToDestroy;

    [SerializeField] private Collider2D m_collider;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private float m_timeToRespawnr;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            StartCoroutine(Respawn());
        }
        
    }

    private IEnumerator Respawn()
    {
        m_collider.enabled = false;
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(m_timeToRespawnr);
        m_collider.enabled = true;
        m_spriteRenderer.enabled = true;
    }
}
