using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Collectable : MonoBehaviour
{
    private ScreenBounds _screenBounds;

    protected virtual void Awake() => _screenBounds = ScreenBounds.Instance;

    private void OnEnable() => PlayerSpaceship.Died += OnPlayerDied;

    private void OnDisable() => PlayerSpaceship.Died -= OnPlayerDied;

    protected virtual void Start() => transform.position = _screenBounds.GetRandomScreenPosition();
    
    protected virtual void GetCollected() => transform.position = _screenBounds.GetRandomScreenPosition();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) GetCollected();
    }
    
    private void OnPlayerDied(object sender, EventArgs e) => Destroy(gameObject);
    
}