using castledice_game_logic.GameObjects;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class KnightTests
{
    [Fact]
    public void Knight_ShouldImplement_IReplaceable()
    {
        var knight = new Knight(GetPlayer(), 1, 1);
        
        Assert.True(knight is IReplaceable);
    }

    [Fact]
    public void Knight_ShouldImplement_IPlayerOwned()
    {
        var knight = new Knight(GetPlayer(), 1, 1);
        
        Assert.True(knight is IPlayerOwned);
    }
    
    [Fact]
    public void Knight_ShouldImplement_IPlaceBlocking()
    {
        var knight = new Knight(GetPlayer(), 1, 1);
        
        Assert.True(knight is IPlaceBlocking);
    }

    [Fact]
    public void Knight_ShouldImplement_IPlaceable()
    {
        var knight = new Knight(GetPlayer(), 1, 1);
        
        Assert.True(knight is IPlaceable);
    }

    [Fact]
    public void GetOwner_ShouldReturnPlayer_GivenInConstructor()
    {
        var player = GetPlayer();
        var knight = new Knight(player, 1, 1);
        
        Assert.Same(player, knight.GetOwner());
    }

    [Fact]
    public void GetPlacementCost_ShouldReturnCost_GivenInConstructor()
    {
        var placementCost = 5;
        var knight = new Knight(GetPlayer(), placementCost, 1);
        
        Assert.Equal(placementCost, knight.GetPlacementCost());
    }

    [Fact]
    public void GetReplaceCost_ShouldReturnKnightHealth()
    {
        var health = 3;
        var knight = new Knight(GetPlayer(), 1, health);
        
        Assert.Equal(health, knight.GetReplaceCost());
    }

    [Fact]
    public void CanBePlacedOnCell_ShouldReturnTrue()
    {
        var knight = new Knight(GetPlayer(), 1, 1);
        var cell = GetCell();
        
        Assert.True(knight.CanBePlacedOn(cell));
    }
    
    [Fact]
    public void GetPlacementType_ShouldReturnKnightPlacementType()
    {
        var knight = new Knight(GetPlayer(), 1, 1);
        
        Assert.Equal(PlacementType.Knight, knight.PlacementType);
    }

    [Fact]
    public void IsBlocking_ShouldReturnTrue()
    {
        var knight = new Knight(GetPlayer(), 1, 1);
        
        Assert.True(knight.IsBlocking());
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositivePlacementCostIsGiven(int cost)
    {
        Assert.Throws<ArgumentException>(() => new Knight(GetPlayer(), cost, 1));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveHealthIsGiven(int health)
    {
        Assert.Throws<ArgumentException>(() => new Knight(GetPlayer(), 1, health));
    }
}