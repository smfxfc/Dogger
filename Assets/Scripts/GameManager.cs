using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

// TODO: track high score
// TODO: add 5 levels
// TODO: add bonus items
public class GameManager : MonoBehaviour
{
    private Home[] homes;

    private Dogger dogger;
    public GameObject gameOverMenu; 
    
    public Text scoreText;
    public Text livesText;
    public Text timeText;
    private int score;
    private int lives;
    private int time;

    private static int _nextLevelIndex = 1;
    
    

    private void Awake()
    {
        homes = FindObjectsOfType<Home>();
        dogger = FindObjectOfType<Dogger>();
    }

    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        gameOverMenu.SetActive(false);
        SetScore(0);
        SetLives(5);
        NewLevel();
    }

    private void NewLevel()
    {
        for (int i = 0; i<homes.Length; i++) {
            homes[i].enabled = false;
        }
        Respawn();
    }
    

    private void Respawn()
    {
        dogger.Respawn();
        StopAllCoroutines();
        StartCoroutine(Timer(30));
    }
    private IEnumerator Timer(int duration)
    {
        time = duration;
        timeText.text = time.ToString();
        
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            timeText.text = time.ToString();
        }

        dogger.Death();
    }

    private bool Cleared()
    {
        for (int i = 0; i<homes.Length; i++)
        {
            if (!homes[i].enabled)
            {
                return false;
            }
        }
        return true;
    }

    public void AdvancedRow()
    {
        SetScore(score + 20);
    }
    public void Died()
    {
        SetLives(lives - 1);
        if (lives > 0) {
            Invoke(nameof(Respawn), 1f);
        } else
        {
            Invoke(nameof(GameOver), 1f);
        }
    }

    public void HomeOccupied()
    {
        dogger.gameObject.SetActive(false);
        int timePoints = time * 3;
        SetScore(score + timePoints + 100);

        if (Cleared())
        {
            SetScore(score + 1000);
            _nextLevelIndex++;

            if (_nextLevelIndex > 5)
            {
                GameOver();
            }

            string nextLevelName = "Level" + _nextLevelIndex;
            SceneManager.LoadScene(nextLevelName);
            Invoke(nameof(NewLevel), 1f);

        } else
        {
            Invoke(nameof(Respawn), 1f);
        }
    }

    // hide game and prompt user to play again or quit
    private void GameOver()
    {
        dogger.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PlayAgain());
    }

    private IEnumerator PlayAgain()
    {
        bool playAgain = false;
        while (!playAgain)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                playAgain = true;
            }
            yield return null;
        }
        NewGame();
    }

    private IEnumerator CheckForPlayAgain()
    {
        bool playAgain = false;

        while (!playAgain) {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                playAgain = true;
            }
            yield return null;
        }
    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
}