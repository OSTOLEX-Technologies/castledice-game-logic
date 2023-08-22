using System.Collections;
using System.Reflection;
using castledice_game_logic;
using castledice_game_logic.Board;
using castledice_game_logic.Board.ContentGeneration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;

namespace castledice_game_logic_tests;



public class CellsPickerTests
{
    public class ExcludeCellsAroundTestData : IEnumerable<object[]>
    {

        private readonly List<object[]> _data = new List<object[]>()
        {
            TreeInTheMiddleCase(),
            TreeInTheLeftUpperCornerCase()
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] TreeInTheMiddleCase()
        {
            var board = GetFullNByNBoard(5);
            board[2, 2].AddContent(new Tree());
            var expectedMatrix = new bool[,]
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, true, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true },
            };
            int radius = 1;
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is Tree);
            return new object[] { board, expectedMatrix, predicate, radius };
        }

        private static object[] TreeInTheLeftUpperCornerCase()
        {
            var board = GetFullNByNBoard(5);
            board[0, 0].AddContent(new Tree());
            var expectedMatrix = new bool[,]
            {
                { true, false, false, true, true },
                { false, false, false, true, true },
                { false, false, false, true, true },
                { true, true, true, true, true },
                { true, true, true, true, true },
            };
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is Tree);
            int radius = 2;
            return new object[] { board, expectedMatrix, predicate, radius };
        }
    }

    [Fact]
    public void TestAvailabilityMatrixExcludesNonExistingCells()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        board.AddCell(0, 0);
        var expectedMatrix = new bool[,]
        {
            { true, false },
            { false, true }
        };
        var picker = new CellsPicker(board);

        var actualMatrix = picker.GetAvailabilityMatrix();

        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestGetAvailabilityMatrixReturnsCopyOfActualMatrix()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        board.AddCell(0, 0);
        var expectedMatrix = new bool[,]
        {
            { true, false },
            { false, true }
        };
        var picker = new CellsPicker(board);

        var matrixToModify = picker.GetAvailabilityMatrix();
        matrixToModify[0, 1] = true;
        var actualMatrix = picker.GetAvailabilityMatrix();
        
        Assert.NotEqual(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestAvailabilityMatrixRowIsFalseAfterExcludeRowsCalled()
    {
        var board = GetFullNByNBoard(n: 3);
        var cellPicker = new CellsPicker(board);
        var expectedMatrix = new bool[,]
        {
            { false, false, false },
            { true, true, true },
            { true, true, true },
        };
        cellPicker.ExcludeRows(0);

        var actualMatrix = cellPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestAvailabilityMatrixColumnIsFalseAfterExcludeColumnsCalled()
    {
        var board = GetFullNByNBoard(n: 3);
        var cellPicker = new CellsPicker(board);
        var expectedMatrix = new bool[,]
        {
            { false, true, true },
            { false, true, true },
            { false, true, true },
        };
        cellPicker.ExcludeColumns(0);

        var actualMatrix = cellPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestExcludeCellsExcludesCellsAccordingToPredicate()
    {
        var board = GetFullNByNBoard(4);
        board[0, 0].AddContent(new Tree());
        board[1, 2].AddContent(new Tree());
        board[2, 1].AddContent(new Tree());
        var expectedMatrix = new bool[,]
        {
            { false, true, true, true },
            { true, true, false, true },
            { true, false, true, true },
            { true, true, true, true },
        };
        var cellPicker = new CellsPicker(board);

        cellPicker.ExcludeCells(c => c.HasContent() == true);
        var actualMatrix = cellPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestExcludeCellsAroundThrowsArgumentExceptionIfNegativeRadiusGiven()
    {
        var board = new Board(CellType.Square);
        var cellPicker = new CellsPicker(board);
        Func<Cell, bool> predicate = c => c.HasContent(ct => ct is Tree);
        Assert.Throws<ArgumentException>(() => cellPicker.ExcludeCellsAround(predicate, -1));
    }

    [Theory]
    [ClassData(typeof(ExcludeCellsAroundTestData))]
    public void TestExcludeCellsAroundExcludesCellsAroundNeededCell(Board board, 
        bool[,] expectedMatrix, 
        Func<Cell, bool> predicate,
        int radius)
    {
        var cellPicker = new CellsPicker(board);
        
        cellPicker.ExcludeCellsAround(predicate, radius);
        var actualMatrix = cellPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestPickRandomCellReturnsOnlyAvailableCells()
    {
        var board = GetFullNByNBoard(3);
        var centralCell = board[1, 1];
        var cellsPicker = new CellsPicker(board);
        cellsPicker.ExcludeRows(0, 2);
        cellsPicker.ExcludeColumns(0, 2);

        var actualCell = cellsPicker.PickRandomCell();
        
        Assert.Same(centralCell, actualCell);
    }

    //TODO: Ask if it is a good idea to throw an exception in such situation
    [Fact]
    public void TestPickRandomCellsThrowsInvalidOperationExceptionIfNoCellsAvailable()
    {
        var board = GetFullNByNBoard(2);
        var cellsPicker = new CellsPicker(board);
        cellsPicker.ExcludeColumns(0, 1);
        cellsPicker.ExcludeRows(0, 1);

        Assert.Throws<InvalidOperationException>(() => cellsPicker.PickRandomCell());
    }

    [Fact]
    public void TestExcludePickedThrowsInvalidOperationExceptionIfCalledBeforePickRandomCell()
    {
        var board = GetFullNByNBoard(3);
        var cellPicker = new CellsPicker(board);

        Assert.Throws<InvalidOperationException>(() => cellPicker.ExcludePicked());
    }

    [Fact]
    public void TestPickedCellPositionIsFalseInAvailabilityMatrixAfterExcludePickedCalled()
    {
        var board = GetFullNByNBoard(3);
        var cellsPicker = new CellsPicker(board);
        var expectedMatrix = new bool[,]
        {
            { false, false, false },
            { false, false, false },
            { false, false, false },
        };
        cellsPicker.ExcludeColumns(0, 2);
        cellsPicker.ExcludeRows(0, 2);

        cellsPicker.PickRandomCell();
        cellsPicker.ExcludePicked();
        var actualMatrix = cellsPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestExcludeAroundPickedThrowsInvalidOperationExceptionIfCalledBeforePickRandomCell()
    {
        var board = GetFullNByNBoard(3);
        var cellPicker = new CellsPicker(board);
        int radius = 1;

        Assert.Throws<InvalidOperationException>(() => cellPicker.ExcludeAroundPicked(radius));
    }

    [Fact]
    public void TestExcludeAroundPickedThrowsArgumentExceptionIfGivenNegativeRadius()
    {
        var board = GetFullNByNBoard(3);
        var cellPicker = new CellsPicker(board);
        int radius = -1;
        cellPicker.PickRandomCell();

        Assert.Throws<ArgumentException>(() => cellPicker.ExcludeAroundPicked(radius));
    }

    [Fact]
    public void TestExcludeAroundPickedSetsCellsInRadiusAvailabilityToFalse()
    {
        var board = GetFullNByNBoard(3);
        var randomGenerator = new Mock<IRandomNumberGenerator>();
        randomGenerator.Setup(rnd => rnd.Range(1, 10)).Returns(5);
        var cellsPicker = new CellsPicker(board, randomGenerator.Object);
        var expectedMatrix = new bool[,]
        {
            { false, false, false },
            { false, true, false },
            { false, false, false }
        };

        cellsPicker.PickRandomCell();
        cellsPicker.ExcludeAroundPicked(1);
        var actualMatrix = cellsPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    private static Board GetFullNByNBoard(int n)
    {
        var board = new Board(CellType.Square);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                board.AddCell(i, j);
            }
        }
        return board;
    }
}