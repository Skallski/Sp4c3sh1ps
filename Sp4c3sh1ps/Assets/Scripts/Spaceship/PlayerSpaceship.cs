using System;
using UnityEngine;

public sealed class PlayerSpaceship : Spaceship
{
    private const float SPEED_INCREASE_PER_POINT = 0.025f;
    public const int MAX_LIVES = 3;
    
    [SerializeField] private int _currentLives = 1;

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
            TakeDamage();
    }

    private void OnCollectedPoint(object sender, EventArgs e)
    {
        _movementSpeed += SPEED_INCREASE_PER_POINT;
        _rotationSpeed += SPEED_INCREASE_PER_POINT;
    }

    private void TakeDamage()
    {
        _currentLives -= 1;

        if (_currentLives <= 0)
            Die();
    }

    public override void Die()
    {
        base.Die();
        
        Died?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

}