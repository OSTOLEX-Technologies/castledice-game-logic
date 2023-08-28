using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public class RemoveMove : AbstractMove
{
    private Content _replacement;

    public Content Replacement => _replacement;
    
    public RemoveMove(Player player, Vector2Int position, Content replacement) : base(player, position)
    {
        _replacement = replacement;
    }
}