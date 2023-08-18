using castledice_game_logic.Board;
using castledice_game_logic.Exceptions;

namespace castledice_game_logic_tests;

public class RectBoardTests
{
    [Fact]
    public void TestGetCellThrowsExceptionOnInvalidCoordinates()
    {
        var board = new RectBoard(10, 10);

        Assert.Throws<CellNotFoundException>(() => board.GetCell(11, 11));
    }

    [Fact]
    public void TestGetCellReturnsTileOnValidCoordinates()
    {
        var board = new RectBoard(10, 10);

        var tile = board.GetCell(1, 1);
        
        Assert.False(tile == null);
    }
}