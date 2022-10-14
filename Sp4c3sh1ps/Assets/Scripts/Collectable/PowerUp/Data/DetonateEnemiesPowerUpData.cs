using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpData", menuName = "Power Up/Detonate Enemies")]
public class DetonateEnemiesPowerUpData : PowerUpData
{
    public override void ApplyPowerUp()
    {
        // kills every alive enemy 
        var size = EnemySpaceship.Enemies.Count;
        if (size == 0) return;

        for (var i = 0; i < size; i++)
        {
            var aliveEnemy = EnemySpaceship.Enemies[i];
            aliveEnemy.Die();
        }
    }
    
}