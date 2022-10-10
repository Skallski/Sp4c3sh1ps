using UnityEngine;

public abstract class TimeBasedPowerUpData : PowerUpData
{
    [SerializeField] private float _duration;
    public float Duration => _duration;
}