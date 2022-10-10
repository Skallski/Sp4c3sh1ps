using SkalluUtils.PropertyAttributes;
using UnityEngine;

public sealed class GameControlsManager : MonoBehaviour
{
    public static GameControlsManager Self { get; private set; }

    [field: SerializeField, ReadOnlyInspector] public bool Left { get; set; }
    [field: SerializeField, ReadOnlyInspector] public bool Right { get; set; }

    private void Awake()
    {
        if (Self != null && Self != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Self = this;
        }
    }

    #region BUTTON ACTIONS
    public void TurnLeft()
    {
        if (Right) Right = false;
        Left = true;
    }

    public void TurnRight()
    {
        if (Left) Left = false;
        Right = true;
    }
    #endregion

}