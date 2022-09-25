using UnityEngine;

public sealed class EntitySpawner : MonoBehaviour
{
    public static EntitySpawner Self { get; private set; }

    [SerializeField] private GameObject enemySpaceshipPrefab;

    private void Awake()
    {
        if (Self != null && Self != this)
            Destroy(gameObject);
        else
            Self = this;
    }

    public GameObject Spawn(GameObject prefab, Vector2 position, Quaternion rotation) => Instantiate(prefab, position, rotation);
    
}