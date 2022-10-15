using System;
using System.Collections;
using UnityEngine;

public abstract class TimeBasedPowerUpData : PowerUpData
{
    [SerializeField] private float _duration;
    private float _timer = 0;

    public override void ApplyPowerUp() {}
    
    protected IEnumerator StartTimeBasedPowerUp(Action before, Action after)
    {
        before.Invoke(); // execute instruction before wait time

        // wait
        _timer = _duration;
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
            
            PowerUpTimer.Instance.SetTimer((int)_timer);
        }
        
        after.Invoke(); // execute instruction after wait time
    }

}