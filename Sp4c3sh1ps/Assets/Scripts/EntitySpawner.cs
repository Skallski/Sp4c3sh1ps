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

    private static GameObject Spawn(GameObject prefab, Vector2 position, Quaternion rotation) => Instantiate(prefab, position, rotation);

    public GameObject SpawnEnemy(Vector2 position, Quaternion rotation) => Spawn(enemySpaceshipPrefab, position, rotation);

}