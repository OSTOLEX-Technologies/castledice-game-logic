using castledice_game_logic.TurnsLogic;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using Moq;

namespace castledice_game_logic_tests.TurnsLogicTests;
using static ObjectCreationUtility;

public class ActionPointsTscTests
{
    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfCurrentPlayerHasActionPoints()
    {
        var player = GetPlayer(actionPoints: 1);
        var currentPlayerProviderMock = new Mock<ICurrentPlayerProvider>();
        currentPlayerProviderMock.Setup(provider => provider.GetCurrentPlayer()).Returns(player);
        var currentPlayerProvider = currentPlayerProviderMock.Object;
        var switchCondition = new ActionPointsTsc(currentPlayerProvider);
        
        Assert.False(switchCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnTrue_IfCurrentPlayerHasNoActionPoints()
    {
        var player = GetPlayer(actionPoints: 0);
        var currentPlayerProviderMock = new Mock<ICurrentPlayerProvider>();
        currentPlayerProviderMock.Setup(provider => provider.GetCurrentPlayer()).Returns(player);
        var currentPlayerProvider = currentPlayerProviderMock.Object;
        var switchCondition = new ActionPointsTsc(currentPlayerProvider);
        
        Assert.True(switchCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void Accept_ShouldCallVisitActionPointsCondition_OnGivenVisitor()
    {
        var visitorMock = new Mock<ITurnSwitchConditionVisitor<bool>>();
        var currentPlayerProviderMock = new Mock<ICurrentPlayerProvider>();
        var switchCondition = new ActionPointsTsc(currentPlayerProviderMock.Object);
        
        switchCondition.Accept(visitorMock.Object);
        
        visitorMock.Verify(visitor => visitor.VisitActionPointsCondition(switchCondition));
    }
}