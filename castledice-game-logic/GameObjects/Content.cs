namespace castledice_game_logic.GameObjects;

public abstract class Content
{
    public event EventHandler? StateModified;
    
    public abstract void Update();

    public abstract T Accept<T>(IContentVisitor<T> visitor);
    
    /// <summary>
    /// This method should be invoked whenever the state of the content is modified.
    /// </summary>
    protected virtual void OnStateModified()
    {
        StateModified?.Invoke(this, EventArgs.Empty);
    }
}