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
        var castle = new CastleGO(GetPlayer(), 1, 1, 1, 1);

        Assert.True(castle is ICapturable);
    }

    [Fact]
    public void Castle_ShouldImplement_IPlayerOwned()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1, 1, 1);

        Assert.True(castle is IPlayerOwned);
    }

    [Fact]
    public void Castle_ShouldImplement_IPlaceBlocking()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1, 1, 1);

        Assert.True(castle is IPlaceBlocking);
    }

    [Fact]
    public void CaptureHit_ShouldInvokeHitEvent_WithHitCostAsArgument()
    {
        var rnd = new Random();
        var captureHitCost = rnd.Next(1, 6);
        var castle = new CastleGO(GetPlayer(), 1, 1, 1, captureHitCost);
        int actualHitCost = 0;
        castle.Hit += (sender, cost) => actualHitCost = cost;
        
        castle.CaptureHit(GetPlayer(6));
        
        Assert.Equal(captureHitCost, actualHitCost);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void GetCurrentMaxDurability_ShouldReturnMaxDurability_IfCastleIsOwned(int maxDurability)
    {
        var castle = new CastleGO(GetPlayer(), maxDurability, maxDurability, 1, 1);

        Assert.Equal(maxDurability, castle.GetCurrentMaxDurability());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void GetCurrentMaxDurability_ShouldReturnMaxFreeDurability_IfCastleIsFree(int freeDurability)
    {
        var castle = new CastleGO(new NullPlayer(), 1, 2, freeDurability, 1);

        Assert.Equal(freeDurability, castle.GetCurrentMaxDurability());
    }

    [Theory]
    [InlineData(1, 3, 3)]
    [InlineData(2, 3, 3)]
    [InlineData(3, 3, 3)]
    [InlineData(4, 5, 3)]
    public void GetDurability_ShouldReturnCastleDurability(int durability, int maxDurability, int freeDurability)
    {
        var castle = new CastleGO(GetPlayer(), durability, maxDurability, freeDurability, 1);

        Assert.Equal(durability, castle.GetDurability());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetMaxDurability_ShouldReturnMaxDurability_GivenInConstructor(int maxDurability)
    {
        var castle = new CastleGO(GetPlayer(), 1, maxDurability, 1, 1);
        
        Assert.Equal(maxDurability, castle.GetMaxDurability());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetMaxFreeDurability_ShouldReturnMaxFreeDurability_GivenInConstructor(int maxFreeDurability)
    {
        var castle = new CastleGO(new NullPlayer(), 1, 1, maxFreeDurability, 1);
        
        Assert.Equal(maxFreeDurability, castle.GetMaxFreeDurability());
    }

    [Fact]
    public void GetOwner_ShouldReturnPlayer_ThatWasGivenInConstructor()
    {
        var player = GetPlayer();
        var castle = new CastleGO(player, 1,1, 1, 1);

        var owner = castle.GetOwner();
        
        Assert.Same(player, owner);
    }

    [Fact]
    public void Free_ShouldRemoveCastleOwner()
    {
        var castle = new CastleGO(GetPlayer(), 3, 3,1, 1);
        
        castle.Free();
        
        Assert.True(castle.GetOwner().IsNull);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    //Free durability - is a durability of the castle that has no owner.
    public void Free_ShouldSetDurability_ToMaxFreeDurabilityValue(int freeDurability)
    {
        var castle = new CastleGO(GetPlayer(), 2, 2,freeDurability, 1);
        
        castle.Free();
        int actualDurability = castle.GetDurability();
        
        Assert.Equal(freeDurability, actualDurability);
    }

    [Fact]
    public void Free_ShouldInvokeStateModifiedEvent()
    {
        var eventInvoked = false;
        var castle = new CastleGO(GetPlayer(), 3, 3, 1, 1);
        castle.StateModified += (sender, args) => eventInvoked = true;
        
        castle.Free();
        
        Assert.True(eventInvoked);
    }

    [Fact]
    public void IsBlocking_ShouldReturnTrue()
    {
        var castle = new CastleGO(GetPlayer(), 1, 1, 1, 1);
        
        Assert.True(castle.IsBlocking());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetCaptureHitCostWithPlayer_ShouldReturnCaptureCost_GivenInConstructor(int captureHitCost)
    {
        var capturer = GetPlayer(actionPoints: 4);
        var castle = new CastleGO(GetPlayer(), 3, 3, 1, captureHitCost);

        var actualCaptureCost = castle.GetCaptureHitCost(capturer);
        
        Assert.Equal(captureHitCost, actualCaptureCost);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetCaptureHitCost_ShouldReturnCaptureCost_GivenInConstructor(int captureHitCost)
    {
        var castle = new CastleGO(GetPlayer(), 3, 3, 1, captureHitCost);

        var actualCaptureCost = castle.GetCaptureHitCost();
        
        Assert.Equal(captureHitCost, actualCaptureCost);
    }

    [Fact]
    public void CanBeCaptured_ShouldReturnFalse_IfCapturerIsOwner()
    {
        var player = GetPlayer();
        var castle = new CastleGO(player, 3, 3, 1, 1);
        
        Assert.False(castle.CanBeCaptured(player));
    }
    
    [Fact]
    public void CanBeCaptured_ShouldReturnFalse_IfCapturerHasNoActionPoints()
    {
        var capturer = GetPlayer(actionPoints: 0);
        var castle = new CastleGO(GetPlayer(), 3, 3, 1, 1);
        
        Assert.False(castle.CanBeCaptured(capturer));
    }

    [Fact]
    public void CanBeCaptured_ShouldReturnFalse_IfCapturerHasNotEnoughActionPoints()
    {
        var capturer = GetPlayer(actionPoints: 2);
        var castle = new CastleGO(GetPlayer(), 3, 3, 1, 3);
        
        Assert.False(castle.CanBeCaptured(capturer));
    }
    
    [Fact]
    public void CanBeCaptured_ShouldReturnTrue_IfCapturerHasEnoughActionPoints()
    {
        var capturer = GetPlayer(actionPoints: 3);
        var castle = new CastleGO(GetPlayer(), 3, 3, 1, 2);
        
        Assert.True(castle.CanBeCaptured(capturer));
    }

    [Fact]
    public void CaptureHit_ShouldNotChangeDurability_IfCapturerIsOwner()
    {
        int castleDurability = 3;
        var owner = GetPlayer(actionPoints: 5);
        var castle = new CastleGO(owner, castleDurability, castleDurability, 1, 1);
        
        castle.CaptureHit(owner);
        
        Assert.Equal(castleDurability, castle.GetDurability());
    }

    [Fact]
    public void CaptureHit_ShouldNotChangeCapturerActionPoints_IfCapturerIsOwner()
    {
        int capturerActionPoints = 5;
        var owner = GetPlayer(actionPoints: capturerActionPoints);
        var castle = new CastleGO(owner, 3, 3, 1, 1);
        
        castle.CaptureHit(owner);
        
        Assert.Equal(capturerActionPoints, owner.ActionPoints.Amount);
    }

    [Fact]
    public void CaptureHit_ShouldNotInvokeStateChangedEvent_IfCapturerIsOwner()
    {
        var eventInvoked = false;
        var owner = GetPlayer(actionPoints: 5);
        var castle = new CastleGO(owner, 3, 3, 1, 1);
        castle.StateModified += (sender, args) => eventInvoked = true;
        
        castle.CaptureHit(owner);
        
        Assert.False(eventInvoked);
    }

    [Fact]
    public void CaptureHit_ShouldNotInvokeStateChangedEvent_IfCapturerHasNotEnoughActionPoints()
    {
        var captureHitCost = 3;
        var capturerActionPoints = 1;
        var eventInvoked = false;
        var capturer = GetPlayer(actionPoints: capturerActionPoints);
        var castle = new CastleGO(GetPlayer(), 3, 3, 1, captureHitCost);
        castle.StateModified += (sender, args) => eventInvoked = true;
        
        castle.CaptureHit(capturer);
        
        Assert.False(eventInvoked);
    }
    
    [Fact]
    public void CaptureHit_ShouldInvokeStateChangedEvent_IfCapturerHasEnoughActionPoints()
    {
        var captureHitCost = 3;
        var capturerActionPoints = 5;
        var eventInvoked = false;
        var capturer = GetPlayer(actionPoints: capturerActionPoints);
        var castle = new CastleGO(GetPlayer(), 3, 3, 1, captureHitCost);
        castle.StateModified += (sender, args) => eventInvoked = true;
        
        castle.CaptureHit(capturer);
        
        Assert.True(eventInvoked);
    }

    [Theory]
    [InlineData(5, 3, 2, 1)]
    [InlineData(3, 4, 3, 1)]
    [InlineData(2, 5, 4, 1)]
    public void CaptureHit_ShouldReduceCapturerActionPoints_ByCaptureHitCost(int castleDurability, int capturerActionPoints, int captureHitCost, int expectedCapturerActionPoints)
    {
        var owner = GetPlayer();
        var capturer = GetPlayer(actionPoints: capturerActionPoints);
        var castle = new CastleGO(owner, castleDurability, castleDurability, 1, captureHitCost);
        
        castle.CaptureHit(capturer);
        
        Assert.Equal(expectedCapturerActionPoints, capturer.ActionPoints.Amount);
    }
    
    [Fact]
    public void CaptureHit_ShouldSetCapturerAsOwner_IfCastleDurabilityGoesBelowOrEqualZero()
    {
        var owner = GetPlayer();
        var capturer = GetPlayer(actionPoints: 4);
        var castle = new CastleGO(owner, 1, 1, 1, 2);
        
        castle.CaptureHit(capturer);
        
        Assert.Same(capturer, castle.GetOwner());
    }

    [Fact]
    public void CaptureHit_ShouldSetDurabilityToMax_IfCaptureIsSuccessful()
    {
        int expectedDurability = 5;
        int captureHitCost = 5;
        var owner = GetPlayer();
        var capturer = GetPlayer(actionPoints: 6);
        var castle = new CastleGO(owner, 1, expectedDurability, 1, captureHitCost);

        castle.CaptureHit(capturer);
        var actualDurability = castle.GetDurability();
        
        Assert.Equal(expectedDurability, actualDurability);
    }

    [Fact]
    public void CaptureHitsLeft_ShouldReturnCastleMaxDurability_IfItWasNotHitBefore()
    {
        var castleMaxDurability = new Random().Next(1, 10);
        var capturer = GetPlayer(actionPoints: 5);
        var castle = new CastleGO(GetPlayer(), castleMaxDurability, castleMaxDurability, castleMaxDurability, 1);
        
        var actualCaptureHitsLeft = castle.CaptureHitsLeft(capturer);
        
        Assert.Equal(castleMaxDurability, actualCaptureHitsLeft);
    }

    [Fact]
    public void CaptureHitsLeft_ShouldReturnActualDurability_IfCastleWasHitBefore()
    {
        var expectedCaptureHitsLeft = new Random().Next(2, 10);
        var castleMaxDurability = expectedCaptureHitsLeft + 1;
        var captureHitCost = 1;
        var capturer = GetPlayer(actionPoints: 5);
        var castle = new CastleGO(GetPlayer(), castleMaxDurability, castleMaxDurability, castleMaxDurability, captureHitCost);
        
        castle.CaptureHit(capturer);
        var actualCaptureHitsLeft = castle.CaptureHitsLeft(capturer);
        
        Assert.Equal(expectedCaptureHitsLeft, actualCaptureHitsLeft);
    }

    [Fact]
    public void CaptureHitsLeft_ShouldReturnMaxDurability_AfterCastleWasCaptured()
    {
        var castleMaxDurability = new Random().Next(2, 6);
        var capturer = GetPlayer(actionPoints: 6);
        var castle = new CastleGO(GetPlayer(), castleMaxDurability, castleMaxDurability, castleMaxDurability, 1);

        for (int i = 0; i < castleMaxDurability; i++)
        {
            castle.CaptureHit(capturer); 
        }
        var actualCaptureHitsLeft = castle.CaptureHitsLeft(capturer);
        
        Assert.Equal(castleMaxDurability, actualCaptureHitsLeft);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveDurabilityGiven(int durability)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), durability, 1, 1, 1));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveFreeDurabilityGiven(int freeDurability)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), 1, 1, freeDurability, 1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveCaptureHitCostGiven(int captureHitCost)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), 1, 1,1, captureHitCost));

    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveMaxDurabilityGiven(int maxDurability)
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), 1, maxDurability, 1, 1));
    }
    
    [Fact]
    public void Constructor_ShouldThrowArgumentException_IfGivenDurabilityIsBiggerThanMaxAndPlayerIsNotNull()
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(GetPlayer(), 4, 3, 1, 1));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_IfGivenDurabilityIsBiggerThanMaxFreeDurabilityAndPlayerIsNull()
    {
        Assert.Throws<ArgumentException>(() => new CastleGO(new NullPlayer(), 3, 3, 2, 1));
    }
}