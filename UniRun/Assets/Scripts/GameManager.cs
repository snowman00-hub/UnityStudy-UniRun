using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework;

public class GameManager : MonoBehaviour
{
    public bool IsGameOver { get;private set;}

    private int score;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverUi;

    private void Awake()
    {
        gameOverUi.SetActive(false);
    }

    private void Update()
    {
        if(IsGameOver && Input.GetMouseButtonDown(0))
        {
            score = 0;
            IsGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);            
        }
    }

    public void AddScore(int add)
    {
        if (!IsGameOver)
        {
            score += add;
            scoreText.text = $"Score: {score}";
        }
    }

    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOverUi.SetActive(true);
    }
}