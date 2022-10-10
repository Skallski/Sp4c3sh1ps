using System;
using UnityEngine;

public sealed class PlayerSpaceship : Spaceship
{
    [SerializeField] private float _speedIncreasePerPoint = 0.025f;

    public static event EventHandler Died;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        Point.Collected += OnCollectedPoint;
    }

    private void OnDisable()
    {
        Point.Collected -= OnCollectedPoint;
    }

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
        if (col.CompareTag("EnemySpaceship"))
            Die();
    }

    private void OnCollectedPoint(object sender, EventArgs e)
    {
        _movementSpeed += _speedIncreasePerPoint;
        _rotationSpeed += _speedIncreasePerPoint;
    }

    protected override void Die()
    {
        base.Die();
        
        Died?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

}