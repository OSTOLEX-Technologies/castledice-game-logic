using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.FactoriesTests;

public class KnightsFactoryTests
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(2, 2)]
    public void GetKnight_ShouldReturnKnight_CreatedAccordingToConfig(int placementCost, int health)
    {
        var config = new KnightConfig() { PlacementCost = placementCost, Health = health };
        var factory = new KnightsFactory(config);
        
        var knight = factory.GetKnight(GetPlayer());
        
        Assert.Equal(config.Health, knight.GetReplaceCost());
        Assert.Equal(config.PlacementCost, knight.GetPlacementCost());
    }

    [Fact]
    public void GetKnight_ShouldReturnKnight_WithGivenOwner()
    {
        var owner = GetPlayer();
        var factory = new KnightsFactory(GetConfig());
        
        var knight = factory.GetKnight(owner);
        
        Assert.Same(owner, knight.GetOwner());
    }

    private static KnightConfig GetConfig(int placementCost = 1, int health = 2)
    {
        return new KnightConfig() { PlacementCost = placementCost, Health = health };
    }
}