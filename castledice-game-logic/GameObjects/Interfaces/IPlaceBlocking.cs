namespace castledice_game_logic.GameObjects;

/// <summary>
/// If cell has content that implements this interface than no other content can be placed on this cell.
/// </summary>
public interface IPlaceBlocking
{
    bool IsBlocking()
    {
        return true;
    }
}