using castledice_game_logic.GameObjects;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class TreeTests
{
    [Fact]
    public void Tree_ShouldImplementIRemovable()
    {
        var tree = new Tree(1, false);
        Assert.True(tree is IRemovable);
    }
    
    [Fact]
    public void Tree_ShouldImplementIPlaceBlocking()
    {
        var tree = new Tree(1, false);
        Assert.True(tree is IPlaceBlocking);
    }
    
    [Fact]
    public void CanBeRemoved_ShouldReturnValue_GivenInConstructor()
    {
        var tree = new Tree(1, false);
        Assert.False(tree.CanBeRemoved());
    }
    
    [Fact]
    public void GetRemoveCost_ShouldReturnValue_GivenInConstructor()
    {
        int expectedRemoveCost = 3;
        var tree = new Tree(expectedRemoveCost, false);

        int actualRemoveCost = tree.GetRemoveCost();
        Assert.Equal(expectedRemoveCost, actualRemoveCost);
    }

    [Fact]
    public void IsBlocking_ShouldAlwaysReturnTrue()
    {
        var tree = new Tree(1, false);
        Assert.True(tree.IsBlocking());
    }
}