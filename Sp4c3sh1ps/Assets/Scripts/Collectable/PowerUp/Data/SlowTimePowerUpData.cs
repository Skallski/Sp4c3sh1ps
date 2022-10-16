using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpData", menuName = "Power Up/Slow Time")]
public sealed class SlowTimePowerUpData : TimeBasedPowerUpData
{
    private PlayerSpaceship _playerSpaceship;
    
    public override void ApplyPowerUp()
    {
        _playerSpaceship = PlayerSpaceship.Instance;
        
        var powerUpManager = PowerUpManager.Instance;
        powerUpManager.StartPowerUp(StartTimeBasedPowerUp(SlowDown, SpeedUp));
    }

    private void SlowDown()
    {
        if (!_playerSpaceship.isSlowedDown) // can slow down once at single time
        {
            _playerSpaceship.MovementSpeed *= 0.5f;
            _playerSpaceship.RotationSpeed *= 0.5f;
            _playerSpaceship.isSlowedDown = true;
        }

        foreach (var enemy in EnemySpaceship.Enemies)
        {
            if (!enemy.isSlowedDown) // can slow down once at single time
            {
                enemy.MovementSpeed *= 0.5f;
                enemy.RotationSpeed *= 0.5f;
                enemy.isSlowedDown = true;
            }
        }
    }

    private void SpeedUp()
    {
        _playerSpaceship.MovementSpeed *= 2;
        _playerSpaceship.RotationSpeed *= 2;
        _playerSpaceship.isSlowedDown = false;
        
        foreach (var enemy in EnemySpaceship.Enemies)
        {
            enemy.MovementSpeed *= 2;
            enemy.RotationSpeed *= 2;
            enemy.isSlowedDown = false;
        }
    }

}