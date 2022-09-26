using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpaceship : Spaceship
{
    protected override void Awake()
    {
        base.Awake();
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
        if (col.gameObject.CompareTag("Spaceship"))
        {
            Destroy(gameObject);
        }
    }
    
} 
