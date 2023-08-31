using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic.Rules;
using Moq;

namespace castledice_game_logic_tests.RulesTests;
using static ObjectCreationUtility;

public class UpgradeRulesTests
{
    [Fact]
    public void CanUpgradeOnCell_ShouldReturnFalse_IfNoUpgradeableContentOnCell()
    {
        var player = GetPlayer();
        var cell = GetCell();
        
        Assert.False(UpgradeRules.CanUpgradeOnCell(cell, player));
    }

    [Fact]
    public void CanUpgradeOnCell_ShouldReturnFalse_IfNotEnoughActionPoints()
    {
        int playerActionPoints = 3;
        int upgradeCost = 4;
        var player = GetPlayer(actionPoints: playerActionPoints);
        var cell = GetCell();
        var contentMock = new UpgradeableMock()
        {
            Upgradeable = true,
            UpgradeCost = upgradeCost
        };
        cell.AddContent(contentMock);
        
        Assert.False(UpgradeRules.CanUpgradeOnCell(cell, player));
    }

    [Fact]
    public void CanUpgradeOnCell_ShouldReturnTrue_IfPlayerHasEnoughActionPoints()
    {
        int playerActionPoints = 6;
        int upgradeCost = 4;
        var player = GetPlayer(actionPoints: playerActionPoints);
        var cell = GetCell();
        var contentMock = new UpgradeableMock()
        {
            Upgradeable = true,
            UpgradeCost = upgradeCost
        };
        cell.AddContent(contentMock);
        
        Assert.True(UpgradeRules.CanUpgradeOnCell(cell, player));
    }
}