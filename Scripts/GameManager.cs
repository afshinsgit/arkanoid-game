using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int ballLives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject gamePausePanel;
    public int numberOfBricksInTheScene;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    public GameObject levelNext2;

    void Start ()
    {
        livesText.text = "Lives: " + ballLives;
        scoreText.text = "Score: " + score;
        numberOfBricksInTheScene = GameObject.FindGameObjectsWithTag("Brick").Length;
	}
	
	void Update ()
    {
        PauseGame();
	}

    public void UpdateLives(int changeLives)
    {
        ballLives += changeLives;
        // Game ends if no lives left
        if (ballLives <= 0)
        {
            ballLives = 0;
            GameOver();
        }

        livesText.text = "Lives: " + ballLives;
    }

    public void UpdateScore(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricksInTheScene--;
        if (numberOfBricksInTheScene <= 0)
        {
            if(currentLevelIndex >= levels.Length - 1)
            {
                GameOver();
            }
            else
            { 
                gameOver = true;
                levelNext2.SetActive(true);
                Invoke("LoadLevel", 2f);
            }
        }
    }
    
    void LoadLevel()
    {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.up * 1.2f, Quaternion.identity);
        numberOfBricksInTheScene = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        levelNext2.SetActive(false);
    }

    // On Game Over activate the game-over panel, destroy the ball and update player's score
    void GameOver()
    {
        gameOver = true;
        Destroy(GameObject.FindWithTag("Ball"));
        gameOverPanel.SetActive(true);
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "New High Score: " + score;
        }
        else
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void RestartGameLevel1()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }

    public void RestartGameLevel2()
    {
        SceneManager.LoadScene("Level2");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameOver = false;
            Time.timeScale = 0;
            gamePausePanel.SetActive(true);
        }
    }

    public void UnpauseGame()
    {
        gameOver = false;
        Time.timeScale = 1;
        gamePausePanel.SetActive(false);
    }
}
