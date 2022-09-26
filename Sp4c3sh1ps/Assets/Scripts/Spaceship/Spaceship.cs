using UnityEngine;

public abstract class Spaceship : MonoBehaviour
{
    private Controls controls;
    private ScreenBounds screenBounds;
    
    [SerializeField] protected float movementSpeed = 2;
    [SerializeField] protected float rotationSpeed = 2;

    protected virtual void Awake()
    {
        controls = Controls.Self;
        screenBounds = ScreenBounds.Self;
    }

    protected virtual void Update() => Move();

    private void Move()
    {
        var tr = transform;
        var movementPos = tr.position + tr.up * (movementSpeed * Time.deltaTime);
        
        if (screenBounds.IsOutOfBounds(tr.position))
            movementPos = screenBounds.CalculateWrappedPosition(tr);
        transform.position = movementPos;

        if (controls.Left) transform.eulerAngles += new Vector3(0, 0, 45 * (rotationSpeed * Time.deltaTime));
        if (controls.Right) transform.eulerAngles -= new Vector3(0, 0, 45 * (rotationSpeed * Time.deltaTime));
    }

}