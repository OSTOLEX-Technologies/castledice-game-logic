using castledice_game_logic.Math;

namespace castledice_game_logic.Board;

public interface IBoard
{
    public Cell GetCell(int x, int y);

    public Cell GetCell(Vector2Int coordinate);
}