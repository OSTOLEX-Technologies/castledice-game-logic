using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic_tests.TurnsLogicTests;
using static ObjectCreationUtility;


public class PlayerTurnsSwitcherTests
{
    [Fact]
    public void PlayerTurnSwitcher_ShouldImplementICurrentPlayerProvider()
    {
        var switcher = GetTurnsSwitcher(GetPlayer());
        Assert.True(switcher is ICurrentPlayerProvider);
    }
    
    [Fact]
    public void GetCurrent_ShouldReturnFirstPlayerFromList_IfSwitchTurnNotCalled()
    {
        var playersList = GetPlayersList(2);
        var turnSwitcher = new PlayerTurnsSwitcher(new PlayersList(playersList));

        var actualPlayer = turnSwitcher.GetCurrentPlayer();
        var expectedPlayer = playersList[0];
        
        Assert.Same(expectedPlayer, actualPlayer);
    }

    [Fact]
    public void SwitchTurn_ShouldSwitchCurrentPlayer_ToNextFromTheList()
    {
        var playersList = GetPlayersList(2);
        var turnSwitcher = new PlayerTurnsSwitcher(new PlayersList(playersList));
        
        turnSwitcher.SwitchTurn();
        var actualPlayer = turnSwitcher.GetCurrentPlayer();
        var expectedPlayer = playersList[1];
        
        Assert.Same(expectedPlayer, actualPlayer);
    }

    [Fact]
    public void SwitchTurn_ShouldSwitchPlayers_InOrderOfList()
    {
        var playersList = GetPlayersList(4);
        var turnsSwitcher = new PlayerTurnsSwitcher(new PlayersList(playersList));

        for (int i = 0; i < playersList.Count; i++)
        {
            var expectedCurrent = playersList[i];
            var actualCurrent = turnsSwitcher.GetCurrentPlayer();
            Assert.Same(expectedCurrent, actualCurrent);
            turnsSwitcher.SwitchTurn();
        }
    }

    [Fact]
    public void SwitchTurn_ShouldSwitchToFirstPlayer_IfReachedEndOfList()
    {
        var playersList = GetPlayersList(4);
        var turnsSwitcher = new PlayerTurnsSwitcher(new PlayersList(playersList));

        for (int i = 0; i < playersList.Count; i++)
        {
            turnsSwitcher.SwitchTurn();
        }

        var actualPlayer = turnsSwitcher.GetCurrentPlayer();
        var expectedPlayer = playersList[0];
        
        Assert.Same(expectedPlayer, actualPlayer);
    }
    
    [Fact]
    public void SwitchTurn_ShouldInvokeTurnSwitchedEvent()
    {
        var playersList = GetPlayersList(4);
        var turnsSwitcher = new PlayerTurnsSwitcher(new PlayersList(playersList));
        bool eventInvoked = false;
        turnsSwitcher.TurnSwitched += (sender, args) => eventInvoked = true;

        turnsSwitcher.SwitchTurn();
        
        Assert.True(eventInvoked);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_IfEmptyPlayerListGiven()
    {
        var playersList = new PlayersList();
        Assert.Throws<ArgumentException>(() => new PlayerTurnsSwitcher(playersList));
    }

    private static PlayersList GetPlayersList(int playersCount)
    {
        var list = new PlayersList();
        for (int i = 1; i <= playersCount; i++)
        {
            list.AddPlayer(GetPlayer(id: i));
        }

        return list;
    }
}