using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 3f;
    public Color flippedColor = new Color(0.5f, 0.5f, 1f);

    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private int direction = 1;
    public bool isFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        direction = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    void Update()
    {
        if (isFlipped)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (direction > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    public void Flip()
    {
        if (isFlipped) return;
        isFlipped = true;
        transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), 1);
        spriteRend.color = flippedColor;
        rb.linearVelocity = new Vector2(0, 5f);
        Invoke("Recover", 8f);
    }

    void Recover()
    {
        if (isFlipped)
        {
            isFlipped = false;
            spriteRend.color = Color.white;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Pipe"))
        {
            direction *= -1;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (isFlipped)
            {
                // A) EL JUGADOR MATA AL ENEMIGO -> Sumar Puntos
                if (Contador.instance != null) // CAMBIO AQUÍ: Usamos Contador
                {
                    Contador.instance.AddScore(100);
                }

                Destroy(gameObject);
            }
            else
            {
                // B) EL ENEMIGO MATA AL JUGADOR -> Game Over
                Destroy(collision.gameObject);

                if (Contador.instance != null) // CAMBIO AQUÍ: Usamos Contador
                {
                    Contador.instance.GameOver();
                }
            }
        }
    }
}