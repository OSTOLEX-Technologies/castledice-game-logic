using static castledice_game_logic_tests.ObjectCreationUtility;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation.ActionPointsTscCreation;
using Moq;

namespace castledice_game_logic_tests.TurnsLogicTests.TurnSwitchConditionsTests;

public class TscFactoryTests
{
    [Theory]
    [MemberData(nameof(GetTscTypes))]
    public void GetTsc_ShouldReturnTsc_CorrespondingToTscType(TscType type)
    {
        var expectedActionPointsTsc = GetActionPointsTsc();
        var actionPointsTscCreatorMock = new Mock<IActionPointsTscCreator>();
        actionPointsTscCreatorMock.Setup(creator => creator.CreateActionPointsTsc()).Returns(expectedActionPointsTsc);
        var actionPointsTscCreator = actionPointsTscCreatorMock.Object;
        var tscFactory = new TscFactory(actionPointsTscCreator);
        
        var actualTsc = tscFactory.GetTsc(type);

        switch (type)
        {
            case TscType.SwitchByActionPoints:
                Assert.Same(expectedActionPointsTsc, actualTsc);
                break;
            default:
                Assert.Fail("No handling code for this TscType: " + type);
                break;
        }
    }
    
    public static IEnumerable<object[]> GetTscTypes()
    {
        var values = Enum.GetValues(typeof(TscType));
        foreach (var type in values)
        {
            yield return new []{type};
        }
    }
}