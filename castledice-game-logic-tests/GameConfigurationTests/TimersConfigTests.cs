using castledice_game_logic.GameConfiguration;

namespace castledice_game_logic_tests.GameConfigurationTests;

public class TimersConfigTests
{
    [Fact]
    public void GetTimeSpanForPlayer_ShouldReturnTimeSpan_GivenInConstructor()
    {
        var rnd = new Random();
        var playerId = rnd.Next();
        var expectedTimeSpan = TimeSpan.FromSeconds(rnd.Next());
        var playerIdToTimeSpan = new Dictionary<int, TimeSpan> {{playerId, expectedTimeSpan}};
        var timersConfig = new TimersConfig(playerIdToTimeSpan);
        
        var actualTimeSpan = timersConfig.GetTimeSpanForPlayer(playerId);
        
        Assert.Equal(expectedTimeSpan, actualTimeSpan);
    }
}