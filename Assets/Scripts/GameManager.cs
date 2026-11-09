using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float startSpeed = 7f;
    public float maxSpeed = 13f;
    public float acceleration = 0.15f;

    public int pointsPerFruit = 10;
    public float pointsPerMeter = 1f;
    public float targetScore = 1000f;

    public Text scoreText;
    public Text fruitsText;
    public GameObject gameOverPanel;
    public Text gameOverText;

    public float CurrentSpeed { get; private set; }
    public bool IsPlaying { get; private set; }
    public int Fruits { get; private set; }
    public int Score => Mathf.FloorToInt(distance * pointsPerMeter) + Fruits * pointsPerFruit;

    const string BestKey = "FelpudoRace_Best";
    float distance;
    float gameOverTime;
    bool isWon;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        CurrentSpeed = startSpeed;
        IsPlaying = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (GameInput.RestartPressed())
        {
            Restart();
            return;
        }

        if (IsPlaying)
        {
            CurrentSpeed = Mathf.Min(maxSpeed, CurrentSpeed + acceleration * Time.deltaTime);
            distance += CurrentSpeed * Time.deltaTime;

            if (Score >= targetScore && !isWon)
            {
                WinGame();
            }

            UpdateUI();
        }
        else if (Time.time - gameOverTime > 0.5f && GameInput.JumpPressed())
        {
            Restart();
        }
    }

    public void AddFruit()
    {
        if (!IsPlaying) return;
        Fruits++;
        UpdateUI();
    }

    public void WinGame()
    {
        if (!IsPlaying) return;

        IsPlaying = false;
        isWon = true;
        CurrentSpeed = 0f;
        gameOverTime = Time.time;

        int score = Score;
        int best = PlayerPrefs.GetInt(BestKey, 0);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt(BestKey, best);
            PlayerPrefs.Save();
        }

        UpdateUI();

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null)
        {
            gameOverText.text = "VOCÊ RESGATOU A FOFURA!\n\nFelpudo chegou à montanha e derrotou os lesmos de Uruca!\nFrutas: " + Fruits + "   Pontos: " + score + "\n\nAperte R para jogar de novo";
        }
    }

    public void GameOver()
    {
        if (!IsPlaying || isWon) return;

        IsPlaying = false;
        CurrentSpeed = 0f;
        gameOverTime = Time.time;

        int score = Score;
        int best = PlayerPrefs.GetInt(BestKey, 0);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt(BestKey, best);
            PlayerPrefs.Save();
        }

        UpdateUI();

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null)
        {
            gameOverText.text = "FIM DE JOGO\n\nPontos: " + score + "\nFrutas: " + Fruits
                              + "\nMelhor: " + best + "\n\nAperte R ou clique para jogar de novo";
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Pontos: " + Score;
        if (fruitsText != null) fruitsText.text = "Frutas: " + Fruits;
    }
}