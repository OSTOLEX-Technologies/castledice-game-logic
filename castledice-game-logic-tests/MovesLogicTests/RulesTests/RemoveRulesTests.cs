using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic.Rules;

namespace castledice_game_logic_tests.RulesTests;
using static ObjectCreationUtility;

public class RemoveRulesTests
{
    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfPositionIsNegative()
    {
        var board = GetFullNByNBoard(5);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var position = new Vector2Int(-1, -1);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfNoCellOnPosition()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[1, 1].AddContent(playerUnit);
        var position = new Vector2Int(0, 0);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }
    
    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfNothingToRemove()
    {
        var board = GetFullNByNBoard(5);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var position = new Vector2Int(1, 1);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfNoPlayerUnitsNearby()
    {
        var board = GetFullNByNBoard(5);
        var player = GetPlayer();
        var removable = new RemovableMock();
        var position = new Vector2Int(1, 1);
        board[1, 1].AddContent(removable);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfPlayerHasNotEnoughActionPoints()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(3);
        var removable = new RemovableMock()
        {
            RemoveCost = 5
        };
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(removable);
        var position = new Vector2Int(1, 1);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    //TODO: Reconsider this test
    //Conditions for player to remove object on cell are following:
    //Object must implement IRemovable interface
    //Object must not belong to player
    //There must be player units in nearby cells
    //Player must have enough action points
    public void CanRemoveOnCell_ShouldReturnTrue_IfPlayerSatisfiesRemoveConditions()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer();
        var removable = new RemovableMock()
        {
            RemoveCost = 3
        };
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(removable);
        var position = new Vector2Int(1, 1);
        
        Assert.True(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    public void CanRemoveOnCell_ShouldReturnFalse_IfRemovableBelongsToPlayer()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer();
        var removable = new RemovableMock()
        {
            RemoveCost = 3,
            Owner = player
        };
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(removable);
        var position = new Vector2Int(1, 1);
        
        Assert.False(RemoveRules.CanRemoveOnCell(board, position, player));
    }

    [Fact]
    //CanRemoveOnCellIgnoreNeighbours should take into account following rules:
    //Cell must contain removable object
    //Removable object must not belong to player
    //Remove cost must be less than player's action points
    public void CanRemoveOnCellIgnoreNeighbours_ShouldReturnTrue_EvenIfNoPlayerUnitsNearby()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer();
        var removable = new RemovableMock()
        {
            RemoveCost = 3
        };
        board[1, 1].AddContent(removable);
        var position = new Vector2Int(1, 1);
        
        Assert.True(RemoveRules.CanRemoveOnCellIgnoreNeighbours(board, position, player));
    }
}