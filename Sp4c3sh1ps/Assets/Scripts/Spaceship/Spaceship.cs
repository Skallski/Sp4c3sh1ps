using SkalluUtils.PropertyAttributes;
using UnityEngine;

public abstract class Spaceship : MonoBehaviour
{
    private GameControlsManager _gameControlsManager;
    private ScreenBounds _screenBounds;

    [SerializeField] protected SpaceshipData _spaceshipData;
    [SerializeField, ReadOnlyInspector] protected float _movementSpeed, _rotationSpeed;

    protected bool canMove = true;

    protected virtual void Awake()
    {
        _gameControlsManager = GameControlsManager.Self;
        _screenBounds = ScreenBounds.Self;
    }

    protected virtual void Start()
    {
        _movementSpeed = _spaceshipData.StartMovementSpeed;
        _rotationSpeed = _spaceshipData.StartRotationSpeed;
        
        canMove = true;
    }

    protected virtual void Update() => Move();

    private void Move()
    {
        if (!canMove) return;

        var tr = transform;
        var movementPos = tr.position + tr.up * (_movementSpeed * Time.deltaTime);
        
        if (_screenBounds.IsOutOfBounds(tr.position))
            movementPos = _screenBounds.CalculateWrappedPosition(tr);
        transform.position = movementPos;

        if (_gameControlsManager.Left) transform.eulerAngles += new Vector3(0, 0, 45 * (_rotationSpeed * Time.deltaTime));
        if (_gameControlsManager.Right) transform.eulerAngles -= new Vector3(0, 0, 45 * (_rotationSpeed * Time.deltaTime));
    }

    protected virtual void Die() => Instantiate(_spaceshipData.DestroyParticles, transform.position, Quaternion.identity);
    
}