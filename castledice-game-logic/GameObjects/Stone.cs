namespace castledice_game_logic.GameObjects;

public class Stone : Content, IPlaceBlocking
{
    public bool IsBlocking()
    {
        return true;
    }
}