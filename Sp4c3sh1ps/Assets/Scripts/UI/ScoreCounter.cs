using System;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI scoreTmp;
    private int counter = 0;

    private void Awake()
    {
        scoreTmp = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Collectable.Collected += OnCollected;
    }

    private void OnDisable()
    {
        Collectable.Collected -= OnCollected;
    }

    private void Start()
    {
        counter = 0;
        scoreTmp.SetText(counter.ToString());
    }

    private void Update()
    {

    }
    
    private void OnCollected(object sender, EventArgs e)
    {
        counter += 1;
        scoreTmp.SetText(counter.ToString());

        if (counter % 5 == 0) EntitySpawner.Self.SpawnEnemy(ScreenBounds.Self.GetRandomScreenPosition(), Quaternion.identity);
    }

}