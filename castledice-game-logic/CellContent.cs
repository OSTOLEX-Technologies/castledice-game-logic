namespace castledice_game_logic;

public class CellContent
{
    private int _playerID;
    private ContentType _type;
    
    public int PlayerID => _playerID;
    public ContentType Type => _type;

    public CellContent(int playerId, ContentType type)
    {
        _playerID = playerId;
        _type = type;
    }
}