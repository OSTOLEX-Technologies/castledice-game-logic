namespace castledice_game_logic.GameObjects;

public class Stone : Content, IPlaceBlocking
{
    public bool IsBlocking()
    {
        return true;
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }

    public override T Accept<T>(IContentVisitor<T> visitor)
    {
        throw new NotImplementedException();
    }
}