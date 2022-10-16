using System.Collections;
using System.Collections.Generic;
using SkalluUtils.PropertyAttributes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public sealed class PowerUp : Collectable
{
    private GameObject _powerUpObject;
    private SpriteRenderer _sr;
    private Light2D _light2D;

    #region INSPECTOR FIELDS
    [SerializeField, ReadOnlyInspector] private float _disappearTimer, _respawnTimer;
    [SerializeField] private List<PowerUpData> _availablePowerUps = new List<PowerUpData>();
    [SerializeField, ReadOnlyInspector] private PowerUpData _currentPowerUp, _lastPowerUp;
    #endregion

    #region DISAPPEAR AND RESPAWN RELATED FIELDS
    private readonly WaitForSeconds _disappearDelay = new WaitForSeconds(2f);
    private const float RESPAWN_TIME = 20;
    private const float DISAPPEAR_TIME = 5;
    
    private Coroutine _disappearCoroutine;
    private Coroutine _respawnCoroutine;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        
        _powerUpObject = transform.GetChild(0).gameObject;
        _sr = _powerUpObject.GetComponent<SpriteRenderer>();
        _light2D = _powerUpObject.GetComponent<Light2D>();
    }

    protected override void Start()
    {
        base.Start();
        Show();
    }

    protected override void GetCollected()
    {
        if (!_powerUpObject.activeSelf) return;

        _currentPowerUp.ApplyPowerUp();
        _lastPowerUp = _currentPowerUp;
        _currentPowerUp = null;
        
        base.GetCollected();

        StopCoroutine(_disappearCoroutine);
        Respawn();
    }

    #region DISAPPEAR, RESPAWN, SHOW
    private void Show()
    {
        _powerUpObject.SetActive(true);
        
        // set power up (same power up cannot be set twice in a row)
        while (true)
        {
            var powerUp = _availablePowerUps[Random.Range(0, _availablePowerUps.Count)];
            if (powerUp == _lastPowerUp) continue;
            
            _currentPowerUp = powerUp;
            break;
        }

        // set color
        _sr.color = _currentPowerUp.PowerUpColor;
        _light2D.color = _currentPowerUp.PowerUpColor;
        
        // start disappearing
        if (_disappearCoroutine != null)
            StopCoroutine(_disappearCoroutine);
        
        _disappearCoroutine = StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return _disappearDelay;
        
        _disappearTimer = DISAPPEAR_TIME;
        var color = _sr.color;

        while (_disappearTimer > 0)
        {
            _disappearTimer -= Time.deltaTime;
            
            // decrease sprite and light alpha to imitate disappearing
            color = new Color(color.r, color.g, color.b, color.a -= Time.deltaTime * 0.2f);
            _sr.color = color;
            _light2D.color = color;

            yield return null;
        }

        _disappearTimer = 0;
        Respawn();
    }
    
    private void Respawn()
    {
        if (_respawnCoroutine != null)
            StopCoroutine(_respawnCoroutine);

        _respawnCoroutine = StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        _respawnTimer = RESPAWN_TIME;
        _powerUpObject.SetActive(false);

        while (_respawnTimer > 0)
        {
            _respawnTimer -= Time.deltaTime;
            yield return null;
        }
        
        _respawnTimer = 0;
        Show();
    }
    #endregion
    
}