namespace castledice_game_logic;

public class CellContent
{
    public int PlayerId { get; }

    public ContentType Type { get; }

    public CellContent(int playerId, ContentType type)
    {
        PlayerId = playerId;
        Type = type;
    }
}