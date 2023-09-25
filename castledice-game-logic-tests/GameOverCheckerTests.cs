using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.TurnsLogic;
using Moq;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;


public class GameOverCheckerTests
{
    private class IsGameOverTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return OneCastleOwnedByPlayer();
            yield return TwoCastlesOwnedByOnePlayer();
            yield return TwoCastlesOwnedByDifferentPlayers();
            yield return OneOfTwoCastlesIsFree();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] OneCastleOwnedByPlayer()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(GetCastle(player));
            var currentPlayerProvider = GetCurrentPlayerProvider(player);
            return new object[] { board, true, currentPlayerProvider };
        }

        private static object[] TwoCastlesOwnedByOnePlayer()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(GetCastle(player));
            board[1, 1].AddContent(GetCastle(player));
            var currentPlayerProvider = GetCurrentPlayerProvider(player);
            return new object[] { board, true, currentPlayerProvider};
        }
        
        private static object[] TwoCastlesOwnedByDifferentPlayers()
        {
            var board = GetFullNByNBoard(2);
            var firstPlayer = GetPlayer();
            var secondPlayer = GetPlayer();
            board[0, 0].AddContent(GetCastle(firstPlayer));
            board[1, 1].AddContent(GetCastle(secondPlayer));
            var currentPlayerProvider = GetCurrentPlayerProvider(firstPlayer);
            return new object[] { board, false, currentPlayerProvider };
        }

        private static object[] OneOfTwoCastlesIsFree()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(GetCastle(player));
            board[1, 1].AddContent(GetCastle(new NullPlayer()));
            var currentPlayerProvider = GetCurrentPlayerProvider(player);
            return new object[] { board, true, currentPlayerProvider };
        }
    }
    
    [Theory]
    [ClassData(typeof(IsGameOverTestCases))]
    public void IsGameOver_ShouldReturn_IfOnlyOneCastlesOwnerOnBoard(Board board, bool isGameOver, ICurrentPlayerProvider currentPlayerProvider)
    {
        var cellMovesSelector = new CellMovesSelector(board);
        var checker = new GameOverChecker(board, currentPlayerProvider, cellMovesSelector);
        
        Assert.Equal(isGameOver, checker.IsGameOver());
    }

    [Fact]
    public void GetWinner_ShouldReturnNullPlayer_IfGameIsNotOver()
    {
        var board = GetFullNByNBoard(10);
        var firstCastle = GetCastle(GetPlayer());
        var secondCastle = GetCastle(GetPlayer());
        board[1, 1].AddContent(firstCastle);
        board[4, 4].AddContent(secondCastle);
        var cellMovesSelector = new CellMovesSelector(board);
        var currentPlayerProvider = GetCurrentPlayerProvider(GetPlayer());
        var checker = new GameOverChecker(board, currentPlayerProvider, cellMovesSelector);
        
        Assert.True(checker.GetWinner().IsNull);
    }
    
    [Fact]
    public void GetWinner_ShouldReturnCastlesOwner_IfOneCastlesOwnerOnBoard()
    {
        
        var board = GetFullNByNBoard(10);
        var castlesOwner = GetPlayer();
        var firstCastle = GetCastle(castlesOwner);
        var secondCastle = GetCastle(castlesOwner);
        board[1, 1].AddContent(firstCastle);
        board[4, 4].AddContent(secondCastle);
        var cellMovesSelector = new CellMovesSelector(board);
        var currentPlayerProvider = GetCurrentPlayerProvider(GetPlayer());
        var checker = new GameOverChecker(board, currentPlayerProvider, cellMovesSelector);
        
        Assert.Same(castlesOwner, checker.GetWinner());
    }

    [Fact]
    public void IsDraw_ShouldReturnFalse_IfCurrentPlayerCanMakeMove()
    {
        var board = GetFullNByNBoard(10);
        var player = GetPlayer(actionPoints: 6);
        var unit = new PlayerUnitMock() { Owner = player };
        board[0, 0].AddContent(unit);
        var cellMovesSelector = new CellMovesSelector(board);
        var currentPlayerProvider = GetCurrentPlayerProvider(player);
        var checker = new GameOverChecker(board, currentPlayerProvider, cellMovesSelector);
        
        Assert.False(checker.IsDraw());
    }

    [Fact]
    public void IsDraw_ShouldReturnTrue_IfCurrentPlayerCannotMakeMoveAndHasActionPoints()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 6);
        var unit = new PlayerUnitMock() { Owner = player, CanUpgrade = false};
        board[0, 0].AddContent(unit);
        board[1, 1].AddContent(GetObstacle());
        board[1, 0].AddContent(GetObstacle());
        board[0, 1].AddContent(GetObstacle());
        var cellMovesSelector = new CellMovesSelector(board);
        var currentPlayerProvider = GetCurrentPlayerProvider(player);
        var checker = new GameOverChecker(board, currentPlayerProvider, cellMovesSelector);
        
        Assert.True(checker.IsDraw());
    }

    private static ICurrentPlayerProvider GetCurrentPlayerProvider(Player currentPlayer)
    {
        var currentPlayerProviderMock = new Mock<ICurrentPlayerProvider>();
        currentPlayerProviderMock.Setup(p => p.GetCurrentPlayer()).Returns(currentPlayer);
        var currentPlayerProvider = currentPlayerProviderMock.Object;
        return currentPlayerProvider;
    }
}