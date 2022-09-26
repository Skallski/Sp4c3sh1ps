using System;
using UnityEngine;

public sealed class EnemySpaceship : Spaceship
{
    [Space, SerializeField] private ParticleSystem enemyShipDestroyParticles;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable() => PlayerSpaceship.Died += OnPlayerDied;

    private void OnDisable() => PlayerSpaceship.Died -= OnPlayerDied;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemySpaceship"))
            Die();
    }

    private void Die()
    {
        Instantiate(enemyShipDestroyParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnPlayerDied(object sender, EventArgs e) => Destroy(gameObject);
    
} 
