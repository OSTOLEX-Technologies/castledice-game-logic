using castledice_game_logic.TurnsLogic;
using Moq;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class ActionPointsConditionTests
{
    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfCurrentPlayerHasActionPoints()
    {
        var player = GetPlayer(actionPoints: 1);
        var currentPlayerProviderMock = new Mock<ICurrentPlayerProvider>();
        currentPlayerProviderMock.Setup(provider => provider.GetCurrentPlayer()).Returns(player);
        var currentPlayerProvider = currentPlayerProviderMock.Object;
        var switchCondition = new ActionPointsCondition(currentPlayerProvider);
        
        Assert.False(switchCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnTrue_IfCurrentPlayerHasNoActionPoints()
    {
        var player = GetPlayer(actionPoints: 0);
        var currentPlayerProviderMock = new Mock<ICurrentPlayerProvider>();
        currentPlayerProviderMock.Setup(provider => provider.GetCurrentPlayer()).Returns(player);
        var currentPlayerProvider = currentPlayerProviderMock.Object;
        var switchCondition = new ActionPointsCondition(currentPlayerProvider);
        
        Assert.True(switchCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void Accept_ShouldCallVisitActionPointsCondition_OnGivenVisitor()
    {
        var visitorMock = new Mock<ITurnSwitchConditionVisitor<bool>>();
        var currentPlayerProviderMock = new Mock<ICurrentPlayerProvider>();
        var switchCondition = new ActionPointsCondition(currentPlayerProviderMock.Object);
        
        switchCondition.Accept(visitorMock.Object);
        
        visitorMock.Verify(visitor => visitor.VisitActionPointsCondition(switchCondition));
    }
}