using castledice_game_logic.MovesLogic.Snapshots;

namespace castledice_game_logic.MovesLogic;

public class MoveSaver : IMoveVisitor
{
    private readonly ActionsHistory _history;

    public MoveSaver(ActionsHistory history)
    {
        _history = history;
    }

    public void SaveMove(AbstractMove move)
    {
        move.Accept(this);
    }

    public bool VisitPlaceMove(PlaceMove move)
    {
        _history.History.Add(new PlaceMoveSnapshot(move));
        return true;
    }

    public bool VisitReplaceMove(ReplaceMove move)
    {
        _history.History.Add(new ReplaceMoveSnapshot(move));
        return true;
    }

    public bool VisitUpgradeMove(UpgradeMove move)
    {
        _history.History.Add(new UpgradeMoveSnapshot(move));
        return true;
    }

    public bool VisitCaptureMove(CaptureMove move)
    {
        _history.History.Add(new CaptureMoveSnapshot(move));
        return true;
    }

    public bool VisitRemoveMove(RemoveMove move)
    {
        _history.History.Add(new RemoveMoveSnapshot(move));
        return true;
    }
}