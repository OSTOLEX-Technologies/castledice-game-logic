namespace castledice_game_logic.MovesLogic;

public interface IMoveVisitor
{
    bool VisitPlaceMove(PlaceMove move);
    bool VisitReplaceMove(ReplaceMove move);
    bool VisitUpgradeMove(UpgradeMove move);
    bool VisitCaptureMove(CaptureMove move);
}