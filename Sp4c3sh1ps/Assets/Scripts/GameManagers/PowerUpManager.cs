using System.Collections;
using SkalluUtils.PropertyAttributes;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    [ReadOnlyInspector] public PowerUpData _activePowerUp;

    public Coroutine _activeCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartPowerUp(IEnumerator coroutine, PowerUpData powerUpData)
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }

        _activeCoroutine = StartCoroutine(coroutine);
        _activePowerUp = powerUpData;
    }

}