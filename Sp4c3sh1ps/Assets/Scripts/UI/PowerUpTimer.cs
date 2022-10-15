using System;
using TMPro;
using UnityEngine;

public class PowerUpTimer : MonoBehaviour
{
    public static PowerUpTimer Instance { get; private set; }

    private TextMeshProUGUI _tmp;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            
            _tmp = GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable() => PlayerSpaceship.Died += OnPlayerDied;

    private void OnDisable() => PlayerSpaceship.Died -= OnPlayerDied;

    private void Start() => Hide();
    
    private void OnPlayerDied(object sender, EventArgs e) => Hide();

    public void Hide() => _tmp.SetText(string.Empty);

    public void SetTimer(int time) => _tmp.SetText(time.ToString());
}