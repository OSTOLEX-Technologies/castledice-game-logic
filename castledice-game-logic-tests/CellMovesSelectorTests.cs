using System.Collections;
using castledice_game_logic;
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
            TwoCastlesOnBoardCase(),
            TwoCastlesOnBoardCaseForSecondPlayer(),
            CastleAndKnightCase(),
            KnightAndEnemyCase(),
            KnigthAndEnemyCastleCase(),
            KnightCastleAndObstacleCase(),
            CastleKnightObstacleAndEnemyKnightCase(),
            CastleAndTwoKnightsCase()
        };
        
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] TwoCastlesOnBoardCase()
        {
            var firstPlayer = GetPlayer();
            var secondPlayer = GetPlayer();
            var firstCastle = new CastleGO(firstPlayer);
            var secondCastle = new CastleGO(secondPlayer);
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(firstCastle);
            board[9, 9].AddContent(secondCastle);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Upgrade),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
                new CellMove(board[1, 1], MoveType.Place)
            };
            return new object[] { board, firstPlayer, expectedCells };
        }
        
        private static object[] TwoCastlesOnBoardCaseForSecondPlayer()
        {
            var firstPlayer = GetPlayer();
            var secondPlayer = GetPlayer();
            var firstCastle = new CastleGO(firstPlayer);
            var secondCastle = new CastleGO(secondPlayer);
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

        private static object[] CastleAndKnightCase()
        {
            var player = GetPlayer();
            var castle = new CastleGO(player);
            var knight = new Knight(player);
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(castle);
            board[1, 1].AddContent(knight);
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

        private static object[] KnightAndEnemyCase()
        {
            var player = GetPlayer();
            var enemyPlayer = GetPlayer();
            var knight = new Knight(player);
            var enemy = new Knight(enemyPlayer);
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(knight);
            board[1, 1].AddContent(enemy);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Upgrade),
                new CellMove(board[1, 1], MoveType.Remove),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
            };
            return new object[] { board, player, expectedCells };
        }

        private static object[] KnigthAndEnemyCastleCase()
        {
            var player = GetPlayer();
            var enemyPlayer = GetPlayer();
            var knight = new Knight(player);
            var enemyCastle = new CastleGO(enemyPlayer);
            var board = GetFullNByNBoard(10);
            board[9, 9].AddContent(enemyCastle);
            board[8, 9].AddContent(knight);
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

        private static object[] KnightCastleAndObstacleCase()
        {
            var player = GetPlayer();
            var castle = new CastleGO(player);
            var knight = new Knight(player);
            var obstacle = GetObstacle();
            var board = GetFullNByNBoard(10);
            board[1, 1].AddContent(castle);
            board[2, 2].AddContent(knight);
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

        private static object[] CastleKnightObstacleAndEnemyKnightCase()
        {
            var player = GetPlayer();
            var enemyPlayer = GetPlayer();
            var castle = new CastleGO(player);
            var knight = new Knight(player);
            var enemyKnight = new Knight(enemyPlayer);
            var obstacle = GetObstacle();
            var board = GetFullNByNBoard(10);
            board[1, 1].AddContent(castle);
            board[2, 2].AddContent(knight);
            board[1, 2].AddContent(obstacle);
            board[1, 3].AddContent(enemyKnight);
            List<CellMove> expectedCells = new List<CellMove>()
            {
                new CellMove(board[0, 0], MoveType.Place),
                new CellMove(board[1, 1], MoveType.Upgrade),
                new CellMove(board[2, 2], MoveType.Upgrade),
                new CellMove(board[0, 1], MoveType.Place),
                new CellMove(board[0, 2], MoveType.Place),
                new CellMove(board[1, 0], MoveType.Place),
                new CellMove(board[1, 3], MoveType.Remove),
                new CellMove(board[2, 0], MoveType.Place),
                new CellMove(board[2, 1], MoveType.Place),
                new CellMove(board[2, 3], MoveType.Place),
                new CellMove(board[3, 1], MoveType.Place),
                new CellMove(board[3, 2], MoveType.Place),
                new CellMove(board[3, 3], MoveType.Place)
            };
            return new object[] { board, player, expectedCells };
        }
        
        private static object[] CastleAndTwoKnightsCase()
        {
            var player = GetPlayer();
            var castle = new CastleGO(player);
            var firstKnight = new Knight(player);
            var secondKnight = new Knight(player);
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(castle);
            board[0, 1].AddContent(firstKnight);
            board[1, 0].AddContent(secondKnight);
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
    
    
    //TODO: Ask if it is a good idea to make such a generalized test
    [Theory]
    [ClassData(typeof(SelectMoveCellWithoutTypeTestCases))]
    public void SelectMoveCells_ShouldReturnListWithMoveCells_WithCorrespondingCellsAndMoveTypes(Board board, Player player, List<CellMove> expectedCells)
    {
        var cellsSelector = new CellMovesSelector(board);

        var actualCells = cellsSelector.SelectMoveCells(player);
        
        Assert.Equal(expectedCells.Count, actualCells.Count);
        foreach (var expectedCell in expectedCells)
        {
            Assert.Contains(expectedCell, actualCells);
        }
    }
}