using System;
using UnityEngine;

public sealed class PlayerSpaceship : Spaceship
{
    [SerializeField] private float speedIncreaseValue = 0.025f;
    [Space, SerializeField] private ParticleSystem playerShipDestroyParticles;

    public static event EventHandler Died;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        Collectable.Collected += OnCollectedPoint;
    }

    private void OnDisable()
    {
        Collectable.Collected -= OnCollectedPoint;
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
        var collidedObject = col.gameObject;
        
        if (collidedObject.CompareTag("Collectible"))
        {
            collidedObject.GetComponent<Collectable>().GetCollected();
        }
        else if (collidedObject.CompareTag("EnemySpaceship"))
        {
            Die();
        }
    }

    private void OnCollectedPoint(object sender, EventArgs e)
    {
        movementSpeed += speedIncreaseValue;
        rotationSpeed += speedIncreaseValue;
    }

    private void Die()
    {
        Instantiate(playerShipDestroyParticles, transform.position, Quaternion.identity);
        
        Died?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

}