using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.Utilities;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class CellNeighboursGetterTests
{
    private class GetNeighboursTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return NoNeighboursSatisfyThePredicateCase();
            yield return TwoNeighboursSatisfyThePredicate();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] NoNeighboursSatisfyThePredicateCase()
        {
            var board = GetFullNByNBoard(3);
            var position = new Vector2Int(1, 1);
            Func<Cell, bool> predicate = (c) => c.HasContent();
            var expectedList = new List<Cell>();
            return new object[] {board, position, predicate, expectedList};
        }

        private static object[] TwoNeighboursSatisfyThePredicate()
        {
            var board = GetFullNByNBoard(3);
            var position = new Vector2Int(1, 1);
            var obstacle = GetObstacle();
            var unit = new PlayerUnitMock();
            Func<Cell, bool> predicate = (c) => c.HasContent();
            board[0, 0].AddContent(obstacle);
            board[2, 2].AddContent(unit);
            var expectedList = new List<Cell>()
            {
                board[0, 0],
                board[2, 2]
            };
            return new object[] {board, position, predicate, expectedList};
        }
    }
    
    [Theory]
    [InlineData(0, -1)]
    [InlineData(-1, -1)]
    [InlineData(10, 10)]
    [InlineData(0, 1)]
    public void GetNeighbours_ShouldThrowArgumentException_IfNoCellOnPosition(int x, int y)
    {
        var position = new Vector2Int(x, y);
        var board = new Board(CellType.Square);
        Func<Cell, bool> predicate = (c) => c.HasContent();
        board.AddCell(0, 0);
        board.AddCell(1, 1);
        
        Assert.Throws<ArgumentException>(() => CellNeighboursGetter.GetNeighbours(board, position, predicate));
    }

    [Theory]
    [ClassData(typeof(GetNeighboursTestCases))]
    public void GetNeighbours_ShouldReturnListWithCells_ThatSatisfyThePredicate(Board board, Vector2Int position,
        Func<Cell, bool> predicate, List<Cell> expectedList)
    {
        var actualList = CellNeighboursGetter.GetNeighbours(board, position, predicate);
        
        Assert.Equal(expectedList.Count, actualList.Count);
        foreach (var cell in expectedList)
        {
            Assert.Contains(cell, actualList);
        }
    }
}