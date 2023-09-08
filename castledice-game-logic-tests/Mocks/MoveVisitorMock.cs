using castledice_game_logic.MovesLogic;

namespace castledice_game_logic_tests.Mocks;

public enum VisitMethodType
{
    Place,
    Upgrade,
    Replace,
    Remove,
    Capture
}

public class MoveVisitorMock : IMoveVisitor
{
    public VisitMethodType CalledMethodType { get; private set; }
    
    public bool VisitPlaceMove(PlaceMove move)
    {
        CalledMethodType = VisitMethodType.Place;
        return true;
    }

    public bool VisitReplaceMove(ReplaceMove move)
    {
        CalledMethodType = VisitMethodType.Replace;
        return true;
    }

    public bool VisitUpgradeMove(UpgradeMove move)
    {
        CalledMethodType = VisitMethodType.Upgrade;
        return true;
    }

    public bool VisitCaptureMove(CaptureMove move)
    {
        CalledMethodType = VisitMethodType.Capture;
        return true;
    }

    public bool VisitRemoveMove(RemoveMove move)
    {
        CalledMethodType = VisitMethodType.Remove;
        return true;
    }
}