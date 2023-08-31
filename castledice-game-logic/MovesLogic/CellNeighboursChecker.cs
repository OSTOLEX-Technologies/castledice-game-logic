using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public static class CellNeighboursChecker
{
    public static bool HasNeighbourOwnedByPlayer(Board board, Vector2Int position, Player player)
    {
        return CellNeighboursChecker.HasNeighbour(board, position,
            c => c.HasContent(content => ContentBelongsToPlayer(content, player)));
    }
    
    public static bool HasNeighbour(Board board, Vector2Int position, Func<Cell, bool> predicate)
    {
        if (!board.HasCell(position))
        {
            throw new ArgumentException("No cell on position: " + position);
        }
        for (int i = position.X - 1; i <= position.X + 1; i++)
        {
            for (int j = position.Y - 1; j <= position.Y + 1; j++)
            {
                if (position.X == i && position.Y == j)
                {
                    continue;
                }
                if (board.HasCell(i, j))
                {
                    var neighbour = board[i, j];
                    if (predicate(neighbour))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    private static bool ContentBelongsToPlayer(Content content, Player player)
    {
        if (content is IPlayerOwned)
        {
            var ownedContent = content as IPlayerOwned;
            var owner = ownedContent.GetOwner();
            return player == owner;
        }
        return false;
    }
}