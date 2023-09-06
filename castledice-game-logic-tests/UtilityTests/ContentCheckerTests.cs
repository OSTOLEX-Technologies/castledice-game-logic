using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Utilities;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class ContentCheckerTests
{
    [Fact]
    public void ContentBelongsToPlayer_ShouldReturnTrue_IfContentOwnedByPlayer()
    {
        var player = GetPlayer();
        var content = new PlayerUnitMock() { Owner = player };
        
        Assert.True(ContentChecker.ContentBelongsToPlayer(content, player));
    }

    [Fact]
    public void ContentBelongsToPlayer_ShouldReturnFalse_IfContentIsNotIPlayerOwned()
    {
        var player = GetPlayer();
        var content = GetObstacle();
        
        Assert.False(ContentChecker.ContentBelongsToPlayer(content, player));
    }
    
    [Fact]
    public void ContentBelongsToPlayer_ShouldReturnFasle_IfContentNotOwnedByPlayer()
    {
        var player = GetPlayer();
        var content = new PlayerUnitMock() { Owner = GetPlayer() };
        
        Assert.False(ContentChecker.ContentBelongsToPlayer(content, player));
    }
}