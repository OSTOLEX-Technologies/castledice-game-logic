using System.Collections;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class MoveCellsSelectorTests
{
    public class SelectMoveCellWithoutTypeTestCases : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            TwoCastlesOnBoardCase(),
        };
        
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] TwoCastlesOnBoardCase()
        {
            var firstPlayer = GetPlayer();
            var secondPlayer = GetPlayer();
            var firstCastle = new CastleGO(firstPlayer);
            var secondCastle = new CastleGO(secondPlayer);
            var board = GetFullNByNBoard(10);
            board[0, 0].AddContent(firstCastle);
            board[9, 9].AddContent(secondCastle);
            List<MoveCell> expectedCells = new List<MoveCell>()
            {
                new MoveCell(board[0, 0], MoveType.Upgrade),
                new MoveCell(board[0, 1], MoveType.Place),
                new MoveCell(board[1, 0], MoveType.Place),
                new MoveCell(board[1, 1], MoveType.Place)
            };
            return new object[] { board, firstPlayer, expectedCells };
        }


    }
    
    [Theory]
    [ClassData(typeof(SelectMoveCellWithoutTypeTestCases))]
    public void SelectMoveCells_ShouldReturnListWithAllPossibleMoveCells_IfMoveTypeIsNotSpecified(Board board, Player player, List<MoveCell> expectedCells)
    {
        var cellsSelector = new MoveCellsSelector(board);

        var actualCells = cellsSelector.SelectMoveCells(player);
        
        Assert.Equal(expectedCells, actualCells);
    }
}