using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;
using static castledice_game_logic_tests.ObjectCreationUtility;
namespace castledice_game_logic_tests;

public class PlayerActionPointsSaverTests
{
    [Fact]
    public void SaveAction_ShouldAddSnapshotToHistory()
    {
        var history = new ActionsHistory();
        int playerId = 1;
        int amountToGive = 3;
        var player = GetPlayer(1, playerId);
        var action = new GiveActionPointsAction(player, amountToGive);
        var expectedSnapshot = action.GetSnapshot();
        var saver = new GiveActionPointsSaver(history);

        saver.SaveAction(action);
        var actualSnapshot = history.GetHistory()[0] as GiveActionPointsSnapshot;
        
        Assert.Equal(expectedSnapshot, actualSnapshot);
    }
}