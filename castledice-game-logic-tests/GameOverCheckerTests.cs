using System.Collections;
using castledice_game_logic;

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

        private object[] OneCastleOwnedByPlayer()
        {
            var board = GetFullNByNBoard(2);
            board[0, 0].AddContent(GetCastle(GetPlayer()));
            return new object[] { board, true };
        }

        private object[] TwoCastlesOwnedByOnePlayer()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(GetCastle(player));
            board[1, 1].AddContent(GetCastle(player));
            return new object[] { board, true };
        }
        
        private object[] TwoCastlesOwnedByDifferentPlayers()
        {
            var board = GetFullNByNBoard(2);
            var firstPlayer = GetPlayer();
            var secondPlayer = GetPlayer();
            board[0, 0].AddContent(GetCastle(firstPlayer));
            board[1, 1].AddContent(GetCastle(secondPlayer));
            return new object[] { board, false };
        }

        private object[] OneOfTwoCastlesIsFree()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(GetCastle(player));
            board[1, 1].AddContent(GetCastle(new NullPlayer()));
            return new object[] { board, true };
        }
    }
    
    [Theory]
    [ClassData(typeof(IsGameOverTestCases))]
    public void IsGameOver_ShouldReturn_IfOnlyOneCastlesOwnerOnBoard(Board board, bool isGameOver)
    {
        var checker = new GameOverChecker(board);
        
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
        var checker = new GameOverChecker(board);
        
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
        var checker = new GameOverChecker(board);
        
        Assert.Same(castlesOwner, checker.GetWinner());
    }
}