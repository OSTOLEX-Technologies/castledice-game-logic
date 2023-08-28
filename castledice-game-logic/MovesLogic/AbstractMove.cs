using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public abstract class AbstractMove 
{
    protected Player _player;
    protected Vector2Int _position;

    public Player Player => _player;
    public Vector2Int Position => _position;

    protected AbstractMove(Player player, Vector2Int position)
    {
        _player = player;
        _position = position;
    }
    
    
}