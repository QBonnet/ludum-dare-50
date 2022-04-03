using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player m_player1;
    private Player m_player2;

    [SerializeField] private GameObject[] m_backgroundSun;
    [SerializeField] private GameObject m_backgroundNight;
    [SerializeField] private float m_timeBetweenTwoGame;
    [SerializeField] private GameObject m_player1WinText;
    [SerializeField] private GameObject m_player2WinText;

    // Start is called before the first frame update
    void Start()
    {
        m_player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        m_player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player1.IsZombie() || m_player2.IsZombie())
        {
            foreach (var b in m_backgroundSun)
            {
                b.SetActive(false);
            }
            m_backgroundNight.SetActive(true);
        }

        if (!m_player1.IsAlive())
        {
            StartCoroutine(GameOver(m_player2WinText));
        }
        else if (!m_player2.IsAlive())
        {
            StartCoroutine(GameOver(m_player1WinText));
        }
    }

    private IEnumerator GameOver(GameObject text)
    {
        text.SetActive(true);
        yield return new WaitForSeconds(m_timeBetweenTwoGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
