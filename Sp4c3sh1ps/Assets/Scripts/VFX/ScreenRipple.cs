using UnityEngine;
 
public class ScreenRipple : MonoBehaviour
{
    public static ScreenRipple Instance { get; private set; }

    #region CACHED SHADER PROPERTIES
    private static readonly int Amount = Shader.PropertyToID("_Amount");
    private static readonly int CenterX = Shader.PropertyToID("_CenterX");
    private static readonly int CenterY = Shader.PropertyToID("_CenterY");
    #endregion
    
    #region INSPECTOR FIELDS
    [SerializeField] private Material _rippleMaterial;
    [SerializeField] private float _maxAmount = 50f;
    [SerializeField, Range(0,1)] private float _friction = 0.9f;
    #endregion

    private float _amount = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        // if (Input.GetMouseButton(0)) 
        // BeginRipple(Vector3.zero);

        _rippleMaterial.SetFloat(Amount, _amount);
        _amount *= _friction;
    }

    public void BeginRipple(Vector3 pos)
    {
        _amount = _maxAmount;
        _rippleMaterial.SetFloat(CenterX, pos.x);
        _rippleMaterial.SetFloat(CenterY, pos.y);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst) => Graphics.Blit(src, dst, _rippleMaterial);
    
}