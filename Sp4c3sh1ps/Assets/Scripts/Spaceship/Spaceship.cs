using System;
using UnityEngine;

public abstract class Spaceship : MonoBehaviour
{
    private GameControlsManager gameControlsManager;
    private ScreenBounds screenBounds;
    
    [SerializeField] protected float movementSpeed = 2;
    [SerializeField] protected float rotationSpeed = 2;

    protected bool canMove = true;

    protected virtual void Awake()
    {
        gameControlsManager = GameControlsManager.Self;
        screenBounds = ScreenBounds.Self;
    }

    protected virtual void Start() => canMove = true;

    protected virtual void Update() => Move();

    private void Move()
    {
        if (!canMove) return;

        var tr = transform;
        var movementPos = tr.position + tr.up * (movementSpeed * Time.deltaTime);
        
        if (screenBounds.IsOutOfBounds(tr.position))
            movementPos = screenBounds.CalculateWrappedPosition(tr);
        transform.position = movementPos;

        if (gameControlsManager.Left) transform.eulerAngles += new Vector3(0, 0, 45 * (rotationSpeed * Time.deltaTime));
        if (gameControlsManager.Right) transform.eulerAngles -= new Vector3(0, 0, 45 * (rotationSpeed * Time.deltaTime));
    }

}