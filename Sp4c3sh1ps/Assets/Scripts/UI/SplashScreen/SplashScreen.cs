using UnityEngine;

public abstract class SplashScreen : MonoBehaviour
{
    [SerializeField] private GameObject _contentObject;

    protected virtual void OpenSelf() => _contentObject.SetActive(true);
    protected virtual void CloseSelf() => _contentObject.SetActive(false);
    
}