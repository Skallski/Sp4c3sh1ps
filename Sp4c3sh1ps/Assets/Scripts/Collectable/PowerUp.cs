using System.Collections;
using SkalluUtils.PropertyAttributes;
using UnityEngine;

public class PowerUp : Collectable
{
    private GameObject _powerUpObject;
    private SpriteRenderer _sr;
    
    [SerializeField, ReadOnlyInspector] private float _disappearTimer, _respawnTimer;
    
    private readonly WaitForSeconds _disappearDelay = new WaitForSeconds(2f);
    private Coroutine _disappearCoroutine;
    private Coroutine _respawnCoroutine;

    private Color _startColor;

    protected override void Awake()
    {
        base.Awake();
        
        _powerUpObject = transform.GetChild(0).gameObject;
        _sr = _powerUpObject.GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        
        _startColor = _sr.color;
        Disappear();
    }

    protected override void GetCollected()
    {
        if (!_powerUpObject.activeSelf) return;

        base.GetCollected();
        
        StopCoroutine(_disappearCoroutine);
        Respawn();
    }

    #region DISAPPEAR
    private void Disappear()
    {
        _sr.color = _startColor;
        
        if (_disappearCoroutine != null)
            StopCoroutine(_disappearCoroutine);
        
        _disappearCoroutine = StartCoroutine(DisappearRoutine());
    }

    private IEnumerator DisappearRoutine()
    {
        yield return _disappearDelay;
        
        _disappearTimer = 5;
        _startColor = _sr.color;

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
    #endregion

    #region RESPAWN
    private void Respawn()
    {
        if (_respawnCoroutine != null)
            StopCoroutine(_respawnCoroutine);

        _respawnCoroutine = StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        _respawnTimer = 10;
        _powerUpObject.SetActive(false);

        while (_respawnTimer > 0)
        {
            _respawnTimer -= Time.deltaTime;
            yield return null;
        }

        _respawnTimer = 0;
        
        _powerUpObject.SetActive(true);
        Disappear();
    }
    #endregion

}