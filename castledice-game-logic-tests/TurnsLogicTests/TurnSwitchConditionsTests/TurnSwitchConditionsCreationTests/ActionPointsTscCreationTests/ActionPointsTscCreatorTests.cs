using System.Reflection;
using castledice_game_logic.TurnsLogic;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation.ActionPointsTscCreation;
using Moq;

namespace castledice_game_logic_tests.TurnsLogicTests.TurnSwitchConditionsTests.TurnSwitchConditionsCreationTests.ActionPointsTscCreationTests;

public class ActionPointsTscCreatorTests
{
    [Fact]
    public void Create_ShouldReturnActionPointsTsc_WithCurrentPlayerProviderFromConstructor()
    {
        var currentPlayerProvider = new Mock<ICurrentPlayerProvider>().Object;
        var creator = new ActionPointsTscCreator(currentPlayerProvider);
        
        var actualTsc = creator.CreateActionPointsTsc();
        var fieldInfo = actualTsc.GetType().GetField("_currentPlayerProvider", BindingFlags.NonPublic | BindingFlags.Instance);
        var actualCurrentPlayerProvider = fieldInfo.GetValue(actualTsc);
        
        Assert.Same(currentPlayerProvider, actualCurrentPlayerProvider);
    }
}