using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests.ActionPointsLogicTests;
using static ObjectCreationUtility;

public class ChangeActionPointsActionTests
{
    private readonly Random _rnd = new Random();
    
    [Fact]
    public void PlayerProperty_ShouldReturnPlayer_GivenInConstructor()
    {
        var player = GetPlayer();
        int amount = 5;
        var action = new ChangeActionPointsAction(player, amount);
        
        Assert.Same(player, action.Player);
    }

    [Fact]
    public void AmountProperty_ShouldReturnNumber_GivenInConstructor()
    {
        var player = GetPlayer();
        int amount = _rnd.Next(0, 3) - _rnd.Next(0, 3);
        var action = new ChangeActionPointsAction(player, amount);
        
        Assert.Equal(amount, action.Amount); 
    }
    
    [Fact]
    public void GetSnapshot_ShouldReturnGiveActionPointsSnapshot()
    {
        var player = GetPlayer();
        var amount = 6;
        var action = new ChangeActionPointsAction(player, amount);

        var snapshot = action.GetSnapshot();
        
        Assert.True(snapshot is ChangeActionPointsSnapshot);
    }

    [Fact]
    public void GetSnapshot_ShouldReturnSnapshot_WithCorrespondingFields()
    {
        int playerActionPoints = 4;
        int id = 3;
        int amount = _rnd.Next(0, 3) - _rnd.Next(0, 3);
        var player = GetPlayer(actionPoints: playerActionPoints, id: id);
        var action = new ChangeActionPointsAction(player, amount);

        var snapshot = action.GetSnapshot();
        var giveApSnapshot = snapshot as ChangeActionPointsSnapshot;
        if (giveApSnapshot is null)
        {
            Assert.Fail("Snapshot is not GiveActionPointsSnapshot");
        }
        Assert.Equal(amount, giveApSnapshot.Amount);
        Assert.Equal(player.Id, giveApSnapshot.PlayerId);
    }
}