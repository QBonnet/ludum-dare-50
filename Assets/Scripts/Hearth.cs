using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour
{
    [SerializeField] private float m_value;
    [SerializeField] private float m_timeRespawn;
    [SerializeField] private float m_timeRespawnIncrease;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Collider2D m_collider;
    [SerializeField] private AudioClip m_audioClip;
    [SerializeField] private AudioSource m_audioSource;

    private Player m_player1;
    private Player m_player2;

    // Start is called before the first frame update
    void Start()
    {
        m_player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        m_player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            m_player1.IncreaseHealth(m_value);
            StartCoroutine(DelayRespawn());
            m_audioSource.PlayOneShot(m_audioClip);
        }
        else if (collision.CompareTag("Player2"))
        {
            m_player2.IncreaseHealth(m_value);
            StartCoroutine(DelayRespawn());
            m_audioSource.PlayOneShot(m_audioClip);
        }
    }

    private IEnumerator DelayRespawn()
    {
        m_collider.enabled = false;
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(m_timeRespawn);
        m_collider.enabled = true;
        m_spriteRenderer.enabled = true;
        m_timeRespawn += m_timeRespawnIncrease;
    }
}