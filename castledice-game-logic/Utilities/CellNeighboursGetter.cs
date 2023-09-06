using castledice_game_logic.Math;

namespace castledice_game_logic.Utilities;

public static class CellNeighboursGetter
{
    public static List<Cell> GetNeighbours(Board board, Vector2Int position, Func<Cell, bool> predicate)
    {
        if (!board.HasCell(position))
        {
            throw new ArgumentException("No cell on position: " + position);
        }
        List<Cell> cells = new List<Cell>();
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
                        cells.Add(neighbour);
                    }
                }
            }
        }
        return cells;
    }
}