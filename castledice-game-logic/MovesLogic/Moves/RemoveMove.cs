using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public class RemoveMove : AbstractMove
{
    private IPlaceable _replacement;

    public IPlaceable Replacement => _replacement;
    
    public RemoveMove(Player player, Vector2Int position, IPlaceable replacement) : base(player, position)
    {
        _replacement = replacement;
    }

    public override bool Accept(IMoveVisitor visitor)
    {
        return visitor.VisitRemoveMove(this);
    }
}