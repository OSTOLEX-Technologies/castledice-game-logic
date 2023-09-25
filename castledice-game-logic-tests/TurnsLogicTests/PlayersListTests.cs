﻿using castledice_game_logic;
using castledice_game_logic.TurnsLogic;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class PlayersListTests
{
    [Fact]
    public void PlayersList_ShouldImplementIEnumerable_ForPlayers()
    {
        var playersList = new PlayersList();
        Assert.True(playersList is IEnumerable<Player>);
    }
    
    [Fact]
    public void AddPlayer_ShouldAddPlayers()
    {
        var playersList = new PlayersList();
        var player = GetPlayer();
        
        playersList.AddPlayer(player);
        
        Assert.Contains(player, playersList);
    }

    [Fact]
    public void AddPlayer_ShouldNotAddPlayer_IfPlayerAlreadyAdded()
    {
        var player = GetPlayer();
        var playersList = new PlayersList();
        playersList.AddPlayer(player);
        
        playersList.AddPlayer(player);
        
        Assert.Single(playersList);
    }

    [Fact]
    public void AddPlayer_ShouldThrowArgumentException_IfAddingPlayerWithExistingId()
    {
        var playersList = new PlayersList(new List<Player>(){GetPlayer(id: 3)});
        var player = GetPlayer(id: 3);

        Assert.Throws<ArgumentException>(() => playersList.AddPlayer(player));
    }

    [Fact]
    public void KickPlayer_ShouldRemovePlayer()
    {
        var player = GetPlayer();
        var playersList = new PlayersList();
        playersList.AddPlayer(player);
        
        playersList.KickPlayer(player);
        
        Assert.DoesNotContain(player, playersList);
    }
    
    [Fact]
    public void KickPlayer_ShouldInvokePlayerKickedEvent_WithKickedPlayer()
    {
        var player = GetPlayer();
        var playersList = new PlayersList();
        playersList.AddPlayer(player);
        Player kickedPlayer = new NullPlayer();
        playersList.PlayerKicked += (sender, playerFromEvent) => kickedPlayer = playerFromEvent;
        
        playersList.KickPlayer(player);
        
        Assert.Same(player, kickedPlayer);
    }
    
    [Theory]
    [InlineData(100)]
    [InlineData(-1)]
    public void Indexer_ShouldThrowArgumentException_IfInvalidIndexGiven(int index)
    {
        var playersList = new PlayersList();
        
        Assert.Throws<ArgumentException>(() => playersList[index]);
    }
    
    [Fact]
    public void Indexer_ShouldReturnPlayers_AccordingToAddPlayerOrder()
    {
        var playersList = new PlayersList();
        var firstPlayer = GetPlayer(id: 1);
        var secondPlayer = GetPlayer(id: 2);
        playersList.AddPlayer(firstPlayer);
        playersList.AddPlayer(secondPlayer);
        
        Assert.Same(firstPlayer, playersList[0]);
        Assert.Same(secondPlayer, playersList[1]);
    }

    [Fact]
    public void Constructor_ShouldNotAddDuplicates_IfEnumerableWithDuplicatesGiven()
    {
        var firstPlayer = GetPlayer(id: 1);
        var secondPlayer = GetPlayer(id: 2);
        var enumerable = new List<Player>() { firstPlayer, firstPlayer, firstPlayer, secondPlayer};
        
        var playersList = new PlayersList(enumerable);
        
        Assert.Same(firstPlayer, playersList[0]);
        Assert.Same(secondPlayer, playersList[1]);
    }

    [Fact]
    public void Count_ShouldReturnPlayersCount()
    {
        var playersList = new PlayersList();
        playersList.AddPlayer(GetPlayer(id: 1));
        playersList.AddPlayer(GetPlayer(id: 2));
        
        Assert.Equal(2, playersList.Count);
    }
    
    
}