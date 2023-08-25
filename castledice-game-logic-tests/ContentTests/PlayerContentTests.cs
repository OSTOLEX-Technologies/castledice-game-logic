using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests;

public class PlayerContentTests
{
    [Fact]
    public void PlayerProperty_ShouldReturnPlayer_ThatWasGivenInConstructor()
    {
        var player = GetPlayer();
        var content = new PlayerContent(player);
        Assert.Same(content.Player, player);
    }

    private Player GetPlayer()
    {
        return new Player();
    }
}