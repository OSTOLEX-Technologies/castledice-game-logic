using castledice_game_logic;
using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;


public class PlayerTurnsSwitcherTests
{
    [Fact]
    public void GetCurrent_ShouldReturnFirstPlayerFromList_IfSwitchTurnNotCalled()
    {
        var playersList = new List<Player>() { GetPlayer() };
        var turnSwitcher = new PlayerTurnsSwitcher(playersList);

        var actualPlayer = turnSwitcher.GetCurrent();
        var expectedPlayer = playersList[0];
        
        Assert.Same(expectedPlayer, actualPlayer);
    }

    [Fact]
    public void SwitchTurn_ShouldSwitchCurrentPlayer_ToNextFromTheList()
    {
        var playersList = new List<Player>() { GetPlayer(), GetPlayer() };
        var turnSwitcher = new PlayerTurnsSwitcher(playersList);
        
        turnSwitcher.SwitchTurn();
        var actualPlayer = turnSwitcher.GetCurrent();
        var expectedPlayer = playersList[1];
        
        Assert.Same(expectedPlayer, actualPlayer);
    }

    [Fact]
    public void SwitchTurn_ShouldSwitchPlayers_InOrderOfList()
    {
        var playersList = new List<Player>()
        {
            GetPlayer(),
            GetPlayer(),
            GetPlayer(),
            GetPlayer()
        };
        var turnsSwitcher = new PlayerTurnsSwitcher(playersList);

        for (int i = 0; i < playersList.Count; i++)
        {
            var expectedCurrent = playersList[i];
            var actualCurrent = turnsSwitcher.GetCurrent();
            Assert.Same(expectedCurrent, actualCurrent);
            turnsSwitcher.SwitchTurn();
        }
    }

    [Fact]
    public void SwitchTurn_ShouldSwitchToFirstPlayer_IfReachedEndOfList()
    {
        var playersList = new List<Player>()
        {
            GetPlayer(),
            GetPlayer(),
            GetPlayer(),
            GetPlayer()
        };
        var turnsSwitcher = new PlayerTurnsSwitcher(playersList);

        for (int i = 0; i < playersList.Count; i++)
        {
            turnsSwitcher.SwitchTurn();
        }

        var actualPlayer = turnsSwitcher.GetCurrent();
        var expectedPlayer = playersList[0];
        
        Assert.Same(expectedPlayer, actualPlayer);
    }
}