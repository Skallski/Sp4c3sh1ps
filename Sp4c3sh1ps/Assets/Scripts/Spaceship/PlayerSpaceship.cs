using System;
using UnityEngine;

public sealed class PlayerSpaceship : Spaceship
{
    [SerializeField] private float speedIncreaseValue = 0.025f;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        Collectable.Collected += OnCollected;
    }

    private void OnDisable()
    {
        Collectable.Collected -= OnCollected;
    }

    private void Start()
    {

    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var collidedObject = col.gameObject;
        
        if (collidedObject.CompareTag("Collectable"))
        {
            collidedObject.GetComponent<Collectable>().GetCollected();
        }
        else if (collidedObject.CompareTag("Spaceship"))
        {
            // TODO: game over
            
            Debug.Log("game over");
        }
    }
    
    private void OnCollected(object sender, EventArgs e)
    {
        movementSpeed += speedIncreaseValue;
        rotationSpeed += speedIncreaseValue;
    }

}