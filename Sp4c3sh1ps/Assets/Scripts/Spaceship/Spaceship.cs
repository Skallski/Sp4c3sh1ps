using SkalluUtils.PropertyAttributes;
using UnityEngine;

public abstract class Spaceship : MonoBehaviour
{
    private GameControlsManager _gameControlsManager;
    private ScreenBounds _screenBounds;

    [SerializeField] protected SpaceshipData _spaceshipData;
    [field: SerializeField, ReadOnlyInspector] public float MovementSpeed { get; set; }
    [field: SerializeField, ReadOnlyInspector] public float RotationSpeed { get; set; }

    [Space]
    [ReadOnlyInspector] public bool canMove = true;
    [ReadOnlyInspector] public bool isSlowedDown = false;

    protected virtual void Awake()
    {
        _gameControlsManager = GameControlsManager.Self;
        _screenBounds = ScreenBounds.Instance;
    }

    protected virtual void Start()
    {
        MovementSpeed = _spaceshipData.StartMovementSpeed;
        RotationSpeed = _spaceshipData.StartRotationSpeed;
        
        canMove = true;
    }

    protected virtual void Update() => Move();

    private void Move()
    {
        if (!canMove) return;

        // move forward
        var tr = transform;
        var movementPos = tr.position + tr.up * (MovementSpeed * Time.deltaTime);
        
        if (_screenBounds.IsOutOfBounds(tr.position)) 
            movementPos = _screenBounds.CalculateWrappedPosition(tr);
        
        transform.position = movementPos;

        // turn left or right
        if (_gameControlsManager.Left) transform.eulerAngles += new Vector3(0, 0, 45 * (RotationSpeed * Time.deltaTime));
        if (_gameControlsManager.Right) transform.eulerAngles -= new Vector3(0, 0, 45 * (RotationSpeed * Time.deltaTime));
    }

    public virtual void Die() => Instantiate(_spaceshipData.DestroyParticles, transform.position, Quaternion.identity);
    
}