using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic.Rules;

namespace castledice_game_logic_tests.RulesTests;
using static ObjectCreationUtility;

public class ReplaceRulesTests
{
    private class GetReplaceCostTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return GetCase(replaceableRemoveCost: 3, placeablePlaceCost: 1);
            yield return GetCase(replaceableRemoveCost: 4, placeablePlaceCost: 2);
            yield return GetCase(replaceableRemoveCost: 2, placeablePlaceCost: 2);
            yield return GetCase(replaceableRemoveCost: 3, placeablePlaceCost: 3);
            yield return GetCase(replaceableRemoveCost: 1, placeablePlaceCost: 1);
            yield return GetCase(replaceableRemoveCost: 6, placeablePlaceCost: 1);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] GetCase(int replaceableRemoveCost, int placeablePlaceCost)
        {
            var board = GetFullNByNBoard(2);
            var position = new Vector2Int(0, 0);
            var replaceable = new ReplaceableMock() { ReplaceCost = replaceableRemoveCost };
            var placeable = new PlaceableMock() { Cost = placeablePlaceCost };
            board[position].AddContent(replaceable);

            return new object[] { board, position, placeable, CalculateReplaceCost(replaceableRemoveCost, placeablePlaceCost) };
        }
        
        private static int CalculateReplaceCost(int removeCost, int replacementCost)
        {
            return removeCost + replacementCost;
        }
    }
    
    [Fact]
    public void CanReplaceOnCell_ShouldReturnFalse_IfPositionIsNegative()
    {
        var board = GetFullNByNBoard(5);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var position = new Vector2Int(-1, -1);
        
        Assert.False(ReplaceRules.CanReplaceOnCell(board, position, player));
    }

    [Fact]
    public void CanReplaceOnCell_ShouldReturnFalse_IfNoCellOnPosition()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[1, 1].AddContent(playerUnit);
        var position = new Vector2Int(0, 0);
        
        Assert.False(ReplaceRules.CanReplaceOnCell(board, position, player));
    }
    
    [Fact]
    public void CanReplaceOnCell_ShouldReturnFalse_IfNothingToReplace()
    {
        var board = GetFullNByNBoard(5);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        var position = new Vector2Int(1, 1);
        
        Assert.False(ReplaceRules.CanReplaceOnCell(board, position, player));
    }

    [Fact]
    public void CanReplaceOnCell_ShouldReturnFalse_IfNoPlayerUnitsNearby()
    {
        var board = GetFullNByNBoard(5);
        var player = GetPlayer();
        var replaceable = new ReplaceableMock();
        var position = new Vector2Int(1, 1);
        board[1, 1].AddContent(replaceable);
        
        Assert.False(ReplaceRules.CanReplaceOnCell(board, position, player));
    }

    [Fact]
    public void CanReplaceOnCell_ShouldReturnFalse_IfPlayerHasNotEnoughActionPoints()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 3);
        var replaceable = new ReplaceableMock()
        {
            ReplaceCost = 5
        };
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(replaceable);
        var position = new Vector2Int(1, 1);
        
        Assert.False(ReplaceRules.CanReplaceOnCell(board, position, player));
    }

    [Fact]
    //Conditions for player to remove object on cell are following:
    //Object must implement IReplaceable interface
    //Object must not belong to player
    //There must be player units in nearby cells
    //Player must have enough action points
    public void CanReplaceOnCell_ShouldReturnTrue_IfValidToReplace()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 6);
        var replaceable = new ReplaceableMock()
        {
            ReplaceCost = 3
        };
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(replaceable);
        var position = new Vector2Int(1, 1);
        
        Assert.True(ReplaceRules.CanReplaceOnCell(board, position, player));
    }

    [Fact]
    public void CanReplaceOnCell_ShouldReturnFalse_IfReplaceableBelongsToPlayer()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer();
        var replaceable = new ReplaceableMock()
        {
            ReplaceCost = 3,
            Owner = player
        };
        var playerUnit = new PlayerUnitMock(){Owner = player};
        board[0, 0].AddContent(playerUnit);
        board[1, 1].AddContent(replaceable);
        var position = new Vector2Int(1, 1);
        
        Assert.False(ReplaceRules.CanReplaceOnCell(board, position, player));
    }

    [Fact]
    //CanReplaceOnCellIgnoreNeighbours should take into account following rules:
    //Cell must contain removable object
    //Removable object must not belong to player
    //ReplaceRemove cost must be less than player's action points
    public void CanReplaceOnCellIgnoreNeighbours_ShouldReturnTrue_EvenIfNoPlayerUnitsNearby()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 6);
        var replaceable = new ReplaceableMock()
        {
            ReplaceCost = 3
        };
        board[1, 1].AddContent(replaceable);
        var position = new Vector2Int(1, 1);
        
        Assert.True(ReplaceRules.CanReplaceOnCellIgnoreNeighbours(board, position, player));
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(10, 10)]
    [InlineData(0, 1)]
    public void GetReplaceCost_ShouldThrowArgumentException_IfNoCellOnGivenPosition(int x, int y)
    {
        var position = new Vector2Int(x, y);
        var board = new Board(CellType.Square);
        var replacement = new PlaceableMock();
        board.AddCell(0, 0);
        board.AddCell(1, 1);

        Assert.Throws<ArgumentException>(()=>ReplaceRules.GetReplaceCost(board, position, replacement));
    }

    [Fact]
    public void GetReplaceCost_ShouldThrowArgumentException_IfNoReplaceableOnCell()
    {
        var board = GetFullNByNBoard(2);
        var replacement = new PlaceableMock();
        var position = new Vector2Int(0, 0);
        
        Assert.Throws<ArgumentException>(()=>ReplaceRules.GetReplaceCost(board, position, replacement));
    }
    
    [Theory]
    [ClassData(typeof(GetReplaceCostTestCases))]
    public void GetReplaceCost_ShouldReturnReplaceCost_IfReplaceableOnPosition(Board board, Vector2Int position, IPlaceable replacement, int expectedCost)
    {
        var actualCost = ReplaceRules.GetReplaceCost(board, position, replacement);
        
        Assert.Equal(expectedCost, actualCost);
    }
}