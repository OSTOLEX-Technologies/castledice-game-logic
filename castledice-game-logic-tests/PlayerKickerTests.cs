using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class PlayerKickerTests
{
    [Fact]
    public void KickFromBoard_ShouldFreeCastles_OwnedByPlayer()
    {
        var player = GetPlayer();
        var board = GetFullNByNBoard(4);
        var firstCastle = GetCastle(player);
        var secondCastle = GetCastle(player);
        board[2, 3].AddContent(firstCastle);
        board[0, 0].AddContent(secondCastle);
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(player);
        
        Assert.True(firstCastle.GetOwner().IsNull);
        Assert.True(secondCastle.GetOwner().IsNull);
    }

    [Fact]
    public void KickFromBoard_ShouldNotFreeCastles_OfOtherPlayers()
    {
        var player = GetPlayer();
        var board = GetFullNByNBoard(4);
        var firstCastle = GetCastle(player);
        var secondCastle = GetCastle(GetPlayer());
        board[2, 3].AddContent(firstCastle);
        board[0, 0].AddContent(secondCastle);
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(player);
        
        Assert.True(firstCastle.GetOwner().IsNull);
        Assert.False(secondCastle.GetOwner().IsNull);
    }

    [Fact]
    public void KickFromBoard_ShouldRemoveUnits_OwnedByPlayer()
    {
        var player = GetPlayer();
        var board = GetFullNByNBoard(3);
        board[0, 0].AddContent(GetUnit(player));
        board[1, 2].AddContent(GetUnit(player));
        board[2, 2].AddContent(GetUnit(player));
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(player);
        
        Assert.False(board[0, 0].HasContent());
        Assert.False(board[1, 2].HasContent());
        Assert.False(board[2, 2].HasContent());
    }
    
    [Fact]
    public void KickFromBoard_ShouldNotRemoveUnits_OfOtherPlayers()
    {
        var player = GetPlayer();
        var board = GetFullNByNBoard(3);
        board[0, 0].AddContent(GetUnit(player));
        board[1, 2].AddContent(GetUnit(GetPlayer()));
        board[2, 2].AddContent(GetUnit(GetPlayer()));
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(player);
        
        Assert.False(board[0, 0].HasContent());
        Assert.True(board[1, 2].HasContent());
        Assert.True(board[2, 2].HasContent());
    }

    [Fact]
    public void KickFromBoard_ShouldFreeCapturables_OwnedByPlayer()
    {
        var player = GetPlayer();
        var capturable = new CapturableMock() { Owner = player };
        var board = GetFullNByNBoard(2);
        board[0, 0].AddContent(capturable);
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(player);
        
        Assert.True(capturable.GetOwner().IsNull);
    }
    
    [Fact]
    public void KickFromBoard_ShouldNotFreeCapturables_OfOtherPlayers()
    {
        var capturable = new CapturableMock() { Owner = GetPlayer() };
        var board = GetFullNByNBoard(2);
        board[0, 0].AddContent(capturable);
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(GetPlayer());
        
        Assert.False(capturable.GetOwner().IsNull);
    }
}