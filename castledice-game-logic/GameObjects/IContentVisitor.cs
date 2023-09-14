namespace castledice_game_logic.GameObjects;

public interface IContentVisitor
{
    void VisitTree(Tree tree);
    void VisitCastle(Castle castle);
    void VisitKnight(Knight knight);
}