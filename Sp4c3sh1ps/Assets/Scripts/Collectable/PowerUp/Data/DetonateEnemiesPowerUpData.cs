using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpData", menuName = "Power Up/Detonate Enemies")]
public sealed class DetonateEnemiesPowerUpData : PowerUpData
{
    public override void ApplyPowerUp()
    {
        // kills every alive enemy 
        if (EnemySpaceship.Enemies.Count == 0) return;

        foreach (var aliveEnemy in EnemySpaceship.Enemies)
        {
            aliveEnemy.Die();
        }
    }
    
}