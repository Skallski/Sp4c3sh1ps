using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpData", menuName = "Power Up/Force Shield")]
public sealed class ForceShieldPowerUpData : TimeBasedPowerUpData
{
    private PlayerSpaceship _playerSpaceship;
    
    public override void ApplyPowerUp()
    {
        _playerSpaceship = PlayerSpaceship.Instance;

        var powerUpManager = PowerUpManager.Instance;
        powerUpManager.StartPowerUp(StartTimeBasedPowerUp(Enable, Disable));
    }

    private void Enable()
    {
        _playerSpaceship.isInvincible = true;
        _playerSpaceship.SetShieldAnimation();
    }

    private void Disable()
    {
        _playerSpaceship.isInvincible = false;
        _playerSpaceship.SetIdleAnimation();
    }
    
}