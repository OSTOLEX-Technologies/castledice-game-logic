﻿using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class GiveActionPointsActionTests
{
    [Fact]
    public void PlayerProperty_ShouldReturnPlayer_GivenInConstructor()
    {
        var player = GetPlayer();
        int amount = 5;
        var action = new GiveActionPointsAction(player, amount);
        
        Assert.Same(player, action.Player);
    }

    [Fact]
    public void AmountProperty_ShouldReturnNumber_GivenInConstructor()
    {
        var player = GetPlayer();
        int amount = 5;
        var action = new GiveActionPointsAction(player, amount);
        
        Assert.Equal(amount, action.Amount); 
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_IfNegativeAmountGiven()
    {
        var player = GetPlayer();
        int amount = -1;
        
        Assert.Throws<ArgumentException>(() => new GiveActionPointsAction(player, amount));
    }
    
    [Fact]
    public void GetSnapshot_ShouldReturnGiveActionPointsActionSnapshot()
    {
        var player = GetPlayer();
        var ap = 6;
        var action = new GiveActionPointsAction(player, ap);

        var snapshot = action.GetSnapshot();
        
        Assert.True(snapshot is GiveActionPointsSnapshot);
    }

    [Fact]
    public void GetSnapshot_ShouldReturnSnapshot_WithCorrespondingFields()
    {
        int playerActionPoints = 4;
        int id = 3;
        int actionPointsToGive = 2;
        var player = GetPlayer(playerActionPoints, id);
        var action = new GiveActionPointsAction(player, actionPointsToGive);

        var snapshot = action.GetSnapshot();
        var giveAPSnapshot = snapshot as GiveActionPointsSnapshot;
        Assert.Equal(actionPointsToGive, giveAPSnapshot.Amount);
        Assert.Equal(player.Id, giveAPSnapshot.PlayerId);
    }
}