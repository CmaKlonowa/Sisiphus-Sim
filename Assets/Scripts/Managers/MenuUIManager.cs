using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;

    // Load and display score using GameManager
    private void Start() {
        float score = new GameManager.SaveData().Load();
        HighScoreText.text = "High Score: " + score;
    }

    // Start game button functionality
    public void StartGame()
    {
        // Load Main
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
