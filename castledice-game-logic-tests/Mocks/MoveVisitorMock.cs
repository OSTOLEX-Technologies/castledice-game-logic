using castledice_game_logic.MovesLogic;

namespace castledice_game_logic_tests.Mocks;

public enum VisitMethodType
{
    Place,
    Upgrade,
    Remove,
    Capture
}

public class MoveVisitorMock : IMoveVisitor
{
    public VisitMethodType CalledMethod { get; private set; }
    
    public bool VisitPlaceMove(PlaceMove move)
    {
        CalledMethod = VisitMethodType.Place;
        return true;
    }

    public bool VisitRemoveMove(RemoveMove move)
    {
        CalledMethod = VisitMethodType.Remove;
        return true;
    }

    public bool VisitUpgradeMove(UpgradeMove move)
    {
        CalledMethod = VisitMethodType.Upgrade;
        return true;
    }

    public bool VisitCaptureMove(CaptureMove move)
    {
        CalledMethod = VisitMethodType.Capture;
        return true;
    }
}