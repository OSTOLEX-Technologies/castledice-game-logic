using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.Utilities;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class CellNeighboursCheckerTests
{
    [Fact]
    public void HasNeighbour_ShouldThrowArgumentException_IfCellPositionIsNegative()
    {
        var board = GetFullNByNBoard(10);
        var position = new Vector2Int(-1, -1);
        Func<Cell, bool> predicate = (c) => c.HasContent();
        
        Assert.Throws<ArgumentException>(() => CellNeighboursChecker.HasNeighbour(board, position, predicate));
    }

    [Fact]
    public void HasNeighbour_ShouldThrowArgumentException_IfNoCellOnPosition()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        var position = new Vector2Int(0, 0);
        Func<Cell, bool> predicate = (c) => c.HasContent();
        
        Assert.Throws<ArgumentException>(() => CellNeighboursChecker.HasNeighbour(board, position, predicate));
    }

    [Fact]
    public void HasNeighbour_ShouldReturnFalse_IfNoCellsSatisfyPredicate()
    {
        var board = GetFullNByNBoard(10);
        var position = new Vector2Int(0, 0);
        Func<Cell, bool> predicate = (c) => c.HasContent();
        
        Assert.False(CellNeighboursChecker.HasNeighbour(board, position, predicate));
    }

    [Fact]
    public void HasNeighbour_ShouldReturnTrue_IfAtLeastOneCellSatisfiesPredicate()
    {
        var board = GetFullNByNBoard(5);
        var position = new Vector2Int(0, 0);
        var content = GetCellContent();
        board[1, 1].AddContent(content);
        Func<Cell, bool> predicate = (c) => c.HasContent();
        
        Assert.True(CellNeighboursChecker.HasNeighbour(board, position, predicate));
    }

    [Fact]
    public void HasNeighbourOwnedByPlayer_ShouldReturnFalse_IfNoNeighboursAreOwnedByPlayer()
    {
        var board = GetFullNByNBoard(5);
        var position = new Vector2Int(2, 2);
        var player = GetPlayer();
        
        Assert.False(CellNeighboursChecker.HasNeighbourOwnedByPlayer(board, position, player));
    }

    [Fact]
    public void HasNeighbourOwnedByPlayer_ShouldReturnTrue_IfAtLeastOneNeighbourIsOwnedByPlayer()
    {        
        var player = GetPlayer();
        var board = GetFullNByNBoard(5);
        var unit = new PlayerUnitMock(){Owner = player};
        board[1, 1].AddContent(unit);
        var position = new Vector2Int(2, 2);
        
        Assert.True(CellNeighboursChecker.HasNeighbourOwnedByPlayer(board, position, player));
    }

}