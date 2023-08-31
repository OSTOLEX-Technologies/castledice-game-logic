using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public class PlaceMove : AbstractMove
{
    private IPlaceable _contentToPlace;

    public IPlaceable ContentToPlace => _contentToPlace;
    
    public PlaceMove(Player player, Vector2Int position, IPlaceable contentToPlace) : base(player, position)
    {
        _contentToPlace = contentToPlace;
    }

    public override bool Accept(IMoveVisitor visitor)
    {
        return visitor.VisitPlaceMove(this);
    }
}