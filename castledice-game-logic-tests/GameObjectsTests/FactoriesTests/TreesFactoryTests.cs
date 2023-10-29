using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories.Trees;

namespace castledice_game_logic_tests.GameObjectsTests.FactoriesTests;

public class TreesFactoryTests
{
    [Theory]
    [InlineData(3, false)]
    [InlineData(2, true)]
    public void GetTree_ShouldReturnTrees_CreatedAccordingToConfig(int removeCost, bool canBeRemoved)
    {
        var config = new TreeConfig(removeCost, canBeRemoved);
        var factory = new TreesFactory(config);
        var tree = factory.GetTree();

        Assert.Equal(removeCost, tree.GetRemoveCost());
        Assert.Equal(canBeRemoved, tree.CanBeRemoved());
    }

    [Fact]
    public void ConfigProperty_ShouldReturnConfig_GivenInConstructor()
    {
        var expectedConfig = new TreeConfig(1, true);
        var factory = new TreesFactory(expectedConfig);
        
        Assert.Same(expectedConfig, factory.Config);
    }

}