using System;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private MenuSplashScreen _menuSplashScreen;
    
    private TextMeshProUGUI _scoreTmp;
    private Animator _animator;

    private int _currentScore = 0;
    private int _highScore = 0;

    public static event EventHandler ScoreSubmited;
    
    private void Awake()
    {
        _menuSplashScreen = MenuSplashScreen.Instance;
        
        _scoreTmp = GetComponent<TextMeshProUGUI>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _menuSplashScreen.GameStarted += OnGameStarted;
        Point.Collected += OnPlayerCollectedPoint;
        PlayerSpaceship.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _menuSplashScreen.GameStarted -= OnGameStarted;
        Point.Collected -= OnPlayerCollectedPoint;
        PlayerSpaceship.Died -= OnPlayerDied;
    }

    private void OnGameStarted(object sender, EventArgs e)
    {
        _currentScore = 0;
        _scoreTmp.SetText(_currentScore.ToString());
    }
    
    private void OnPlayerCollectedPoint(object sender, EventArgs e)
    {
        _currentScore += 1;
        _scoreTmp.SetText(_currentScore.ToString());
        _animator.Play("ScoreCounter_increment");

        if (_currentScore % 5 == 0)
            ObjectSpawner.Self.SpawnEnemy();
    }
    
    private void OnPlayerDied(object sender, EventArgs e)
    {
        PlayerPrefs.SetInt("lastScore", _currentScore);
        
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetInt("highscore", _highScore);
        }
        
        ScoreSubmited?.Invoke(this, EventArgs.Empty);
        _scoreTmp.SetText(string.Empty);
    }
    
}