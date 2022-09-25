using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public sealed class ScreenBounds : MonoBehaviour
{
    public static ScreenBounds Self { get; private set; }
    
    [SerializeField] private float wrapOffset = 0.2f;
    private Camera mainCamera;
    private BoxCollider2D boundingBox;

    private void Awake()
    {
        if (Self != null && Self != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Self = this;
            
            mainCamera = Camera.main;
            boundingBox = GetComponent<BoxCollider2D>();
            boundingBox.isTrigger = true; // bounding box has to be trigger
        }
    }

    private void Start()
    {
        transform.position = Vector2.zero;
        
        // set bounds size
        var y = mainCamera.orthographicSize * 2;
        boundingBox.size = new Vector2(y * mainCamera.aspect, y);
    }

    /// <summary>
    /// Checks if position is out of bounds
    /// </summary>
    /// <param name="worldPos"> world position to check if it is out of screen bounds </param>
    /// <returns> bool value that determines whether provided position is out of screen bounds </returns>
    public bool IsOutOfBounds(Vector3 worldPos)
    {
        return Mathf.Abs(worldPos.x) > Mathf.Abs(boundingBox.bounds.min.x)
               || Mathf.Abs(worldPos.y) > Mathf.Abs(boundingBox.bounds.min.y);
    }

    /// <summary>
    /// Calculate wrapped position of an object
    /// </summary>
    /// <param name="tr"> transform used to calculate its wrapped position </param>
    /// <returns> wrapped position </returns>
    public Vector2 CalculateWrappedPosition(Transform tr)
    {
        // var outOfX = Mathf.Abs(worldPos.x) > Mathf.Abs(boundingBox.bounds.min.x) - cornerOffset;
        // var outOfY = Mathf.Abs(worldPos.y) > Mathf.Abs(boundingBox.bounds.min.y) - cornerOffset;
        // var signWorldPos = new Vector2(Mathf.Sign(worldPos.x), Mathf.Sign(worldPos.y));

        // if (outOfX && outOfY) return Vector2.Scale(worldPos, -Vector2.one) + Vector2.Scale(new Vector2(wrapOffset, wrapOffset), signWorldPos);
        // if (outOfX) return new Vector2(-worldPos.x, worldPos.y) + new Vector2((wrapOffset * signWorldPos.x), 0);
        // if (outOfY) return new Vector2(worldPos.x, -worldPos.y) + new Vector2(0, (wrapOffset * signWorldPos.y));

        //return worldPos;

        var warpedPosition = (Vector2)(tr.position - tr.up * 20);
        var div = 1f;

        if (Mathf.Abs(warpedPosition.x) > Mathf.Abs(warpedPosition.y))
        {
            if (Mathf.Abs(warpedPosition.x) > boundingBox.size.x * 0.5f)
            {
                div = (boundingBox.size.x * 0.5f) / Mathf.Abs(warpedPosition.x);
                warpedPosition *= div;
            }
            if (Mathf.Abs(warpedPosition.y) > boundingBox.size.y * 0.5f)
            {
                div = (boundingBox.size.y * 0.5f) / Mathf.Abs(warpedPosition.y);
                warpedPosition *= div;
            }
        }
        else
        {
            if (Mathf.Abs(warpedPosition.y) > boundingBox.size.y * 0.5f)
            {
                div = (boundingBox.size.y * 0.5f) / Mathf.Abs(warpedPosition.y);
                warpedPosition *= div;
            }
            if (Mathf.Abs(warpedPosition.x) > boundingBox.size.x * 0.5f)
            {
                div = (boundingBox.size.x * 0.5f) / Mathf.Abs(warpedPosition.x);
                warpedPosition *= div;
            }
        }
        
        return warpedPosition + (Vector2)(tr.up * wrapOffset);
    }

} 