using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_health;
    [SerializeField] private float m_rateTransform;

    [SerializeField] private bool m_isZombie = false;
    private bool m_isAlive = true;

    [SerializeField] private Animator m_animator;

    [SerializeField] private SliderBar m_healthSlider;
    [SerializeField] private SliderBar m_transformSlider;
    [SerializeField] private Player m_otherPlayer;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    private bool m_isInvincible = false;
    [SerializeField] private float m_invicibilityFlashDelay;
    [SerializeField] private float m_invicilityTime;
    [SerializeField] private float m_damage;

    // Start is called before the first frame update
    void Start()
    {
        m_healthSlider.SetValue(m_health);
        m_transformSlider.SetValue(m_rateTransform);
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        //IncreaseRateZombieTransform(0.015f);
    }

    private void FixedUpdate()
    {
        IncreaseRateZombieTransform(0.1f);
        if (m_isZombie)
        {
            m_health -= 0.1f;
            m_healthSlider.SetValue(m_health);
        }
    }

    public bool IsAlive()
    {
        return m_isAlive;
    }

    private void IncreaseRateZombieTransform(float i)
    {
        m_rateTransform += i;
        m_transformSlider.SetValue(m_rateTransform);
        if (m_rateTransform > 100)
        {
            m_rateTransform = 100;
        }
    }

    public void DecreaseRateZombieTransform(float i)
    {
        m_rateTransform -= i;
        m_transformSlider.SetValue(m_rateTransform);
        if (m_rateTransform < 0)
        {
            m_rateTransform = 0;
        }
    }

    public void IncreaseHealth(float i)
    {
        m_health += i;
        m_healthSlider.SetValue(m_health);
        if (m_health > 100)
        {
            m_health = 100;
        }
    }

    public bool IsZombie()
    {
        return m_isZombie;
    }

    public void DecreaseHealth(float i)
    {
        if (!m_isInvincible)
        {
            m_isInvincible = true;
            m_health -= i;
            m_healthSlider.SetValue(m_health);
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvicibilityDelay(m_invicilityTime));
        }
    }

    private void CheckStatus()
    {
        if (m_health <= 0)
        {
            m_isAlive = false;
            gameObject.SetActive(false);
        }
        if (m_rateTransform >= 100)
        {
            m_isZombie = true;
            m_animator.SetBool("IsZombie", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() == m_otherPlayer && m_isZombie)
        {
            m_otherPlayer.DecreaseHealth(m_damage);
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (m_isInvincible)
        {
            m_spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(m_invicibilityFlashDelay);
            m_spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(m_invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay(float time)
    {
        yield return new WaitForSeconds(time);
        m_isInvincible = false;
    }
}
