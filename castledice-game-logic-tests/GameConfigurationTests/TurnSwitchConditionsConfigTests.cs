using castledice_game_logic.GameConfiguration;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;

namespace castledice_game_logic_tests.GameConfigurationTests;

public class TurnSwitchConditionsConfigTests
{
    [Fact]
    public void Properties_ShouldReturnValues_GivenInConstructor()
    {
        var conditionsToUse = new List<TscType> { TscType.SwitchByActionPoints };
        var turnSwitchConditionsConfig = new TurnSwitchConditionsConfig(conditionsToUse);
        
        Assert.Equal(conditionsToUse, turnSwitchConditionsConfig.ConditionsToUse);
    }
}