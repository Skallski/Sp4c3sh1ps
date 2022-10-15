using UnityEngine;

public abstract class PowerUpData : ScriptableObject
{
    [SerializeField] private Color _powerUpColor;
    public Color PowerUpColor => _powerUpColor;

    public abstract void ApplyPowerUp();
    
}