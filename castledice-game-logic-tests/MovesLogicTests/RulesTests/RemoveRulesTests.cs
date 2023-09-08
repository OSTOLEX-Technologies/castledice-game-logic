using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic.Rules;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.RulesTests;

public class RemoveRulesTests
{
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(1, 0)]
    [InlineData(10, 10)]
    public void CanRemoveOnCell_ShouldReturnFalse_IfNoCellOnPosition(int x, int y)
    {
        var player = GetPlayer();
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        board.AddCell(0, 0);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, new Vector2Int(x, y), player));
    }

    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfNothingToRemove()
    {
        var player = GetPlayer();
        var board = GetFullNByNBoard(2);
        var unit = new PlayerUnitMock() { Owner = player };
        board[0, 0].AddContent(unit);
        var position = new Vector2Int(1, 1);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfNoPlayerUnitsNearby()
    {
        var player = GetPlayer();
        var board = GetFullNByNBoard(2);
        var removable = new RemovableMock();
        var position = new Vector2Int(1, 1);
        board[position].AddContent(removable);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfRemoveIsTooExpensive()
    {
        var player = GetPlayer(actionPoints: 2);
        var board = GetFullNByNBoard(2);
        var unit = new PlayerUnitMock() { Owner = player };
        var removable = new RemovableMock() { RemoveCost = 4 };
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(unit);
        board[position].AddContent(removable);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfRemovableCannotBeRemoved()
    {
        var player = GetPlayer(actionPoints: 6);
        var board = GetFullNByNBoard(2);
        var unit = new PlayerUnitMock() { Owner = player };
        var removable = new RemovableMock() { RemoveCost = 2, CanRemove = false};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(unit);
        board[position].AddContent(removable);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    
    //Player can remove object on cell if all of the following conditions are met:
    //There is a removable on position and it can be removed
    //Player has enough action points to remove object
    //Player units are nearby cell with removable object
    public void CanRemoveOnCell_ShouldReturnTrue_IfRemoveIsValid()
    {
        var player = GetPlayer(actionPoints: 6);
        var board = GetFullNByNBoard(2);
        var unit = new PlayerUnitMock() { Owner = player };
        var removable = new RemovableMock() { RemoveCost = 2, CanRemove = true};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(unit);
        board[position].AddContent(removable);
        
        Assert.True(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    public void CanRemoveOnCellIgnoreNeighbours_ShouldReturnTrue_EvenIfNoPlayerUnitsNearby()
    {
        var player = GetPlayer(actionPoints: 6);
        var board = GetFullNByNBoard(2);
        var removable = new RemovableMock() { RemoveCost = 2, CanRemove = true};
        var position = new Vector2Int(1, 1);
        board[position].AddContent(removable);
        
        Assert.True(RemoveRules.CanRemoveOnCellIgnoreNeighbours(board, position, player));
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(1, 0)]
    [InlineData(10, 10)]
    public void GetRemoveCost_ShouldThrowArgumentException_IfNoCellOnPosition(int x, int y)
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        board.AddCell(0, 0);
        
        Assert.Throws<ArgumentException>(()=>RemoveRules.GetRemoveCost(board, new Vector2Int(x, y)));
    }

    [Fact]
    public void GetRemoveCost_ShouldThrowArgumentException_IfNoRemovableOnPosition()
    {
        var board = GetFullNByNBoard(2);
        var position = new Vector2Int(1, 1);

        Assert.Throws<ArgumentException>(() => RemoveRules.GetRemoveCost(board, position));
    }

    [Fact]
    public void GetRemoveCost_ShouldReturnRemoveCost_OfRemovableOnCell()
    {
        var board = GetFullNByNBoard(3);
        int expectedCost = 2;
        var removable = new RemovableMock() {RemoveCost = expectedCost};
        var position = new Vector2Int(1, 1);
        board[position].AddContent(removable);

        int actualCost = RemoveRules.GetRemoveCost(board, position);
        
        Assert.Equal(expectedCost, actualCost);
    }
}