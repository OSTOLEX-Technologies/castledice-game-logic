using castledice_game_logic;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class BoardValidatorTests
{
    [Fact]
    public void BoardCastlesAreValid_ShouldReturnFalse_IfPlayersListDoesntCorrespondsToCastlesOnBoard()
    {
        var board = GetFullNByNBoard(10);
        var firstPlayer = GetPlayer();
        var secondPlayer = GetPlayer();
        var castle = GetCastle(firstPlayer);
        board[0, 0].AddContent(castle);
        var playersList = new List<Player>()
        {
            firstPlayer,
            secondPlayer
        };
        var validator = new BoardValidator();
        
        Assert.False(validator.BoardCastlesAreValid(playersList, board));
    }
}