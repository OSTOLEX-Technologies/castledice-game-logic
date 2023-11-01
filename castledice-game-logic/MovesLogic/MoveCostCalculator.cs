using castledice_game_logic.MovesLogic.Rules;

namespace castledice_game_logic.MovesLogic;

internal class MoveCostCalculator : IMoveVisitor<int>
{
    private readonly Board _board;
    
    public MoveCostCalculator(Board board)
    {
        _board = board;
    }

    public int GetMoveCost(AbstractMove move)
    {
        return move.Accept(this);
    }

    public int VisitPlaceMove(PlaceMove move)
    {
        return move.ContentToPlace.GetPlacementCost();
    }

    public int VisitReplaceMove(ReplaceMove move)
    {
        return ReplaceRules.GetReplaceCost(_board, move.Position, move.Replacement);
    }

    public int VisitUpgradeMove(UpgradeMove move)
    {
        return UpgradeRules.GetUpgradeCost(_board, move.Position);
    }

    public int VisitCaptureMove(CaptureMove move)
    {
        return CaptureRules.GetCaptureCost(_board, move.Position, move.Player);
    }

    public int VisitRemoveMove(RemoveMove move)
    {
        return RemoveRules.GetRemoveCost(_board, move.Position);

    }
}