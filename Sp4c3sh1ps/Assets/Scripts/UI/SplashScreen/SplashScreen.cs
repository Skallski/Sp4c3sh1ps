using UnityEngine;

public abstract class SplashScreen : MonoBehaviour
{
    [SerializeField] private GameObject contentObject;

    protected void OpenSelf() => contentObject.SetActive(true);
    protected void CloseSelf() => contentObject.SetActive(false);
    
}