using UnityEngine;

public sealed class PlayerSpaceship : Spaceship
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
        var collidedObject = col.gameObject;
        
        if (collidedObject.CompareTag("Collectable"))
        {
            
        }
        else if (collidedObject.CompareTag("Spaceship"))
        {
            // TODO: game over
            
            Debug.Log("game over");
        }
    }

}