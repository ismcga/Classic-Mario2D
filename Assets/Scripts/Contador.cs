using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    // Ahora la instancia se llama 'Contador'
    public static Contador instance;

    [Header("Configuración UI")]
    public TextMeshProUGUI scoreText;

    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
    }

    public void GameOver()
    {
        Debug.Log("¡Juego Terminado! Puntuación final: " + score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}