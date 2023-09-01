using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic.Rules;

namespace castledice_game_logic_tests.RulesTests;
using static ObjectCreationUtility;

public class CaptureRulesTests
{
    [Fact]
    public void CanCaptureOnCell_ShouldReturnFalse_IfNegativePositionGiven()
    {
        var board = GetFullNByNBoard(10);
        var position = new Vector2Int(-1, -1);
        var player = GetPlayer();
        
        Assert.False(CaptureRules.CanCaptureOnCell(board, position, player));
    }
    
    [Fact]
    public void CanCaptureOnCell_ShouldReturnFalse_IfNoCellOnGivenPosition()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        var position = new Vector2Int(0, 1);
        var player = GetPlayer();
        
        Assert.False(CaptureRules.CanCaptureOnCell(board, position, player));
    }

    [Fact]
    public void CanCaptureOnCell_ShouldReturnFalse_IfNoCapturablesOnCell()
    {
        var board = GetFullNByNBoard(2);
        var position = new Vector2Int(1, 1);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        
        Assert.False(CaptureRules.CanCaptureOnCell(board, position, player));
    }

    [Fact]
    public void CanCaptureOnCell_ShouldReturnFalse_IfNoNeighboursBelongToPlayer()
    {
        var player = GetPlayer();
        var enemy = GetPlayer();
        var enemyCapturable = new CapturableMock(){Owner = enemy};
        var board = GetFullNByNBoard(2);
        board[1, 1].AddContent(enemyCapturable);
        var position = new Vector2Int(1, 1);
        
        Assert.False(CaptureRules.CanCaptureOnCell(board, position, player));
    }

    [Fact]
    public void CanCaptureOnCell_ShouldReturnTrue_IfCapturableOnCellAndPlayerUnitsAreNear()
    {
        var player = GetPlayer();
        var enemy = GetPlayer();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var enemyCapturable = new CapturableMock(){Owner = enemy};
        var board = GetFullNByNBoard(2);
        board[1, 1].AddContent(enemyCapturable);
        board[1, 0].AddContent(playerUnit);
        var position = new Vector2Int(1, 1);
        
        Assert.True(CaptureRules.CanCaptureOnCell(board, position, player));
    }
    
    [Fact]
    public void CanCaptureOnCell_ShouldReturnFalse_IfCapturingOwnContent()
    {
        var player = GetPlayer();
        var capturable = new CapturableMock(){Owner = player};
        var board = GetFullNByNBoard(2);
        board[1, 1].AddContent(capturable);
        var position = new Vector2Int(1, 1);
        
        Assert.False(CaptureRules.CanCaptureOnCell(board, position, player));
    }

    [Fact]
    public void CanCaptureOnCell_ShouldReturnFalse_IfContentCannotBeCapturedByPlayer()
    {
        var player = GetPlayer();
        var enemy = GetPlayer();
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var capturable = new CapturableMock() { Owner = enemy, CanCapture = false };
        var board = GetFullNByNBoard(2);
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(capturable);
        var position = new Vector2Int(1, 1);
        
        Assert.False(CaptureRules.CanCaptureOnCell(board, position, player));
    }

    [Fact]
    public void CanCaptureOnCellIgnoreNeighbours_ShouldReturnTrue_IfEnemyCapturableOnCell()
    {
        var player = GetPlayer();
        var enemy = GetPlayer();
        var enemyCapturable = new CapturableMock(){Owner = enemy};
        var board = GetFullNByNBoard(2);
        board[1, 1].AddContent(enemyCapturable);
        var position = new Vector2Int(1, 1);
        
        Assert.True(CaptureRules.CanCaptureOnCellIgnoreNeighbours(board, position, player));
    }
}