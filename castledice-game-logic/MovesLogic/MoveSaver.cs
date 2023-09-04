using castledice_game_logic.MovesLogic.Rules;
using castledice_game_logic.MovesLogic.Snapshots;

namespace castledice_game_logic.MovesLogic;

public class MoveSaver : IMoveVisitor
{
    private ActionsHistory _history;
    private Board _board;

    public MoveSaver(ActionsHistory history, Board board)
    {
        _history = history;
        _board = board;
    }

    public void SaveMove(AbstractMove move)
    {
        move.Accept(this);
    }

    public bool VisitPlaceMove(PlaceMove move)
    {
        int moveCost = PlaceRules.GetPlaceCost(move.ContentToPlace);
        _history.History.Add(new PlaceMoveSnapshot(move, moveCost));
        return true;
    }

    public bool VisitReplaceMove(ReplaceMove move)
    {
        int moveCost = ReplaceRules.GetReplaceCost(_board, move.Position, move.Replacement);
        _history.History.Add(new ReplaceMoveSnapshot(move, moveCost));
        return true;
    }

    public bool VisitUpgradeMove(UpgradeMove move)
    {
        int moveCost = UpgradeRules.GetUpgradeCost(_board, move.Position);
        _history.History.Add(new UpgradeMoveSnapshot(move, moveCost));
        return true;
    }

    public bool VisitCaptureMove(CaptureMove move)
    {
        int moveCost = CaptureRules.GetCaptureCost(_board, move.Position, move.Player);
        _history.History.Add(new CaptureMoveSnapshot(move, moveCost));
        return true;
    }
}