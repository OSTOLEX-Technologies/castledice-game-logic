namespace castledice_game_logic.GameObjects;

public abstract class Content
{
    public abstract void Update();

    public abstract void Accept(IContentVisitor visitor);
}