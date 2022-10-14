using UnityEngine;

public abstract class PowerUpData : ScriptableObject
{ 
    [SerializeField] private float _duration;
    public float Duration => _duration;

    [SerializeField] private Color _powerUpColor;
    public Color PowerUpColor => _powerUpColor;

    public abstract void ApplyPowerUp();
}