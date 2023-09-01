using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class CellMovesSelectorTests
{
    public class SelectMoveCellWithoutTypeTestCases : IEnumerable<object[]>
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
            ThreeUnitsCase()
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
    }
    
    
    [Theory]
    [ClassData(typeof(SelectMoveCellWithoutTypeTestCases))]
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
}