using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner Self { get; private set; }

    private ScreenBounds _screenBounds;

    #region INSPECTOR FIELDS
    [SerializeField] private GameObject _playerSpaceshipPrefab;
    [SerializeField] private GameObject _enemySpaceshipPrefab;
    
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private GameObject _powerUpPrefab;
    #endregion

    private void Awake()
    {
        if (Self != null && Self != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Self = this;
            
            _screenBounds = ScreenBounds.Instance;
        }
    }

    private void OnEnable() => MenuSplashScreen.Instance.GameStarted += OnGameStarted;

    private void OnDisable() => MenuSplashScreen.Instance.GameStarted -= OnGameStarted;

    private void OnGameStarted(object sender, EventArgs e)
    {
        var spawnRotation = Quaternion.identity;
        SpawnObject(_playerSpaceshipPrefab, Vector2.zero, spawnRotation); // spawn player
        SpawnObject(_pointPrefab, _screenBounds.GetRandomScreenPosition(), spawnRotation); // spawn point
        SpawnObject(_powerUpPrefab, _screenBounds.GetRandomScreenPosition(), spawnRotation); // spawn power up
        
        SpawnEnemy();
    }

    private static void SpawnObject(GameObject prefab, Vector2 position, Quaternion rotation) =>
        Instantiate(prefab, position, rotation);
    
    public void SpawnEnemy() =>
        SpawnObject(_enemySpaceshipPrefab, _screenBounds.GetRandomScreenPosition(),
            Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
}