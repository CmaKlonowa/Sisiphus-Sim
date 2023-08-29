using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public RectTransform PauseButtonRect;
    public Vector3 defaultPauseButtonPos;
    public TextMeshProUGUI pauseButtonText;
    public GameObject orPressPText;
    public GameObject gameOverUI;
    public GameObject pauseUI;

    public float timer;
    void Start()
    {
        Time.timeScale = 1F;
        timer = 0F;
        paused = false;
    }
    void Update()
    {
        if (!MainManager.IsGameOver)
        {
            // score funtionality
            timer += Time.deltaTime;
            UpdateText();

            // pause if p
            if (Input.GetKeyDown("p") && !paused)
            {
                PauseButton();
                // Set position to mouse ((relative to upper right corner))
                Vector3[] temp = new Vector3[4];
                PauseButtonRect.GetWorldCorners(temp);
                PauseButtonRect.anchoredPosition = Input.mousePosition - temp[3];
            }
        } 
    }

    void UpdateText()
    {
        scoreText.text = "Height: " + Mathf.FloorToInt(timer) + "m. above msl";
        highScoreText.text = "High score: " + Mathf.FloorToInt(GameManager.instance.Score);
    }

    public void EndGame(bool gameOver = false)
    {
        if (timer > GameManager.instance.Score)
        {
            // New High Score!
            GameManager.instance.Score = timer;
            new GameManager.SaveData().Save();
        }

        MainManager.IsGameOver = gameOver;
        gameOverUI.SetActive(gameOver);
    }

    // Pause button functionality
    private bool paused = false;
    public void PauseButton()
    {
        if (!MainManager.IsGameOver) {
            // set position to default
            PauseButtonRect.anchoredPosition = defaultPauseButtonPos;

            if (paused) {
                pauseButtonText.text = "Pause";
                // resume
                Time.timeScale = 1F;
            }
            else {
                pauseButtonText.text = "Resume";
                // pause
                Time.timeScale = 0F;
            }

            orPressPText.SetActive(paused);

            paused = !paused;
            pauseUI.SetActive(paused);
        }
    }

    // Back to menu button
    public void BackToMenu()
    {
        EndGame(false);
        SceneManager.LoadScene("Menu");
    }
}