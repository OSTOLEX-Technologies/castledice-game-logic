using System.Runtime.InteropServices;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using static castledice_game_logic_tests.ObjectCreationUtility;
namespace castledice_game_logic_tests;

public class CastleTests
{
    [Fact]
    public void Castle_ShouldImplement_ICapturable()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1, 1);
        
        Assert.True(castle is ICapturable);
    }
    
    [Fact]
    public void Castle_ShouldImplement_IPlayerOwned()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1, 1);
        
        Assert.True(castle is IPlayerOwned);
    }
    
    [Fact]
    public void Castle_ShouldImplement_IPlaceBlocking()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1, 1);
        
        Assert.True(castle is IPlaceBlocking);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void GetMaxDurability_ShouldReturnDefaultDurability_IfCastleIsOwned(int defaultDurability)
    {
        var castle = new CastleGO(GetPlayer(), defaultDurability, 1, 1);
        
        Assert.Equal(defaultDurability, castle.GetMaxDurability());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void GetMaxDurability_ShouldReturnFreeDurability_IfCastleIsFree(int freeDurability)
    {
        var castle = new CastleGO(new NullPlayer(), 2, freeDurability, 1);
        
        Assert.Equal(freeDurability, castle.GetMaxDurability());
    }
    
    [Fact]
    public void GetDurability_ShouldReturnCastleDurability()
    {
        int durability = 3;
        var castle = new CastleGO(GetPlayer(), durability, durability, 1);
        
        Assert.Equal(durability, castle.GetDurability());
    }

    [Fact]
    public void GetOwner_ShouldReturnPlayer_ThatWasGivenInConstructor()
    {
        var player = GetPlayer();
        var castle = new CastleGO(player, 1, 1, 1);

        var owner = castle.GetOwner();
        
        Assert.Same(player, owner);
    }

    [Fact]
    public void Free_ShouldRemoveCastleOwner()
    {
        var castle = new CastleGO(GetPlayer(), 3, 1, 1);
        
        castle.Free();
        
        Assert.True(castle.GetOwner().IsNull);
    }

    [Fact]
    //Free durability - is a durability of the castle that has no owner.
    public void Free_ShouldSetDurability_ToFreeDurabilityValue()
    {
        int freeDurability = 3;
        var castle = new CastleGO(GetPlayer(), 2, freeDurability, 1);
        
        castle.Free();
        int actualDurability = castle.GetDurability();
        
        Assert.Equal(freeDurability, actualDurability);
    }

    [Fact]
    public void IsBlocking_ShouldReturnTrue()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1, 1);
        
        Assert.True(castle.IsBlocking());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetCaptureCost_ShouldReturnCaptureCost_GivenInConstructor(int captureCost)
    {
        var capturer = GetPlayer(actionPoints: 4);
        var castle = new CastleGO(GetPlayer(), 3, 1, captureCost);

        var actualCaptureCost = castle.GetCaptureCost(capturer);
        
        Assert.Equal(captureCost, actualCaptureCost);
    }

    [Fact]
    public void GetCaptureCost_ShouldReturnCastleDurability_IfItIsLessThanCaptureCost()
    {
        var durability = 2;
        var captureCost = 3;
        var castle = new CastleGO(GetPlayer(), durability, 1, captureCost);
        
        var actualCaptureCost = castle.GetCaptureCost(GetPlayer());
        
        Assert.Equal(durability, actualCaptureCost);
    }

    [Fact]
    public void CanBeCaptured_ShouldReturnFalse_IfCapturerIsOwner()
    {
        var player = GetPlayer();
        var castle = new CastleGO(player, 3, 1, 1);
        
        Assert.False(castle.CanBeCaptured(player));
    }
    
    [Fact]
    public void CanBeCaptured_ShouldReturnFalse_IfCapturerHasNoActionPoints()
    {
        var capturer = GetPlayer(actionPoints: 0);
        var castle = new CastleGO(GetPlayer(), 3, 1, 1);
        
        Assert.False(castle.CanBeCaptured(capturer));
    }

    [Fact]
    public void CanBeCaptured_ShouldReturnFalse_IfCapturerHasNotEnoughActionPoints()
    {
        var capturer = GetPlayer(actionPoints: 2);
        var castle = new CastleGO(GetPlayer(), 3, 1, 3);
        
        Assert.False(castle.CanBeCaptured(capturer));
    }
    
    [Fact]
    public void CanBeCaptured_ShouldReturnTrue_IfCapturerHasEnoughActionPoints()
    {
        var capturer = GetPlayer(actionPoints: 3);
        var castle = new CastleGO(GetPlayer(), 3, 1, 2);
        
        Assert.True(castle.CanBeCaptured(capturer));
    }

    [Fact]
    // In this test the amount of durability of castle is checked by passing player
    // with amount of action points bigger than castle durability into GetCaptureCost method.
    public void Capture_ShouldDoNothing_IfCapturerIsOwner()
    {
        int capturerActionPoints = 5;
        int castleDurability = 3;
        var owner = GetPlayer(actionPoints: capturerActionPoints);
        var castle = new CastleGO(owner, castleDurability, 1, 1);
        
        castle.Capture(owner);
        
        Assert.Equal(capturerActionPoints, owner.ActionPoints.Amount);
        Assert.Equal(castleDurability, castle.GetDurability());
    }

    [Theory]
    [InlineData(5, 3, 2, 1)]
    [InlineData(3, 4, 3, 1)]
    [InlineData(2, 5, 4, 3)]
    public void Capture_ShouldReduceCapturerActionPoints_ByCaptureCost(int castleDurability, int capturerActionPoints, int captureCost, int expectedCapturerActionPoints)
    {
        var owner = GetPlayer();
        var capturer = GetPlayer(actionPoints: capturerActionPoints);
        var castle = new CastleGO(owner, castleDurability, 1, captureCost);
        
        castle.Capture(capturer);
        
        Assert.Equal(expectedCapturerActionPoints, capturer.ActionPoints.Amount);
    }
    
    [Fact]
    public void Capture_ShouldSetCapturerAsOwner_IfCastleDurabilityGoesBelowZero()
    {
        var owner = GetPlayer();
        var capturer = GetPlayer(actionPoints: 4);
        var castle = new CastleGO(owner, 1, 1, 2);
        
        castle.Capture(capturer);
        
        Assert.Same(capturer, castle.GetOwner());
    }

    [Fact]
    //In this test durability of castle is checked by passing player with 6 action points to GetCaptureCost method.
    public void Capture_ShouldSetDurabilityToDefault_IfCaptureIsSuccessful()
    {
        int expectedDurability = 5;
        int captureCost = 5;
        var owner = GetPlayer();
        var firstCapturer = GetPlayer(actionPoints: 6);
        var castle = new CastleGO(owner, expectedDurability, 1, captureCost);

        castle.Capture(firstCapturer);
        var actualDurability = castle.GetDurability();
        
        Assert.Equal(expectedDurability, actualDurability);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveDurabilityGiven(int durability)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), durability, 1, 1));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveFreeDurabilityGiven(int freeDurability)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), 1, freeDurability, 1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveCaptureCostGiven(int captureCost)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), 1, 1, captureCost));

    }
}