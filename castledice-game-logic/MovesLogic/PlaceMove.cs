using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public class PlaceMove : AbstractMove
{
    private Content _content;

    public Content Content => _content;
    
    public PlaceMove(Player player, Vector2Int position, Content content) : base(player, position)
    {
        _content = content;
    }
}