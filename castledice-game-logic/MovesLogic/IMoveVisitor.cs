namespace castledice_game_logic.MovesLogic;

public interface IMoveVisitor<out T>
{
    T VisitPlaceMove(PlaceMove move);
    T VisitReplaceMove(ReplaceMove move);
    T VisitUpgradeMove(UpgradeMove move);
    T VisitCaptureMove(CaptureMove move);
    T VisitRemoveMove(RemoveMove move);
}