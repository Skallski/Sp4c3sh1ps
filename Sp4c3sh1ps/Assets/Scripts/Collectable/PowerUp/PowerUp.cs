using System.Collections;
using System.Collections.Generic;
using SkalluUtils.PropertyAttributes;
using UnityEngine;

public class PowerUp : Collectable
{
    private GameObject _powerUpObject;
    private SpriteRenderer _sr;

    #region INSPECTOR FIELDS
    [SerializeField, ReadOnlyInspector] private float _disappearTimer, _respawnTimer;
    [SerializeField] private List<PowerUpData> _availablePowerUps = new List<PowerUpData>();
    [SerializeField, ReadOnlyInspector] private PowerUpData _currentPowerUp;
    #endregion
   
    #region DISAPPEAR AND RESPAWN RELATED FIELDS
    private readonly WaitForSeconds _disappearDelay = new WaitForSeconds(2f);
    private const float RESPAWN_TIME = 1; // 20
    private const float DISAPPEAR_TIME = 20; // 5
    
    private Coroutine _disappearCoroutine;
    private Coroutine _respawnCoroutine;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        
        _powerUpObject = transform.GetChild(0).gameObject;
        _sr = _powerUpObject.GetComponent<SpriteRenderer>();
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
        _currentPowerUp = null;
        base.GetCollected();

        StopCoroutine(_disappearCoroutine);
        Respawn();
    }

    #region DISAPPEAR, RESPAWN, SHOW
    private void Show()
    {
        _powerUpObject.SetActive(true);
        _currentPowerUp = _availablePowerUps[Random.Range(0, _availablePowerUps.Count)];
        _sr.color = _currentPowerUp.PowerUpColor;
        
        if (_disappearCoroutine != null)
            StopCoroutine(_disappearCoroutine);
        
        _disappearCoroutine = StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return _disappearDelay;
        
        _disappearTimer = DISAPPEAR_TIME;

        while (_disappearTimer > 0)
        {
            _disappearTimer -= Time.deltaTime;
            
            var color = _sr.color;
            _sr.color = new Color(color.r, color.g, color.b, color.a -= Time.deltaTime * 0.2f);
            
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