using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUpData", menuName = "Power Up/Slow Time")]
public class SlowTimePowerUpData : PowerUpData
{
    private WaitForSeconds _slowTimeDuration;
    
    private void Awake() => _slowTimeDuration = new WaitForSeconds(Duration);

    public override void ApplyPowerUp()
    {
        // player moves slower
        var playerSpaceship = PlayerSpaceship.Instance;
        playerSpaceship.StartCoroutine(SlowSpaceshipDown(playerSpaceship));
        
        // makes alive enemies move slower
        var size = EnemySpaceship.Enemies.Count;
        if (size == 0) return;
        
        for (var i = 0; i < size; i++)
        {
            var enemy = EnemySpaceship.Enemies[i];
            enemy.StartCoroutine(SlowSpaceshipDown(enemy));
        }
    }
    
    private IEnumerator SlowSpaceshipDown(Spaceship spaceship)
    {
        spaceship.MovementSpeed *= 0.5f;
        spaceship.RotationSpeed *= 0.5f;
        yield return _slowTimeDuration;
        spaceship.MovementSpeed *= 0.5f;
        spaceship.RotationSpeed *= 0.5f;
    }
}