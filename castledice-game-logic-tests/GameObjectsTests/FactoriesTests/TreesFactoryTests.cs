using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;

namespace castledice_game_logic_tests.FactoriesTests;

public class TreesFactoryTests
{
    [Theory]
    [InlineData(3, false)]
    [InlineData(2, true)]
    public void GetTree_ShouldReturnTrees_CreatedAccordingToConfig(int removeCost, bool canBeRemoved)
    {
        var config = new TreeConfig() { RemoveCost = removeCost, CanBeRemoved = canBeRemoved };
        var factory = new TreesFactory(config);
        var tree = factory.GetTree();
        
        Assert.Equal(removeCost, tree.GetRemoveCost());
        Assert.Equal(canBeRemoved, tree.CanBeRemoved());
    }
}