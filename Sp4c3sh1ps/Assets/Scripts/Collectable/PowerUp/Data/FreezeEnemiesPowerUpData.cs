using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpData", menuName = "Power Up/Freeze Enemies")]
public class FreezeEnemiesPowerUpData : TimeBasedPowerUpData
{
    private int _size;
    
    public override void ApplyPowerUp()
    {
        _size = EnemySpaceship.Enemies.Count;
        if (_size == 0) return;

        var powerUpManager = PowerUpManager.Instance;
        powerUpManager.StartPowerUp(StartTimeBasedPowerUp(Freeze, Unfreeze));
    }

    private void Freeze()
    {
        for (int i = 0; i < _size; i++)
        {
            EnemySpaceship.Enemies[i].canMove = false;
        }
    }
    
    private void Unfreeze()
    {
        for (int i = 0; i < _size; i++)
        {
            EnemySpaceship.Enemies[i].canMove = true;
        }
    }

} 