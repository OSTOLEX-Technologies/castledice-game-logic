using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic.Rules;
using Moq;

namespace castledice_game_logic_tests.RulesTests;
using static ObjectCreationUtility;

public class UpgradeRulesTests
{
    private class GetUpgradeCostTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return GetCase(1);
            yield return GetCase(2);
            yield return GetCase(3);
            yield return GetCase(4);
            yield return GetCase(5);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] GetCase(int upgradeCost)
        {
            var board = GetFullNByNBoard(2);
            var upgradeable = new UpgradeableMock() { UpgradeCost = upgradeCost };
            var position = new Vector2Int(0, 0);
            board[position].AddContent(upgradeable);

            return new object[] { board, position, upgradeCost };
        }
    }
    
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

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(10, 10)]
    [InlineData(0, 1)]
    public void GetUpgradeCost_ShouldThrowArgumentException_IfNoCellOnPosition(int x, int y)
    {
        var position = new Vector2Int(x, y);
        var board = new Board(CellType.Square);
        board.AddCell(0, 0);
        board.AddCell(1, 1);

        Assert.Throws<ArgumentException>(() => UpgradeRules.GetUpgradeCost(board, position));
    }

    [Fact]
    public void GetUpgradeCost_ShouldThrowArgumentException_IfNoUpgradeableOnCell()
    {
        var board = GetFullNByNBoard(2);
        var position = new Vector2Int(1, 1);

        Assert.Throws<ArgumentException>(() => UpgradeRules.GetUpgradeCost(board, position));
    }
    
    [Theory]
    [ClassData(typeof(GetUpgradeCostTestCases))]
    public void GetUpgradeCost_ShouldReturnUpgradeCost_OfUpgradeableOnPosition(Board board, Vector2Int position, int expectedUpgradeCost)
    {
        var actualUpgradeCost = UpgradeRules.GetUpgradeCost(board, position);
        
        Assert.Equal(expectedUpgradeCost, actualUpgradeCost);
    }
}