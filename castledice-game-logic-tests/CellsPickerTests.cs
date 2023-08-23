﻿using System.Collections;
using castledice_game_logic;
using castledice_game_logic.Board;
using castledice_game_logic.Board.ContentGeneration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;

namespace castledice_game_logic_tests;



public class CellsPickerTests
{
    public class ExcludeCellsAroundTestCases : IEnumerable<object[]>
    {

        private readonly List<object[]> _data = new List<object[]>()
        {
            ObstacleInTheMiddleRadiusOneCase(),
            ObstacleInTheLeftUpperCornerRadiusTwoCase(),
            ObstacleInTheMiddleRadiusTwoCase(),
            ObstacleInTheMiddleRadiusThreeCase()
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] ObstacleInTheMiddleRadiusOneCase()
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

        private static object[] ObstacleInTheLeftUpperCornerRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            board[0, 0].AddContent(new Tree());
            var expectedMatrix = new bool[,]
            {
                { true, false, false, true, true },
                { false, false, false, true, true },
                { false, false, true, true, true },
                { true, true, true, true, true },
                { true, true, true, true, true },
            };
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is Tree);
            int radius = 2;
            return new object[] { board, expectedMatrix, predicate, radius };
        }
        
        private static object[] ObstacleInTheMiddleRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            board[2, 2].AddContent(new Tree());
            var expectedMatrix = new bool[,]
            {
                { true, false, false, false, true },
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, false, false, false, false },
                { true, false, false, false, true },
            };
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is Tree);
            int radius = 2;
            return new object[] { board, expectedMatrix, predicate, radius };
        }
        
        private static object[] ObstacleInTheMiddleRadiusThreeCase()
        {
            var board = GetFullNByNBoard(7);
            board[3, 3].AddContent(new Tree());
            var expectedMatrix = new bool[,]
            {
                {true, true, false, false, false, true, true},
                {true, false, false, false, false, false, true},
                {false, false, false, false, false, false, false},
                {false, false, false, true, false, false, false},
                {false, false, false, false, false, false, false},
                {true, false, false, false, false, false, true},
                {true, true, false, false, false, true, true},
            };
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is Tree);
            int radius = 3;
            return new object[] { board, expectedMatrix, predicate, radius };
        }
    }
    
    public class CountIntersectionsForCellTestCases : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            UpperLeftCornerRadiusTwoCase(),
            UpperRightCornerRadiusTwoCase(),
            LowerRightCornerRadiusThreeCase(),
            LowerLeftCornerRadiusOneCase(),
            MiddleRadiusTwoCase(),
            CellNearToExcludedRowsAndColsRadiusOneCase()
        };
        
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] UpperLeftCornerRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPicker(board);
            var cellPosition = new Vector2Int(0, 0);
            int radius = 2;
            int expectedIntersectionsCount = 13;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }
        
        private static object[] UpperRightCornerRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPicker(board);
            var cellPosition = new Vector2Int(0, 4);
            int radius = 2;
            int expectedIntersectionsCount = 13;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }
        
        private static object[] LowerRightCornerRadiusThreeCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPicker(board);
            var cellPosition = new Vector2Int(4, 4);
            int radius = 3;
            int expectedIntersectionsCount = 24;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }
        
        private static object[] LowerLeftCornerRadiusOneCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPicker(board);
            var cellPosition = new Vector2Int(4, 0);
            int radius = 1;
            int expectedIntersectionsCount = 5;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }

        private static object[] MiddleRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPicker(board);
            var cellPosition = new Vector2Int(2, 2);
            int radius = 2;
            int expectedIntersectionsCount = 0;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }

        private static object[] CellNearToExcludedRowsAndColsRadiusOneCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPicker(board);
            picker.ExcludeColumns(2);
            picker.ExcludeRows(2);
            var cellPosition = new Vector2Int(3, 3);
            int radius = 1;
            int expectedIntersectionsCount = 5;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
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
    [ClassData(typeof(ExcludeCellsAroundTestCases))]
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
    public void TestPickRandomReturnsOnlyAvailableCells()
    {
        var board = GetFullNByNBoard(3);
        var centralCell = board[1, 1];
        var cellsPicker = new CellsPicker(board);
        cellsPicker.ExcludeRows(0, 2);
        cellsPicker.ExcludeColumns(0, 2);

        var actualCell = cellsPicker.PickRandom();
        
        Assert.Same(centralCell, actualCell);
    }

    //TODO: Ask if it is a good idea to throw an exception in such situation
    [Fact]
    public void TestPickRandomThrowsInvalidOperationExceptionIfNoCellsAvailable()
    {
        var board = GetFullNByNBoard(2);
        var cellsPicker = new CellsPicker(board);
        cellsPicker.ExcludeColumns(0, 1);
        cellsPicker.ExcludeRows(0, 1);

        Assert.Throws<InvalidOperationException>(() => cellsPicker.PickRandom());
    }

    [Fact]
    public void TestExcludePickedThrowsInvalidOperationExceptionIfCalledBeforePickRandom()
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

        cellsPicker.PickRandom();
        cellsPicker.ExcludePicked();
        var actualMatrix = cellsPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestExcludeAroundPickedThrowsInvalidOperationExceptionIfCalledBeforePickRandom()
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
        cellPicker.PickRandom();

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

        cellsPicker.PickRandom();
        cellsPicker.ExcludeAroundPicked(1);
        var actualMatrix = cellsPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Theory]
    [ClassData(typeof(CountIntersectionsForCellTestCases))]
    public void TestCountIntersectionsForCellReturnsValidIntersectionsNumber(CellsPicker picker, Vector2Int cellPosition, 
        int radius, int expectedIntersectionsCount)
    {
        int actualIntersectionsCount = picker.CountIntersectionsForCell(cellPosition, radius);
        Assert.Equal(expectedIntersectionsCount, actualIntersectionsCount);
    }

    [Fact]
    public void TestCountIntersectionsForCellThrowsArgumentExceptionIfNegativeRadiusGiven()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPicker(board);
        var cellPosition = new Vector2Int(0, 0);
        int radius = -1;

        Assert.Throws<ArgumentException>(() => picker.CountIntersectionsForCell(cellPosition, radius));
    }

    [Fact]
    public void TestCountIntersectionsForCellThrowsArgumentExceptionIfNegativeCellCoordinateGiven()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPicker(board);
        var cellPosition = new Vector2Int(-1, -1);
        int radius = 1;

        Assert.Throws<ArgumentException>(() => picker.CountIntersectionsForCell(cellPosition, radius));
    }

    [Fact]
    public void TestCountIntersectionsForCellThrowsArgumentExceptionIfCellCoordinatesAreOutsideOfBoard()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPicker(board);
        var cellPosition = new Vector2Int(3, 3);
        int radius = 1;
        
        Assert.Throws<ArgumentException>(() => picker.CountIntersectionsForCell(cellPosition, radius));

    }

    [Fact]
    public void TestExcludeCellSetsCellToFalseInAvailabilityMatrix()
    {
        var board = GetFullNByNBoard(3);
        var expectedMatrix = new bool[,]
        {
            { true, true, true },
            { true, false, true },
            { true, true, true }
        };
        var picker = new CellsPicker(board);
        var cellPosition = new Vector2Int(1, 1);
        
        picker.ExcludeCell(cellPosition);
        var actualMatrix = picker.GetAvailabilityMatrix();

        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void TestExcludeCellThrowsArgumentExceptionIfNegativePositionIsGiven()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPicker(board);
        var position = new Vector2Int(-1, -1);

        Assert.Throws<ArgumentException>(() => picker.ExcludeCell(position));
    }

    [Fact]
    public void TestExcludeCellThrowsArgumentExceptionIfCellPositionIsOutsideTheBoard()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPicker(board);
        var position = new Vector2Int(4, 4);

        Assert.Throws<ArgumentException>(() => picker.ExcludeCell(position));
    }

    [Fact]
    public void TestIncludeCellThrowsArgumentExceptionIfCellPositionIsNegative()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPicker(board);
        var position = new Vector2Int(-1, -1);

        Assert.Throws<ArgumentException>(() => picker.IncludeCell(position));
    }

    [Fact]
    public void TestIncludeCellThrowsArgumentExceptionIfCellIsOutsideOfTheBoard()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPicker(board);
        var position = new Vector2Int(4, 4);

        Assert.Throws<ArgumentException>(() => picker.IncludeCell(position));
    }

    [Fact]
    public void TestIncludeCellThrowsInvalidOperationExceptionIfCellDoesntExistOnTheBoard()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        var picker = new CellsPicker(board);
        var position = new Vector2Int(0, 0);

        Assert.Throws<InvalidOperationException>(() => picker.IncludeCell(position));
    }

    [Fact]
    public void TestPickSmartThrowsInvalidOperationExceptionIfNoCellsAvailable()
    {
        var board = GetFullNByNBoard(2);
        var picker = new CellsPicker(board);
        picker.ExcludeColumns(0, 1);
        picker.ExcludeRows(0, 1);

        Assert.Throws<InvalidOperationException>(() => picker.PickSmart(2));
    }
    
    [Fact]
    public void TestPickSmartReturnsMostOptimalCell()
    {
        var board = GetFullNByNBoard(5);
        var cellsPicker = new CellsPicker(board);
        cellsPicker.ExcludeColumns(0, 1);
        cellsPicker.ExcludeRows(0, 1);
        cellsPicker.IncludeCell(new Vector2Int(0, 0));
        var expectedCell = board[0, 0];

        var actualCell = cellsPicker.PickSmart(2);
        
        Assert.Equal(expectedCell, actualCell);
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