using castledice_game_logic.Exceptions;
using castledice_game_logic.Math;

namespace castledice_game_logic.Board;

public class RectBoard : IBoard
{
    private Cell[,] _cells;
    
    public RectBoard(int width, int height)
    {
        _cells = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _cells[i, j] = new Cell();
            }   
        }
    }
    
    public Cell GetCell(Vector2Int coordinate)
    {
        return GetCell(coordinate.X, coordinate.Y);
    }
    
    public Cell GetCell(int x, int y)
    {
        if (x >= _cells.GetLength(0) || y >= _cells.GetLength(1))
        {
            throw new CellNotFoundException();
        }

        return _cells[x, y];
    }
}