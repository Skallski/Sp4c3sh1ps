using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpaceship : Spaceship
{
    public static readonly List<EnemySpaceship> Enemies = new List<EnemySpaceship>();

    protected override void Awake()
    {
        Enemies.Add(this);
        
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

    protected override void Die()
    {
        base.Die();
        
        Enemies.Remove(this);
        Destroy(gameObject);
    }

    private void OnPlayerDied(object sender, EventArgs e) => Destroy(gameObject);
    
}