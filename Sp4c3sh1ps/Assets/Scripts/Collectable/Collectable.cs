using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
    private ScreenBounds screenBounds;

    public static event EventHandler Collected;
    
    private void Awake() => screenBounds = ScreenBounds.Self;

    private void Start() => transform.position = screenBounds.GetRandomScreenPosition();

    private void Update()
    {
        
    }

    public virtual void GetCollected()
    {
        transform.position = screenBounds.GetRandomScreenPosition();
        Collected?.Invoke(this, EventArgs.Empty);
    }
    
} 