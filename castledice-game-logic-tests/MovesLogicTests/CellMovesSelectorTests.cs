using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class CellMovesSelectorTests
{
    private class SelectCellMovesTestCases : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            TwoUnitsOnBoardCase(),
            TwoUnitsOnBoardCaseForSecondPlayer(),
            TwoUnitsOfOnePlayerCase(),
            PlayerUnitAndEnemyUnitCase(),
            UnitAndEnemyCapturableCase(),
            UnitAndEnemyCapturableThatCannotBeCapturedCase(),
            TwoUnitsAndObstacleCase(),
            TwoUnitsObstacleAndEnemyUnitCase(),
            ThreeUnitsCase(),
            UnitAndRemovableCase()
        };
        
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] TwoUnitsOnBoardCase()
        {
            var firstPlayer = GetPlayer();
            var secondPlayer = GetPlayer();
            var firstUnit = new PlayerUnitMock(){Owner = firstPlayer};
            var secondUnit = new PlayerUnitMock(){Owner = secondPlayer};
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(firstUnit);
            board[9, 9].AddContent(secondUnit);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Upgrade),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
                new CellMove(board[1, 1], MoveType.Place)
            };
            return new object[] { board, firstPlayer, expectedCells };
        }
        
        private static object[] TwoUnitsOnBoardCaseForSecondPlayer()
        {
            var firstPlayer = GetPlayer();
            var secondPlayer = GetPlayer();
            var firstCastle = new PlayerUnitMock(){Owner = firstPlayer};
            var secondCastle = new PlayerUnitMock(){Owner = secondPlayer};
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(firstCastle);
            board[9, 9].AddContent(secondCastle);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[9, 9], MoveType.Upgrade),
                new CellMove(board[9, 8], MoveType.Place),
                new CellMove(board[8, 9], MoveType.Place),
                new CellMove(board[8, 8], MoveType.Place)
            };
            return new object[] { board, secondPlayer, expectedCells };
        }

        private static object[] TwoUnitsOfOnePlayerCase()
        {
            var player = GetPlayer();
            var firstUnit = new PlayerUnitMock() { Owner = player };
            var secondUnit = new PlayerUnitMock() { Owner = player };
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(firstUnit);
            board[1, 1].AddContent(secondUnit);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Upgrade),
                new CellMove(board[1, 1], MoveType.Upgrade),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
                new CellMove(board[2, 2], MoveType.Place),
                new CellMove(board[2, 1], MoveType.Place),
                new CellMove(board[1, 2], MoveType.Place),
                new CellMove(board[0, 2], MoveType.Place),
                new CellMove(board[2, 0], MoveType.Place)
            };
            return new object[] { board, player, expectedCells };
        }

        private static object[] PlayerUnitAndEnemyUnitCase()
        {
            var player = GetPlayer();
            var enemyPlayer = GetPlayer();
            var playerUnit = new PlayerUnitMock() { Owner = player };
            var enemyUnit = new PlayerUnitMock() { Owner = enemyPlayer };
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(playerUnit);
            board[1, 1].AddContent(enemyUnit);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Upgrade),
                new CellMove(board[1, 1], MoveType.Replace),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
            };
            return new object[] { board, player, expectedCells };
        }

        private static object[] UnitAndEnemyCapturableCase()
        {
            var player = GetPlayer();
            var enemyPlayer = GetPlayer();
            var playerUnit = new PlayerUnitMock(){Owner =  player};
            var enemyCapturable = new CapturableMock(){Owner = enemyPlayer};
            var board = GetFullNByNBoard(10);
            board[9, 9].AddContent(enemyCapturable);
            board[8, 9].AddContent(playerUnit);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[9, 9], MoveType.Capture),
                new CellMove(board[8, 9], MoveType.Upgrade),
                new CellMove(board[9, 8], MoveType.Place),
                new CellMove(board[8, 8], MoveType.Place),
                new CellMove(board[7, 8], MoveType.Place),
                new CellMove(board[7, 9], MoveType.Place)
            };
            return new object[] { board, player, expectedCells };
        }
        
        private static object[] UnitAndEnemyCapturableThatCannotBeCapturedCase()
        {
            var player = GetPlayer();
            var enemyPlayer = GetPlayer();
            var playerUnit = new PlayerUnitMock(){Owner =  player};
            var enemyCapturable = new CapturableMock(){Owner = enemyPlayer, CanCapture = false};
            var board = GetFullNByNBoard(10);
            board[9, 9].AddContent(enemyCapturable);
            board[8, 9].AddContent(playerUnit);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[8, 9], MoveType.Upgrade),
                new CellMove(board[9, 8], MoveType.Place),
                new CellMove(board[8, 8], MoveType.Place),
                new CellMove(board[7, 8], MoveType.Place),
                new CellMove(board[7, 9], MoveType.Place)
            };
            return new object[] { board, player, expectedCells };
        }

        private static object[] TwoUnitsAndObstacleCase()
        {
            var player = GetPlayer();
            var firstUnit = new PlayerUnitMock(){Owner = player};
            var secondUnit = new PlayerUnitMock(){Owner = player};
            var obstacle = GetObstacle();
            var board = GetFullNByNBoard(10);
            board[1, 1].AddContent(firstUnit);
            board[2, 2].AddContent(secondUnit);
            board[1, 2].AddContent(obstacle);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Place),
                new CellMove(board[1, 1], MoveType.Upgrade),
                new CellMove(board[2, 2], MoveType.Upgrade),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[0, 2], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
                new CellMove(board[1, 3], MoveType.Place),
                new CellMove(board[2, 0], MoveType.Place),
                new CellMove(board[2, 1], MoveType.Place),
                new CellMove(board[2, 3], MoveType.Place),
                new CellMove(board[3, 1], MoveType.Place),
                new CellMove(board[3, 2], MoveType.Place),
                new CellMove(board[3, 3], MoveType.Place)
            };
            return new object[] { board, player, expectedCells };
        }

        private static object[] TwoUnitsObstacleAndEnemyUnitCase()
        {
            var player = GetPlayer();
            var enemyPlayer = GetPlayer();
            var firstUnit = new PlayerUnitMock(){Owner = player};
            var secondUnit = new PlayerUnitMock(){Owner = player};
            var enemyUnit = new PlayerUnitMock(){Owner = enemyPlayer};
            var obstacle = GetObstacle();
            var board = GetFullNByNBoard(10);
            board[1, 1].AddContent(firstUnit);
            board[2, 2].AddContent(secondUnit);
            board[1, 2].AddContent(obstacle);
            board[1, 3].AddContent(enemyUnit);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Place),
                new CellMove(board[1, 1], MoveType.Upgrade),
                new CellMove(board[2, 2], MoveType.Upgrade),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[0, 2], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
                new CellMove(board[1, 3], MoveType.Replace),
                new CellMove(board[2, 0], MoveType.Place),
                new CellMove(board[2, 1], MoveType.Place),
                new CellMove(board[2, 3], MoveType.Place),
                new CellMove(board[3, 1], MoveType.Place),
                new CellMove(board[3, 2], MoveType.Place),
                new CellMove(board[3, 3], MoveType.Place)
            };
            return new object[] { board, player, expectedCells };
        }
        
        private static object[] ThreeUnitsCase()
        {
            var player = GetPlayer();
            var firstUnit = new PlayerUnitMock(){Owner = player};
            var secondUnit = new PlayerUnitMock(){Owner = player};
            var thirdUnit = new PlayerUnitMock(){Owner = player};
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(firstUnit);
            board[0, 1].AddContent(secondUnit);
            board[1, 0].AddContent(thirdUnit);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Upgrade),
                new CellMove(board[0, 1], MoveType.Upgrade),
                new CellMove(board[1, 0], MoveType.Upgrade),
                new CellMove(board[1, 1], MoveType.Place),
                new CellMove(board[0, 2], MoveType.Place),
                new CellMove(board[1, 2], MoveType.Place),
                new CellMove(board[2, 0], MoveType.Place),
                new CellMove(board[2, 1], MoveType.Place),
            };
            return new object[] { board, player, expectedCells };
        }

        private static object[] UnitAndRemovableCase()
        {
            var player = GetPlayer();
            var unit = new PlayerUnitMock() { Owner = player };
            var board = GetFullNByNBoard(3);
            var removable = new RemovableMock();
            board[0, 0].AddContent(unit);
            board[1, 1].AddContent(removable);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Upgrade),
                new CellMove(board[1, 1], MoveType.Remove),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
            };
            return new object[] { board, player, expectedCells };
        }
    }

    private class NoMovePossibleTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return CellTooFarFromPlayerUnitsCase();
            yield return CellWithObstacleCase();
            yield return CapturableCannotBeCapturedCase();
            yield return CapturableIsTooExpensiveCase();
            yield return CannotCaptureOwnCapturableCase();
            yield return UpgradeableCannotBeUpgradedCase();
            yield return UpgradeableIsTooExpensiveCase();
            yield return ReplaceableIsTooExpensiveCase();
            yield return RemovableCannotBeRemovedCase();
            yield return RemovableIsTooExpensiveCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] CellTooFarFromPlayerUnitsCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer();
            var playerUnit = new PlayerUnitMock(){Owner = player};
            board[0, 0].AddContent(playerUnit);
            var cellPosition = new Vector2Int(2, 2);

            return new object[] { board, player, cellPosition };
        }

        private static object[] CellWithObstacleCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer();
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var obstacle = GetObstacle();
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(obstacle);

            return new object[] { board, player, cellPosition };
        }

        private static object[] CapturableCannotBeCapturedCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer();
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var capturable = new CapturableMock(){CanCapture = false};
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(capturable);

            return new object[] { board, player, cellPosition };
        }

        private static object[] CapturableIsTooExpensiveCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer(actionPoints: 2);
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var capturable = new CapturableMock(){GetCaptureCostHitFunc = (p) => 6};
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(capturable);

            return new object[] { board, player, cellPosition };
        }
        
        private static object[] CannotCaptureOwnCapturableCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer(actionPoints: 2);
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var capturable = new CapturableMock() { Owner = player };
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(capturable);

            return new object[] { board, player, cellPosition };
        }

        private static object[] UpgradeableCannotBeUpgradedCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer();
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var upgradeable = new UpgradeableMock(){Owner = player, Upgradeable = false};
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(upgradeable);

            return new object[] { board, player, cellPosition };
        }
        
        private static object[] UpgradeableIsTooExpensiveCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer(actionPoints: 2);
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var upgradeable = new UpgradeableMock(){Owner = player, UpgradeCost = 6};
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(upgradeable);

            return new object[] { board, player, cellPosition };
        }
        
        private static object[] ReplaceableIsTooExpensiveCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer(actionPoints: 2);
            var enemyPlayer = GetPlayer();
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var replaceable = new ReplaceableMock() { Owner = enemyPlayer, ReplaceCost = 6};
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(replaceable);

            return new object[] { board, player, cellPosition };
        }

        private static object[] RemovableCannotBeRemovedCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer(actionPoints: 6); 
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var removable = new RemovableMock(){CanRemove = false};
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(removable);

            return new object[] { board, player, cellPosition };
        }

        private static object[] RemovableIsTooExpensiveCase()
        {
            var board = GetFullNByNBoard(3);
            var player = GetPlayer(actionPoints: 2); 
            var playerUnit = new PlayerUnitMock(){Owner = player};
            var cellPosition = new Vector2Int(1, 1);
            var removable = new RemovableMock(){CanRemove = true, RemoveCost = 5};
            board[0, 0].AddContent(playerUnit);
            board[cellPosition].AddContent(removable);

            return new object[] { board, player, cellPosition };
        }
    }
    
    
    [Theory]
    [ClassData(typeof(SelectCellMovesTestCases))]
    public void SelectMoveCells_ShouldReturnListWithMoveCells_WithCorrespondingCellsAndMoveTypes(Board board, Player player, List<CellMove> expectedCells)
    {
        var cellsSelector = new CellMovesSelector(board);

        var actualCells = cellsSelector.SelectCellMoves(player);
        
        Assert.Equal(expectedCells.Count, actualCells.Count);
        foreach (var expectedCell in expectedCells)
        {
            Assert.Contains(expectedCell, actualCells);
        }
    }

    [Theory]
    [ClassData(typeof(NoMovePossibleTestCases))]
    public void GetCellMove_ShouldReturnCellMoveWithNoneType_IfNoMovePossibleOnCell(Board board, Player player,
        Vector2Int cellPosition)
    {
        var cell = board[cellPosition];
        var cellMovesSelector = new CellMovesSelector(board);
        
        var cellMove = cellMovesSelector.GetCellMoveForCell(player, cell);
        
        Assert.Equal(MoveType.None, cellMove.MoveType);
    }
    
    [Fact]
    public void GetCellMove_ShouldReturnCellMoveWithUpgradeType_IfUpgradePossibleOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer(actionPoints: 6);
        var playerUnit = new UpgradeableMock(){Owner = player, UpgradeCost = 1};
        board[0, 0].AddContent(playerUnit);
        var cell = board[0, 0];
        var cellMovesSelector = new CellMovesSelector(board);
        
        var cellMove = cellMovesSelector.GetCellMoveForCell(player, cell);
        
        Assert.Equal(MoveType.Upgrade, cellMove.MoveType);
    }
    
    [Fact]
    public void GetCellMove_ShouldReturnCellMoveWithPlaceType_IfPlacePossibleOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer(actionPoints: 6);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var cell = board[0, 1];
        var cellMovesSelector = new CellMovesSelector(board);
        
        var cellMove = cellMovesSelector.GetCellMoveForCell(player, cell);
        
        Assert.Equal(MoveType.Place, cellMove.MoveType);
    }
    
    [Fact]
    public void GetCellMove_ShouldReturnCellMoveWithCaptureType_IfCapturePossibleOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer(actionPoints: 6);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var enemyPlayer = GetPlayer();
        var enemyCapturable = new CapturableMock(){Owner = enemyPlayer, CanCapture = true, GetCaptureCostHitFunc = (p) => 1};
        board[0, 0].AddContent(playerUnit);
        board[0, 1].AddContent(enemyCapturable);
        var cell = board[0, 1];
        var cellMovesSelector = new CellMovesSelector(board);
        
        var cellMove = cellMovesSelector.GetCellMoveForCell(player, cell);
        
        Assert.Equal(MoveType.Capture, cellMove.MoveType);
    }
    
    [Fact]
    public void GetCellMove_ShouldReturnCellMoveWithReplaceType_IfReplacePossibleOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer(actionPoints: 6);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var enemyPlayer = GetPlayer();
        var enemyUnit = new ReplaceableMock(){Owner = enemyPlayer, ReplaceCost = 2};
        board[0, 0].AddContent(playerUnit);
        board[0, 1].AddContent(enemyUnit);
        var cell = board[0, 1];
        var cellMovesSelector = new CellMovesSelector(board);
        
        var cellMove = cellMovesSelector.GetCellMoveForCell(player, cell);
        
        Assert.Equal(MoveType.Replace, cellMove.MoveType);
    }

    [Fact]
    public void GetCellMove_ShouldReturnCellMoveWithRemoveType_IfRemovePossibleOnCell()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer(actionPoints: 6);
        var playerUnit = new PlayerUnitMock(){Owner = player};
        var removable = new RemovableMock() { RemoveCost = 2, CanRemove = true };
        board[0, 0].AddContent(playerUnit);
        board[0, 1].AddContent(removable);
        var cell = board[0, 1];
        var cellMovesSelector = new CellMovesSelector(board);
        
        var cellMove = cellMovesSelector.GetCellMoveForCell(player, cell);
        
        Assert.Equal(MoveType.Remove, cellMove.MoveType);
    }
}