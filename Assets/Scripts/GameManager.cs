using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private int score;
    private Home[] homes;
    private int lives;

    private Dogger dogger;

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
    
    private void NewRound()
    {
        Respawn();
    }

    private void Respawn()
    {
        dogger.Respawn();
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
    public void HomeOccupied()
    {
        dogger.gameObject.SetActive(false);

        if (Cleared())
        {
            Invoke(nameof(NewLevel), 1f);

        } else
        {
            Invoke(nameof(NewRound), 1f);
        }
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

}