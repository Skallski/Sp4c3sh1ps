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
        if (col.CompareTag("EnemySpaceship") || col.CompareTag("Player"))
            Die();
    }

    private void OnDestroy() => Enemies.Remove(this);

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    private void OnPlayerDied(object sender, EventArgs e) => Destroy(gameObject);

}