using System;

public sealed class Point : Collectable
{
    public static event EventHandler Collected;

    protected override void GetCollected()
    {
        base.GetCollected();
        Collected?.Invoke(this, EventArgs.Empty);
    }
    
}