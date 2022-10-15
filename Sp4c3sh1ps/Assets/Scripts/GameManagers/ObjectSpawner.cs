using System;
using Unity.Mathematics;
using UnityEngine;

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
        SpawnPlayer();
        
        Spawn(_pointPrefab, _screenBounds.GetRandomScreenPosition(), quaternion.identity);
        Spawn(_powerUpPrefab, _screenBounds.GetRandomScreenPosition(), quaternion.identity);
    }

    private static void Spawn(GameObject prefab, Vector2 position, Quaternion rotation) => Instantiate(prefab, position, rotation);
    
    private void SpawnPlayer() => Spawn(_playerSpaceshipPrefab, Vector2.zero, Quaternion.identity);
    public void SpawnEnemy(Vector2 position, Quaternion rotation) => Spawn(_enemySpaceshipPrefab, position, rotation);

}