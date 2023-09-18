using castledice_game_logic;
using Moq;

namespace castledice_game_logic_tests;

public class ActionsHistoryTests
{
    [Fact]
    public void GetHistory_ShouldReturnEmptyList_IfNoActionsWereAdded()
    {
        var actionsHistory = new ActionsHistory();

        Assert.Empty(actionsHistory.GetHistory());
    }
    
    [Fact]
    public void GetHistory_ShouldReturnListWithOneAction_IfOneActionWasAdded()
    {
        var actionsHistory = new ActionsHistory();
        var action = GetActionSnapshot();
        actionsHistory.AddActionSnapshot(action);

        Assert.Single(actionsHistory.GetHistory());
    }

    [Fact]
    public void SnapshotAdded_ShouldBeInvoked_IfAddActionCalled()
    {
        var actionsHistory = new ActionsHistory();
        var action = GetActionSnapshot();
        bool isInvoked = false;
        actionsHistory.SnapshotAdded += (sender, snapshot) => isInvoked = true;
        actionsHistory.AddActionSnapshot(action);
        
        Assert.True(isInvoked);
    }

    private static IActionSnapshot GetActionSnapshot()
    {
        return new Mock<IActionSnapshot>().Object;
    }
}