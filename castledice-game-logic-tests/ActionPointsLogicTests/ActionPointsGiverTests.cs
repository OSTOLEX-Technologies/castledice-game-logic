using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.Math;
using Moq;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class ActionPointsGiverTests
{
    [Fact]
    public void GiveActionPoints_ShouldReturnActionWithPlayer_GivenInConstructor()
    {
        int minAmount = 1;
        int maxAmount = 6;
        var player = GetPlayer();
        var randomizerMock = new Mock<IRandomNumberGenerator>();
        randomizerMock.Setup(r => r.GetNextRandom()).Returns(6);
        var randomizer = randomizerMock.Object;
        var giver = new ActionPointsGiver(randomizer, player);

        var action = giver.GiveActionPoints();
        
        Assert.Same(player, action.Player);
    }

    [Fact]
    public void GiveActionPoints_ShouldUseRandomizer_GivenInConstructor()
    {
        int minAmount = 1;
        int maxAmount = 6;
        var player = GetPlayer();
        int expectedAmount = 6;
        var randomizerMock = new Mock<IRandomNumberGenerator>();
        randomizerMock.Setup(r => r.GetNextRandom()).Returns(expectedAmount);
        var randomizer = randomizerMock.Object;
        var giver = new ActionPointsGiver(randomizer, player);

        var action = giver.GiveActionPoints();
        
        Assert.Equal(expectedAmount, action.Amount);
    }
}