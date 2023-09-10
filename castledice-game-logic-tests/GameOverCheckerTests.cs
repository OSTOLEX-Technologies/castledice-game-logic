using castledice_game_logic;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;


public class GameOverCheckerTests
{
    [Fact]
    public void IsGameOver_ShouldReturnFalse_IfTwoOrMoreCastlesOwnersOnBoard()
    {
        var board = GetFullNByNBoard(10);
        var firstCastle = GetCastle(GetPlayer());
        var secondCastle = GetCastle(GetPlayer());
        board[1, 1].AddContent(firstCastle);
        board[4, 4].AddContent(secondCastle);
        var checker = new GameOverChecker(board);
        
        Assert.False(checker.IsGameOver());
    }
    
    [Fact]
    public void IsGameOver_ShouldReturnTrue_IfOnlyOneCastlesOwnerOnBoard()
    {
        var board = GetFullNByNBoard(10);
        var castlesOwner = GetPlayer();
        var firstCastle = GetCastle(castlesOwner);
        var secondCastle = GetCastle(castlesOwner);
        board[1, 1].AddContent(firstCastle);
        board[4, 4].AddContent(secondCastle);
        var checker = new GameOverChecker(board);
        
        Assert.True(checker.IsGameOver());
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