using System;
using UnityEngine;

public sealed class EntitySpawner : MonoBehaviour
{
    public static EntitySpawner Self { get; private set; }

    [SerializeField] private GameObject playerSpaceshipPrefab;
    [SerializeField] private GameObject enemySpaceshipPrefab;

    private void Awake()
    {
        if (Self != null && Self != this)
            Destroy(gameObject);
        else
            Self = this;
    }

    private void OnEnable()
    {
        MenuSplashScreen.Self.GameStarted += OnGameStarted;
    }
    
    private void OnDisable()
    {
        MenuSplashScreen.Self.GameStarted -= OnGameStarted;
    }
    
    private void OnGameStarted(object sender, EventArgs e) => SpawnPlayer();

    
    private static GameObject Spawn(GameObject prefab, Vector2 position, Quaternion rotation) => Instantiate(prefab, position, rotation);
    
    private void SpawnPlayer() => Spawn(playerSpaceshipPrefab, Vector2.zero, Quaternion.identity);
    
    public GameObject SpawnEnemy(Vector2 position, Quaternion rotation) => Spawn(enemySpaceshipPrefab, position, rotation);

}