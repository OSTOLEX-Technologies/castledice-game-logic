using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic.Rules;

namespace castledice_game_logic_tests.RulesTests;
using static ObjectCreationUtility;

public class PlaceRulesTests
{
    [Fact]
    public void CanPlaceOnCell_ShouldReturnFalse_IfCellPositionIsNegative()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer();
        var unit = new PlayerUnitMock() { Owner = player };
        board[0,0].AddContent(unit);
        var position = new Vector2Int(-1, -1);
        
        Assert.False(PlaceRules.CanPlaceOnCell(board, position, player));
    }
    
    [Fact]
    public void CanPlaceOnCell_ShouldReturnFalse_IfNoCellOnPosition()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        var player = GetPlayer();
        var unit = new PlayerUnitMock() { Owner = player };
        board[1, 1].AddContent(unit);
        var position = new Vector2Int(0, 0);
        
        Assert.False(PlaceRules.CanPlaceOnCell(board, position, player));
    }

    [Fact]
    public void CanPlaceOnCell_ShouldReturnFalse_IfNoPlayerUnitsNearby()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var position = new Vector2Int(0, 0);
        
        Assert.False(PlaceRules.CanPlaceOnCell(board, position, player));
    }

    [Fact]
    public void CanPlaceOnCell_ShouldReturnFalse_IfObstacleOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var unit = new PlayerUnitMock() { Owner = player };
        var obstacle = GetObstacle();
        board[1,1].AddContent(unit);
        board[0,0].AddContent(obstacle);
        var position = new Vector2Int(0, 0);
        
        Assert.False(PlaceRules.CanPlaceOnCell(board, position, player));
    }
    
    [Fact]
    public void CanPlaceOnCell_ShouldReturnFalse_IfPlayerUnitOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var unit = new PlayerUnitMock() { Owner = player };
        board[1,1].AddContent(unit);
        var position = new Vector2Int(1, 1);
        
        Assert.False(PlaceRules.CanPlaceOnCell(board, position, player));
    }
    
    [Fact]
    public void CanPlaceOnCell_ShouldReturnFalse_IfEnemyOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var enemy = GetPlayer();
        var unit = new PlayerUnitMock() { Owner = player };
        var enemyUnit = new PlayerUnitMock() { Owner = enemy };
        board[1,1].AddContent(unit);
        board[0, 0].AddContent(enemyUnit);
        var position = new Vector2Int(0, 0);
        
        Assert.False(PlaceRules.CanPlaceOnCell(board, position, player));
    }

    [Fact]
    public void CanPlaceOnCell_ShouldReturnTrue_IfUnitsNearbyAndNoObstacles()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var unit = new PlayerUnitMock() { Owner = player };
        board[1,1].AddContent(unit);
        var position = new Vector2Int(0, 0);
        
        Assert.True(PlaceRules.CanPlaceOnCell(board, position, player));
    }

    [Fact]
    public void CanPlaceOnCellIgnoreNeighbours_ShouldReturnTrue_IfNoObstaclesOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var position = new Vector2Int(0, 0);
        
        Assert.True(PlaceRules.CanPlaceOnCellIgnoreNeighbours(board, position, player));
    }
}