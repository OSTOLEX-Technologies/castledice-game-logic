using castledice_game_logic;
using castledice_game_logic;
using castledice_game_logic.Math;

namespace castledice_game_logic_tests;

public static class ObjectCreationUtility
{
    public static Board GetFullNByNBoard(int size)
    {
        var board = new Board(CellType.Square);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board.AddCell(i, j);
            }
        }
        return board;
    }
    
    public static Cell GetCell()
    {
        return new Cell(new Vector2Int(0, 0));
    }
    
    public static Player GetPlayer()
    {
        return new Player();
    }
}