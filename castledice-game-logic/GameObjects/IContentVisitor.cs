namespace castledice_game_logic.GameObjects;

public interface IContentVisitor<out T>
{
    T VisitTree(Tree tree);
    T VisitCastle(Castle castle);
    T VisitKnight(Knight knight);
}