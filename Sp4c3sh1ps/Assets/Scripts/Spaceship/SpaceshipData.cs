using UnityEngine;

[CreateAssetMenu(fileName = "NewSpaceshipData", menuName = "Spaceship Data")]
public class SpaceshipData : ScriptableObject
{
    [SerializeField] private float _startMovementSpeed = 2;
    public float StartMovementSpeed => _startMovementSpeed;
    
    [SerializeField] private float _startRotationSpeed = 2;
    public float StartRotationSpeed => _startRotationSpeed;

    [SerializeField] private ParticleSystem _destroyParticles;
    public ParticleSystem DestroyParticles => _destroyParticles;
}