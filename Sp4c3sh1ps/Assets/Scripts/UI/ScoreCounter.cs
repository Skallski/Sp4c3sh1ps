using System;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI scoreTmp;
    public static int CurrentScore { get; private set; } = 0;
    public static int HighScore { get; private set; } = 0;

    public static event EventHandler ScoreSubmited;
    
    private void Awake() => scoreTmp = GetComponent<TextMeshProUGUI>();

    private void OnEnable()
    {
        MenuSplashScreen.Self.GameStarted += OnGameStarted;
        Collectable.Collected += OnPlayerCollectedPoint;
        PlayerSpaceship.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        MenuSplashScreen.Self.GameStarted -= OnGameStarted;
        Collectable.Collected -= OnPlayerCollectedPoint;
        PlayerSpaceship.Died -= OnPlayerDied;
    }

    private void OnGameStarted(object sender, EventArgs e)
    {
        CurrentScore = 0;
        scoreTmp.SetText(CurrentScore.ToString());
    }
    
    private void OnPlayerCollectedPoint(object sender, EventArgs e)
    {
        CurrentScore += 1;
        scoreTmp.SetText(CurrentScore.ToString());

        if (CurrentScore % 5 == 0)
            EntitySpawner.Self.SpawnEnemy(ScreenBounds.Self.GetRandomScreenPosition(), Quaternion.identity);
    }
    
    private void OnPlayerDied(object sender, EventArgs e)
    {
        PlayerPrefs.SetInt("lastScore", CurrentScore);
        
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("highscore", HighScore);
        }
        
        ScoreSubmited?.Invoke(this, EventArgs.Empty);
        scoreTmp.SetText(string.Empty);
    }
    
}