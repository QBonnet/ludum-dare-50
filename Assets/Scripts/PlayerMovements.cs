using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public bool m_isPlayer1;
    // Deplacements
    public float m_moveSpeed;
    public float m_jumpForce;
    public Rigidbody2D m_rb;
    private Vector3 m_velocity = Vector3.zero;
    private float m_horizontalMovement;
    private float m_smoothTime = 0.05f;

    // Saut
    private bool m_isJumping = false;
    private bool m_isGrounded = true;
    public Transform m_groundCheck;
    public float m_groundCheckRadius;
    public LayerMask m_collisionLayer;

    // Animations
    public Animator m_animator;
    public SpriteRenderer m_spriteRenderer;

    public Collider2D m_collider;

    void FixedUpdate()
    {
        //m_isGrounded = Physics2D.OverlapArea(m_groundCheckLeft.position, m_groundCheckRight.position);
        if (m_isPlayer1)
        {
            m_horizontalMovement = Input.GetAxis("HorizontalPlayer1") * m_moveSpeed * Time.deltaTime;
        }
        else
        {
            m_horizontalMovement = Input.GetAxis("HorizontalPlayer2") * m_moveSpeed * Time.deltaTime;
        }

        Move(m_horizontalMovement);

        Flip(m_rb.velocity.x);

        float characterVelocity = Mathf.Abs(m_rb.velocity.x);
        m_animator.SetFloat("Speed", characterVelocity);
    }

    private void Update()
    {
        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_collisionLayer);
        if (m_isPlayer1 && Input.GetKeyDown(KeyCode.RightControl) && m_isGrounded)
        {
            m_isJumping = true;
        }
        else if (!m_isPlayer1 && Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_isJumping = true;
        }
    }

    void Move(float horizontalMovement)
    {

        Vector3 targetVelocity = new Vector2(horizontalMovement, m_rb.velocity.y);
        m_rb.velocity = Vector3.SmoothDamp(m_rb.velocity, targetVelocity, ref m_velocity, m_smoothTime);

        if (m_isJumping)
        {
            m_rb.AddForce(new Vector2(0f, m_jumpForce));
            m_isJumping = false;
        }


    }

    void Flip(float velocity)
    {
        if (velocity > 0.1f)
        {
            m_spriteRenderer.flipX = false;
        }
        else if (velocity < -0.1f)
        {
            m_spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(m_groundCheck.position, m_groundCheckRadius);
    }

}
