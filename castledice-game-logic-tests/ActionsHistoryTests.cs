using castledice_game_logic;

namespace castledice_game_logic_tests;

public class ActionsHistoryTests
{
    [Fact]
    public void History_ShouldReturnEmptyList_IfNoActionsWereAdded()
    {
        var actionsHistory = new ActionsHistory();

        Assert.Empty(actionsHistory.History);
    }
}