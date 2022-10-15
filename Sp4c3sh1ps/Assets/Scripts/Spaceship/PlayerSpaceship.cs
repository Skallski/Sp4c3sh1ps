using System;
using UnityEngine;

public sealed class PlayerSpaceship : Spaceship
{
    public static PlayerSpaceship Instance { get; private set; }

    private const float SPEED_INCREASE_PER_POINT = 0.025f;
    public const int MAX_LIVES = 3;
    
    [SerializeField] private int _currentLives = 1;

    public static event EventHandler Died;
    
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            
            base.Awake();
        }
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
        MovementSpeed += SPEED_INCREASE_PER_POINT;
        RotationSpeed += SPEED_INCREASE_PER_POINT;
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