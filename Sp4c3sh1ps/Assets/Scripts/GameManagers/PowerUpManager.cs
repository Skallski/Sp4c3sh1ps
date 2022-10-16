using System;
using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }
    
    private Coroutine _activeCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    private void OnEnable() => PlayerSpaceship.Died += OnPlayerDied;

    private void OnDisable() => PlayerSpaceship.Died -= OnPlayerDied;
    
    private void OnPlayerDied(object sender, EventArgs e)
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);
    }

    public void StartPowerUp(IEnumerator coroutine)
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        _activeCoroutine = StartCoroutine(coroutine);
    }

}