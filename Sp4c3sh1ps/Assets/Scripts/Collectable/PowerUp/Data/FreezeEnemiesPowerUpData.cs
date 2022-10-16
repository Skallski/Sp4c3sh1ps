using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpData", menuName = "Power Up/Freeze Enemies")]
public sealed class FreezeEnemiesPowerUpData : TimeBasedPowerUpData
{
    public override void ApplyPowerUp()
    {
        if (EnemySpaceship.Enemies.Count == 0) return;

        var powerUpManager = PowerUpManager.Instance;
        powerUpManager.StartPowerUp(StartTimeBasedPowerUp(Freeze, Unfreeze));
    }

    private void Freeze()
    {
        foreach (var enemy in EnemySpaceship.Enemies)
        {
            enemy.canMove = false;
        }
    }
    
    private void Unfreeze()
    {
        foreach (var enemy in EnemySpaceship.Enemies)
        {
            enemy.canMove = true;
        }
    }

} 