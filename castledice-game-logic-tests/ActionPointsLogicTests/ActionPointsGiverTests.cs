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
        int minAmount = 0;
        int maxAmount = 6;
        var player = GetPlayer();
        var randomizerMock = new Mock<IRandomNumberGenerator>();
        randomizerMock.Setup(r => r.GetRandom(minAmount, maxAmount)).Returns(6);
        var randomizer = randomizerMock.Object;
        var giver = new ActionPointsGiver(randomizer, player, minAmount, maxAmount);

        var action = giver.GiveActionPoints();
        
        Assert.Same(player, action.Player);
    }

    [Fact]
    public void GiveActionPoints_ShouldUseRandomizer_GivenInConstructor()
    {
        int minAmount = 0;
        int maxAmount = 6;
        var player = GetPlayer();
        int expectedAmount = 6;
        var randomizerMock = new Mock<IRandomNumberGenerator>();
        randomizerMock.Setup(r => r.GetRandom(minAmount, maxAmount)).Returns(expectedAmount);
        var randomizer = randomizerMock.Object;
        var giver = new ActionPointsGiver(randomizer, player, minAmount, maxAmount);

        var action = giver.GiveActionPoints();
        
        Assert.Equal(expectedAmount, action.Amount);
    }
}