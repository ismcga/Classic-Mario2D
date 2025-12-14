using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 8f;
    public float jumpForce = 18f;

    [Header("Detección")]
    public Transform feetPos;
    public float checkRadius = 0.4f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Detectamos si tocamos suelo
        // IMPORTANTE: 'groundLayer' debe estar configurado en el Inspector
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);

        // 2. Movimiento Horizontal
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Voltear Sprite
        if (moveInput != 0)
        {
            float scaleX = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(moveInput > 0 ? scaleX : -scaleX, transform.localScale.y, 1);
        }

        // 3. LÓGICA DE SALTO (Estricta de 1 solo salto)
        if (Input.GetButtonDown("Jump"))
        {
            // Solo saltamos si 'isGrounded' es VERDADERO.
            // Al quitar el "|| jumpCount < maxJumps", es imposible saltar en el aire.
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }

    // --- COLISIONES CON BLOQUES ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.6f)
            {
                BlockScript block = collision.gameObject.GetComponent<BlockScript>();
                if (block != null)
                {
                    block.Hit();
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (feetPos != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(feetPos.position, checkRadius);
        }
    }
}