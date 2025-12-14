using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 4f;

    private Vector3 originalPos;
    private bool isBouncing = false;

    void Start()
    {
        originalPos = transform.position;
    }

    public void Hit()
    {
        if (!isBouncing)
        {
            StartCoroutine(BounceRoutine());
        }
    }

    IEnumerator BounceRoutine()
    {
        isBouncing = true;

        Vector3 targetPos = originalPos + Vector3.up * bounceHeight;
        while (transform.position.y < targetPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, bounceSpeed * Time.deltaTime);
            yield return null;
        }

        KillEnemyOnTop();

        while (transform.position.y > originalPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, bounceSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = originalPos;
        isBouncing = false;
    }

    void KillEnemyOnTop()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            Vector2 topCenter = new Vector2(col.bounds.center.x, col.bounds.max.y + 0.25f);
            Vector2 boxSize = new Vector2(col.bounds.size.x * 0.9f, 0.5f);

            Collider2D[] enemies = Physics2D.OverlapBoxAll(topCenter, boxSize, 0f, enemyLayer);

            foreach (Collider2D enemy in enemies)
            {
                // CAMBIO AQUÍ: Usamos Contador en lugar de GameManager
                if (Contador.instance != null)
                {
                    Contador.instance.AddScore(100);
                }

                Destroy(enemy.gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            Gizmos.color = Color.red;
            Vector2 topCenter = new Vector2(col.bounds.center.x, col.bounds.max.y + 0.25f);
            Vector2 boxSize = new Vector2(col.bounds.size.x * 0.9f, 0.5f);
            Gizmos.DrawWireCube(topCenter, boxSize);
        }
    }
}