using castledice_game_logic.GameObjects;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using static castledice_game_logic_tests.ObjectCreationUtility;
namespace castledice_game_logic_tests;

public class CastleTests
{
    [Fact]
    public void Castle_ShouldImplement_ICapturable()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1);
        
        Assert.True(castle is ICapturable);
    }
    
    [Fact]
    public void Castle_ShouldImplement_IPlayerOwned()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1);
        
        Assert.True(castle is IPlayerOwned);
    }
    
    [Fact]
    public void Castle_ShouldImplement_IPlaceBlocking()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1);
        
        Assert.True(castle is IPlaceBlocking);
    }

    [Fact]
    public void GetOwner_ShouldReturnPlayer_ThatWasGivenInConstructor()
    {
        var player = GetPlayer();
        var castle = new CastleGO(player, 1, 1);

        var owner = castle.GetOwner();
        
        Assert.Same(player, owner);
    }

    [Fact]
    public void Free_ShouldRemoveCastleOwner()
    {
        var castle = new CastleGO(GetPlayer(), 3, 1);
        
        castle.Free();
        
        Assert.True(castle.GetOwner().IsNull);
    }

    [Fact]
    //Free durability - is a durability of the castle that has no owner.
    public void Free_ShouldSetDurability_ToFreeDurabilityValue()
    {
        int freeDurability = 3;
        var castle = new CastleGO(GetPlayer(), 2, freeDurability);
        
        castle.Free();
        int actualDurability = castle.GetCaptureCost(GetPlayer(actionPoints: freeDurability + 1));
        
        Assert.Equal(freeDurability, actualDurability);
    }

    [Fact]
    public void IsBlocking_ShouldReturnTrue()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1);
        
        Assert.True(castle.IsBlocking());
    }

    [Fact]
    public void GetCaptureCost_ShouldReturnCapturerActionPointsAmount_IfItIsLessOreEqualThanCastleDurability()
    {
        var capturer = GetPlayer(actionPoints: 4);
        var captureCost = 6;
        var expectedCaptureCost = 4;
        var castle = new CastleGO(GetPlayer(), captureCost, 1);

        var actualCaptureCost = castle.GetCaptureCost(capturer);
        
        Assert.Equal(expectedCaptureCost, actualCaptureCost);
    }

    [Fact]
    public void GetCaptureCost_ShouldReturnDurabilityValue_IfCapturerActionPointsAreBiggerThanIt()
    {
        var capturer = GetPlayer(actionPoints: 6);
        var captureCost = 3;
        var expectedCaptureCost = 3;
        var castle = new CastleGO(GetPlayer(), captureCost, 1);

        var actualCaptureCost = castle.GetCaptureCost(capturer);
        
        Assert.Equal(expectedCaptureCost, actualCaptureCost);
    }

    [Fact]
    public void CanBeCaptured_ShouldReturnFalse_IfCaptuererIsOwner()
    {
        var player = GetPlayer();
        var castle = new CastleGO(player, 3, 1);
        
        Assert.False(castle.CanBeCaptured(player));
    }
    
    [Fact]
    public void CanBeCaptured_ShouldReturnFalse_IfCapturerHasNoActionPoints()
    {
        var capturer = GetPlayer(actionPoints: 0);
        var castle = new CastleGO(GetPlayer(), 3, 1);
        
        Assert.False(castle.CanBeCaptured(capturer));
    }
    
    [Fact]
    public void CanBeCaptured_ShouldReturnTrue_IfCapturerHasActionPoints()
    {
        var capturer = GetPlayer(actionPoints: 1);
        var castle = new CastleGO(GetPlayer(), 3, 1);
        
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
        var capturer = GetPlayer(actionPoints: capturerActionPoints); //Player, with same amount of action points as capturer.
        var castle = new CastleGO(owner, castleDurability, 1);
        
        castle.Capture(owner);
        
        Assert.Equal(capturerActionPoints, owner.ActionPoints.Amount);
        Assert.Equal(castleDurability, castle.GetCaptureCost(capturer));
    }

    [Theory]
    [InlineData(5, 3, 0)]
    [InlineData(3, 4, 1)]
    [InlineData(2, 5, 3)]
    public void Capture_ShouldReduceCapturerActionPoints(int castleDurability, int capturerActionPoints, int expectedCapturerActionPoints)
    {
        var owner = GetPlayer();
        var capturer = GetPlayer(actionPoints: capturerActionPoints);
        var castle = new CastleGO(owner, castleDurability, 1);
        
        castle.Capture(capturer);
        
        Assert.Equal(expectedCapturerActionPoints, capturer.ActionPoints.Amount);
    }
    
    [Fact]
    // If amount of capturer`s action points is more ore equal to castle`s durability, 
    // then capturer will become owner of castle after calling Capture method
    public void Capture_ShouldSetCapturerAsOwner_IfCapturerHadEnoughActionPoints()
    {
        var owner = GetPlayer();
        var capturer = GetPlayer(actionPoints: 4);
        var castle = new CastleGO(owner, 3, 1);
        
        castle.Capture(capturer);
        
        Assert.Same(capturer, castle.GetOwner());
    }

    [Fact]
    //In this test durability of castle is checked by passing player with 6 action points to GetCaptureCost method.
    public void Capture_ShouldSetDurabilityToDefault_IfCaptureIsSuccessful()
    {
        int expectedCaptureCost = 5;
        var owner = GetPlayer();
        var firstCapturer = GetPlayer(actionPoints: 6);
        var secondCapturer = GetPlayer(actionPoints: 6);
        var castle = new CastleGO(owner, expectedCaptureCost, 1);
        
        castle.Capture(firstCapturer);
        var actualCaptureCost = castle.GetCaptureCost(secondCapturer);
        
        Assert.Equal(expectedCaptureCost, actualCaptureCost);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveDurabilityGiven(int durability)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), durability, 1));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveFreeDurabilityGiven(int freeDurability)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), 1, freeDurability));
    }
}