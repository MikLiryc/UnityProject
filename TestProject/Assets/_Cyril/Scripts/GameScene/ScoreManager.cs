using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private void Awake() => Instance = this;

    public Text scoreText;
    public Text highScoreText;
    public TextMeshProUGUI textTxt;

    private int score = 0;
    private int highScore = 0;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        if (highScore == 0)
        {
            highScoreText.text = "HighScore : 0";
        }
        else
        {
            highScoreText.text = "HighScore : " + highScore;
        }
    }
    // Update is called once per frame
    void Update()
    {
        saveHighScore();
    }

    private void saveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "HighScore : " + highScore;
        }
    }

    public void addScore()
    {
        score++;
        scoreText.text = "Score : " + score;
    }
}
