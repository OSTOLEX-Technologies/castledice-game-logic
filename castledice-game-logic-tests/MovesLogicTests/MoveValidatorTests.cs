using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.TurnsLogic;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

//TODO: Can I improve agility of these tests? Because after modifying MoveValidator constructor I ended up modifying all of these guys.
public class MoveValidatorTests
{
    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfMovePositionIsOutsideOfBoard()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var position = new Vector2Int(100, 100);
        var move = new TestMove(player, position);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }
    
    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfMovePositionIsNegative()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var position = new Vector2Int(-1, -1);
        var move = new TestMove(player, position);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfNoCellWithGivenPosition()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        var position = new Vector2Int(0, 0);
        var move = new TestMove(player, position);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }
    
    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfNotPlayerTurn()
    {
        var player = GetPlayer();
        var otherPlayer = GetPlayer();
        var turnSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player, otherPlayer});
        var board = GetFullNByNBoard(2);
        var position = new Vector2Int(0, 0);
        var move = new TestMove(player, position);
        var validator = new MoveValidator(board, turnSwitcher);
        turnSwitcher.SwitchTurn();
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfPlaceMoveOnCellWithObstacle()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        var obstacle = GetObstacle();
        board[0, 0].AddContent(castle);
        board[1, 1].AddContent(obstacle);
        var position = new Vector2Int(1, 1);
        var move = new PlaceMoveBuilder(){Player = player, Position = position}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfPlaceMoveFarFromPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        var position = new Vector2Int(2, 2);
        var move = new PlaceMoveBuilder(){Player = player, Position = position}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfPlaceMoveOnCellWithEnemyUnit()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        var enemyKnight = new PlayerUnitMock(){Owner = enemy};
        board[1, 1].AddContent(enemyKnight);
        var position = new Vector2Int(1, 1);
        var move = new PlaceMoveBuilder(){Player = player, Position = position}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfPlaceMoveOnCellWithPlayerUnit()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        var knight = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(castle);
        board[1, 1].AddContent(knight);
        var position = new Vector2Int(1, 1);
        var move = new PlaceMoveBuilder(){Player = player, Position = position}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnTrue_IfPlaceMoveNearPlayerUnitsWithNoObstaclesOrEnemies()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        var position = new Vector2Int(1, 1);
        var move = new PlaceMoveBuilder(){Player = player, Position = position}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);

        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfRemoveMoveOnEmptyCell()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        var position = new Vector2Int(1, 1);
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfRemoveMoveOnCellWithNoRemovableObjects()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var obstacle = GetObstacle();
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        board[1, 1].AddContent(obstacle);
        var position = new Vector2Int(1, 1);
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfRemoveMoveFarFromPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        var position = new Vector2Int(2, 2);
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnTrue_IfRemovableObjectOnCellNearPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        var enemyKnight = new PlayerUnitMock(){Owner = enemy};
        board[0, 0].AddContent(castle);
        board[1, 1].AddContent(enemyKnight);
        var position = new Vector2Int(1, 1);
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfUpgradeMoveOnCellWithNoUpgradableObjects()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        var movePosition = new Vector2Int(1, 1);
        var move = new UpgradeMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);

        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfUpgradeMoveOnEnemyUpgradableObject()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        var enemyKnight = new PlayerUnitMock(){Owner = enemy};
        board[0, 0].AddContent(castle);
        board[1, 1].AddContent(enemyKnight);
        var movePosition = new Vector2Int(1, 1);
        var move = new UpgradeMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnTrue_IfUpgradeMoveOnCellWithUpgradablePlayerUnit()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        var movePosition = new Vector2Int(0, 0);
        var move = new UpgradeMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfCaptureMoveOnCellWithNoCapturableObjects()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[0, 0].AddContent(castle);
        var movePosition = new Vector2Int(1, 1);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfCaptureMoveOnAllyCapturableObject()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var castle = new CastleGO(player);
        board[1, 1].AddContent(castle);
        var movePosition = new Vector2Int(1, 1);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnTrue_IfCaptureMoveOnEnemyCapturableObjectNearPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var enemyCastle = new CastleGO(enemy);
        var playerKnight = new PlayerUnitMock(){Owner = player};
        board[9, 9].AddContent(enemyCastle);
        board[8, 8].AddContent(playerKnight);
        var movePosition = new Vector2Int(9, 9);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfCaptureMoveOnEnemyCapturableObjectFarFromPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var enemyCastle = new CastleGO(enemy);
        var playerKnight = new PlayerUnitMock(){Owner = player};
        board[9, 9].AddContent(enemyCastle);
        board[5, 5].AddContent(playerKnight);
        var movePosition = new Vector2Int(9, 9);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldThrowNotImplementedException_ForUnfamiliarMoveTypes()
    {
        var player = GetPlayer();
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(10);
        var movePosition = new Vector2Int(0, 0);
        var testMove = new TestMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);

        Assert.Throws<NotImplementedException>(() => validator.ValidateMove(testMove));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfPlaceMoveTooExpensive()
    {
        var player = GetPlayer(1, 2);
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var board = GetFullNByNBoard(2);
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var contentToPlace = new PlaceableMock(){Cost = 4};
        board[0, 0].AddContent(playerUnit);
        var validator = new MoveValidator(board, turnsSwitcher);
        var position = new Vector2Int(1, 1);
        var move = new PlaceMove(player, position, contentToPlace);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfRemoveMoveIsTooExpensive()
    {
        var player = GetPlayer(1, 2);
        var turnsSwitcher = new PlayerTurnsSwitcher(new List<Player>(){player});
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(2);
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var enemyUnit = new PlayerUnitMock(){Owner = enemy, RemoveCost = 5};
        var replacement = new PlaceableMock() { Cost = 1 };
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(enemyUnit);
        var validator = new MoveValidator(board, turnsSwitcher);
        var position = new Vector2Int(1, 1);
        var move = new RemoveMove(player, position, replacement);
        
        Assert.False(validator.ValidateMove(move));
    }
}