using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.Utilities;

public static class CellNeighboursChecker
{
    public static bool HasNeighbourOwnedByPlayer(Board board, Vector2Int position, Player player)
    {
        return CellNeighboursChecker.HasNeighbour(board, position,
            c => c.HasContent(content => ContentChecker.ContentBelongsToPlayer(content, player)));
    }
    
    public static bool HasNeighbour(Board board, Vector2Int position, Func<Cell, bool> predicate)
    {
        if (!board.HasCell(position))
        {
            throw new ArgumentException("No cell on position: " + position);
        }
        var neighbours = CellNeighboursGetter.GetNeighbours(board, position, predicate);
        return neighbours.Count > 0;
    }
    
}