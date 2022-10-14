using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpData", menuName = "Power Up/Freeze Enemies")]
public class FreezeEnemiesPowerUpData : PowerUpData
{
    private readonly WaitForSeconds _freezeTime = new WaitForSeconds(5);
    
    public override void ApplyPowerUp()
    {
        // makes enemies immovable for 5 seconds
        var size = EnemySpaceship.Enemies.Count;
        if (size == 0) return;
        
        for (var i = 0; i < size; i++)
        {
            var enemy = EnemySpaceship.Enemies[i];
            enemy.StartCoroutine(FreezeEnemy(enemy));
        }
    }

    /// <summary>
    /// Makes enemy immovable for some time
    /// </summary>
    /// <param name="enemySpaceship"> enemy spaceship to freeze </param>
    /// <returns></returns>
    private IEnumerator FreezeEnemy(EnemySpaceship enemySpaceship)
    {
        enemySpaceship.canMove = false;
        yield return _freezeTime;
        enemySpaceship.canMove = true;
    }

} 