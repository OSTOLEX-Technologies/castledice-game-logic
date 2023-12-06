using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation;
using Moq;

namespace castledice_game_logic_tests.TurnsLogicTests.TurnSwitchConditionsTests.TurnSwitchConditionsCreationTests;

public class FactoryTscListCreatorTests
{
    [Theory]
    [MemberData(nameof(TscTypes))]
    public void GetTscList_ShouldReturnList_WithAppropriateTscObjects(List<TscType> types)
    {
        var tscFactoryMock = new Mock<ITscFactory>();
        var expectedTscList = new List<ITsc>();
        foreach (var type in types)
        {
            var tsc = new Mock<ITsc>();
            tscFactoryMock.Setup(factory => factory.GetTsc(type)).Returns(tsc.Object);
            expectedTscList.Add(tsc.Object);
        }
        var tscFactory = tscFactoryMock.Object;
        var tscListCreator = new FactoryTscListCreator(tscFactory);
        
        var actualTscList = tscListCreator.GetTscList(types);

        foreach (var tsc in expectedTscList)
        {
            Assert.Contains(tsc, actualTscList);
        }
    }

    public static IEnumerable<object[]> TscTypes()
    {
        var values = Enum.GetValues(typeof(TscType));
        var types = new List<TscType>();
        foreach (var value in values)
        {
            types.Add((TscType) value);
        }
        yield return new object[] {types};
    }
}