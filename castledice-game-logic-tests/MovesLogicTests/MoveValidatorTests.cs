using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.TurnsLogic;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class MoveValidatorTests
{
    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfMovePositionIsOutsideOfBoard()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
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
        var turnsSwitcher = GetTurnsSwitcher(player);
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
        var turnsSwitcher = GetTurnsSwitcher(player);
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
        var player = GetPlayer(id: 1);
        var turnSwitcher = GetTurnsSwitcher(player, GetPlayer(id: 2));
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
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var obstacle = GetObstacle();
        board[0, 0].AddContent(playerUnit);
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
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var position = new Vector2Int(2, 2);
        var move = new PlaceMoveBuilder(){Player = player, Position = position}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfPlaceMoveOnCellWithEnemyUnit()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var playerKnight = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerKnight);
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
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var knight = new PlayerUnitMock(){Owner = player};
        board[1, 1].AddContent(knight);
        var position = new Vector2Int(1, 1);
        var move = new PlaceMoveBuilder(){Player = player, Position = position}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfPlaceMoveIsTooExpensive()
    {
        var player = GetPlayer(actionPoints: 1);
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var knight = new PlayerUnitMock(){Owner = player};
        var contentToPlace = new PlaceableMock() { Cost = 3 };
        board[1, 1].AddContent(knight);
        var position = new Vector2Int(1, 1);
        var move = new PlaceMoveBuilder(){Player = player, Position = position, Content = contentToPlace}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }
    
    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfPlaceMoveTooExpensive()
    {
        var player = GetPlayer(actionPoints: 2);
        var turnsSwitcher = GetTurnsSwitcher(player);
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
    //Place move is valid if it satisfies following conditions:
    //Done on cell without any obstacles or units
    //Done on cell that have at least one neighbour with player unit on it
    //Player has enough action points to place object.
    public void ValidateMove_ShouldReturnTrue_IfPlaceIsValid()
    {
        var player = GetPlayer(actionPoints: 6);
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var position = new Vector2Int(1, 1);
        var move = new PlaceMoveBuilder(){Player = player, Position = position}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);

        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfReplaceMoveOnEmptyCell()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var position = new Vector2Int(1, 1);
        var move = new ReplaceMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfReplaceMoveOnCellWithNoReplaceableObjects()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var obstacle = GetObstacle();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(obstacle);
        var position = new Vector2Int(1, 1);
        var move = new ReplaceMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfReplaceMoveFarFromPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var position = new Vector2Int(2, 2);
        var move = new ReplaceMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    //This method tests the situation when player has enough action points to
    //replace object with cheap unit that costs 1 action points, but 
    //tries to replace with more expensive unit, so that replace cost
    //is too gib.
    public void ValidateMove_ShouldReturnFalse_IfReplaceMoveIsTooExpensive()
    {
        var player = GetPlayer(actionPoints: 3);
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var replaceable = new ReplaceableMock() { ReplaceCost = 2 };
        var replacement = new PlaceableMock() { Cost = 3 };
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(replaceable);
        var position = new Vector2Int(1, 1);
        var move = new ReplaceMoveBuilder() { Player = player, Position = position, Replacement = replacement}.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }
    
    [Fact]
    //This method tests the situation when player tries to replace object that is simply too expensive to replace.
    public void ValidateMove_ShouldReturnFalse_IfReplaceMoveOnObjectExpensiveToReplace()
    {
        var player = GetPlayer(actionPoints: 2);
        var turnsSwitcher = GetTurnsSwitcher(player);
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(2);
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var enemyUnit = new PlayerUnitMock(){Owner = enemy, RemoveCost = 5};
        var replacement = new PlaceableMock() { Cost = 1 };
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(enemyUnit);
        var validator = new MoveValidator(board, turnsSwitcher);
        var position = new Vector2Int(1, 1);
        var move = new ReplaceMove(player, position, replacement);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnTrue_IfReplaceableObjectOnCellNearPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var enemyKnight = new PlayerUnitMock(){Owner = enemy};
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(enemyKnight);
        var position = new Vector2Int(1, 1);
        var move = new ReplaceMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfRemoveMoveOnCellWithNoRemovables()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(3);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(GetObstacle());//Obstacle is not a removable
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfRemoveMoveFarFromPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(3);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var removable = new RemovableMock();
        var position = new Vector2Int(2, 2);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(removable);
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfRemoveMoveOnRemovableThatCannotBeRemoved()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(3);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var removable = new RemovableMock(){CanRemove = false};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(removable);
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    //Removable objects cost action points to remove and player may not have enough action points.
    public void ValidateMove_ShouldReturnFalse_IfNotEnoughActionPointsForRemoveMove()
    {
        var player = GetPlayer(actionPoints: 2);
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(3);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var removable = new RemovableMock(){CanRemove = true, RemoveCost = 5};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(removable);
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }
    
    [Fact]
    public void ValidateMove_ShouldReturnTrue_IfRemoveMoveOnRemovableNearPlayerUnits()
    {
        var player = GetPlayer(actionPoints: 2);
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(3);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var removable = new RemovableMock(){CanRemove = true, RemoveCost = 1};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(removable);
        var move = new RemoveMoveBuilder() { Player = player, Position = position }.Build();
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfUpgradeMoveOnCellWithNoUpgradableObjects()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var movePosition = new Vector2Int(1, 1);
        var move = new UpgradeMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);

        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfUpgradeMoveOnEnemyUpgradableObject()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var enemyKnight = new PlayerUnitMock(){Owner = enemy};
        board[0, 0].AddContent(playerUnit);
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
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new UpgradeableMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var movePosition = new Vector2Int(0, 0);
        var move = new UpgradeMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfCaptureMoveOnCellWithNoCapturableObjects()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var movePosition = new Vector2Int(1, 1);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfCaptureMoveOnAllyCapturableObject()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[1, 1].AddContent(playerUnit);
        var movePosition = new Vector2Int(1, 1);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }
    
    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfCaptureMoveOnEnemyCapturableObjectFarFromPlayerUnits()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var enemyCapturable = new CapturableMock(){Owner = enemy};
        var playerKnight = new PlayerUnitMock(){Owner = player};
        board[9, 9].AddContent(enemyCapturable);
        board[5, 5].AddContent(playerKnight);
        var movePosition = new Vector2Int(9, 9);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldReturnFalse_IfCaptureMoveOnCapturableThatCannotBeCaptured()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var enemyCapturable = new CapturableMock(){Owner = enemy, CanCapture = false};
        var playerKnight = new PlayerUnitMock(){Owner = player};
        board[9, 9].AddContent(enemyCapturable);
        board[8, 8].AddContent(playerKnight);
        var movePosition = new Vector2Int(9, 9);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.False(validator.ValidateMove(move));
    }

    [Fact]
    //CaptureHit move is valid if it satisfies following conditions:
    //Cell contains capturable object and this object belongs to enemy
    //At least one cell neighbour has player unit on it
    //Player can perform capture hit and has enough action points.
    public void ValidateMove_ShouldReturnTrue_IfValidCaptureMove()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var enemy = GetPlayer();
        var board = GetFullNByNBoard(10);
        var enemyCapturable = new CapturableMock(){Owner = enemy};
        var playerKnight = new PlayerUnitMock(){Owner = player};
        board[9, 9].AddContent(enemyCapturable);
        board[8, 8].AddContent(playerKnight);
        var movePosition = new Vector2Int(9, 9);
        var move = new CaptureMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);
        
        Assert.True(validator.ValidateMove(move));
    }

    [Fact]
    public void ValidateMove_ShouldThrowNotImplementedException_ForUnfamiliarMoveTypes()
    {
        var player = GetPlayer();
        var turnsSwitcher = GetTurnsSwitcher(player);
        var board = GetFullNByNBoard(10);
        var movePosition = new Vector2Int(0, 0);
        var testMove = new TestMove(player, movePosition);
        var validator = new MoveValidator(board, turnsSwitcher);

        Assert.Throws<NotImplementedException>(() => validator.ValidateMove(testMove));
    }


}